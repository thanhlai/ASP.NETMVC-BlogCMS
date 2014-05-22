using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Infastructure;
using System;
using System.Collections.Generic;
using NHibernate.Linq;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles="admin")]
    [SelectedTab("users")]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View(new UsersIndex { 
                Users = Database.Session.Query<User>().ToList()
            });
        }
        public ActionResult New()
        {
            return View(new UsersNew
            {

            });
        }
        [HttpPost]
        public ActionResult New(UsersNew form)
        {
            if (Database.Session.Query<User>().Any(f => f.Username == form.Username))
                ModelState.AddModelError("Username", "Username already exists");

            if (!ModelState.IsValid)
                return View(form);

            var user = new User
            {
                Email = form.Email,
                Username = form.Username
            };

            user.SetPassword(form.Password);
            return RedirectToAction("index");
        }
	}
}