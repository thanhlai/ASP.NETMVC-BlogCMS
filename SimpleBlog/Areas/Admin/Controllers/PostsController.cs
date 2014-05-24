using SimpleBlog.Infastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using SimpleBlog.Models;
using SimpleBlog.Areas.Admin.ViewModels;
namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("posts")]
    public class PostsController : Controller
    {
        private const int PostsPerPage = 5;

        public ActionResult Index(int page = 1)
        {
            var totalPostCount = Database.Session.Query<Post>().Count();
            var currentPostPage = Database.Session.Query<Post>()
                .OrderByDescending(c => c.CreateAt)
                .Skip((page - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .ToList();

            return View(new PostsIndex 
            { 
                Posts = new PagedData<Post>(currentPostPage, totalPostCount, page, PostsPerPage)
            });
        }

        public ActionResult New()
        {
            return View("Form", new PostsForm 
            {
                IsNew = true
            });
        }

        public ActionResult Edit(int id)
        {
            var post = Database.Session.Load<Post>(id);
            if (post == null)
                return HttpNotFound();

            return View("Form", new PostsForm 
            {
                IsNew = false,
                PostId = id,
                Title = post.Title,
                Slug = post.Slug,
                Content = post.Content
            });

        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = (form.PostId == null);
            if (!ModelState.IsValid)
                return View(form);

            Post post;
            if(form.IsNew)
            {
                post = new Post 
                {
                    CreateAt = DateTime.Now,
                    User = Auth.User,
                };
            }
            else
            {
                post = Database.Session.Load<Post>(form.PostId);
                if (post == null)
                    return HttpNotFound();
                post.UpdateAt = DateTime.Now;
            }
            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            Database.Session.SaveOrUpdate(post);

            return RedirectToAction("Index");
        }
    }
}