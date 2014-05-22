using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using SimpleBlog.ViewModels;
using SimpleBlog.Models;
using NHibernate.Linq;
namespace SimpleBlog.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login() //name of the razor file cshtml
        {
            return View(new AuthLogin { 
            });
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }
        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {

            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == form.Username);

            if (user == null)
                SimpleBlog.Models.User.FakeHash();

            if (user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Username", "Username or Password is not correct" );

            if (!ModelState.IsValid)
                return View(form);

            FormsAuthentication.SetAuthCookie(user.Username, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);
            return RedirectToRoute("home");
        }

	}
}