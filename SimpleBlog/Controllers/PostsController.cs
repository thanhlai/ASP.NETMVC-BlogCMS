using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using SimpleBlog.ViewModels;
using SimpleBlog.Infastructure;
using System.Text.RegularExpressions;
namespace SimpleBlog.Controllers
{
    public class PostsController : Controller
    {
        private const int PostsPerPage = 12;

        public ActionResult Index(int page = 1) //name of the razor file cshtml
        {
            var baseQuery = Database.Session.Query<Post>().Where(t => t.DeleteAt == null).OrderByDescending(t => t.CreateAt);

            var totalPostCount = baseQuery.Count();
            var postIds = baseQuery.Skip((page - 1) * PostsPerPage).Take(PostsPerPage).Select(t => t.Id).ToArray();
            var posts = baseQuery.Where(t => postIds.Contains(t.Id)).FetchMany(t => t.Tags).Fetch(t => t.User).ToList();

            return View(new PostsIndex 
            {
                Posts = new PagedData<Post>(posts, totalPostCount,page, PostsPerPage)
            });
        }

        public ActionResult Tag (string idAndSlug, int page = 1)
        {
            var parts = SeperateIdAndSlug(idAndSlug);
            if (parts == null)
                return HttpNotFound();
            var tag = Database.Session.Load<Tag>(parts.Item1);
            if (tag == null)
                return HttpNotFound();
            if (!tag.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("Tag", new { id = parts.Item1, slug = tag.Slug });

            var totalPostCount = tag.Posts.Count();
            var postIds = tag.Posts
                .OrderByDescending(g => g.CreateAt)
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .Where(t => t.DeleteAt == null)
                .Select(t => t.Id).ToArray();
            var posts = Database.Session.Query<Post>()
                .OrderByDescending(b => b.CreateAt)
                .Where(t => postIds.Contains(t.Id))
                .FetchMany(f => f.Tags)
                .Fetch(f => f.User)
                .ToList();

            return View(new PostsTag
            {
                Tag = tag,
                Posts = new PagedData<Post>(posts, totalPostCount,page, PostsPerPage)
            });
        }

        public ActionResult Show(string idAndSlug)
        {
            var parts = SeperateIdAndSlug(idAndSlug);
            if (parts == null)
                return HttpNotFound();
            var post = Database.Session.Load<Post>(parts.Item1);
            if (post == null || post.IsDeleted)
                return HttpNotFound();
            //SEO
            if (!post.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("Post", new { id = parts.Item1, slug = post.Slug });
            return View(new PostsShow
            {
                Post = post
            });
        }
        private System.Tuple<int,string> SeperateIdAndSlug(string idAndSlug)
        {
            var matches = Regex.Match(idAndSlug, @"^(\d+)\-(.*)?$");
            if (!matches.Success)
                return null;

            var id = int.Parse(matches.Result("$1"));
            var slug = matches.Result("$2");
            return Tuple.Create(id, slug);
        }
    }
}