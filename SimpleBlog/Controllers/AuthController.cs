using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleBlog.ViewModels;
namespace SimpleBlog.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login() //name of the razor file cshtml
        {
            return View(new AuthLogin { 
            });
        }
        [HttpPost]
        public ActionResult Login(AuthLogin form)
        {
            if (!ModelState.IsValid)
                return View(form);
            if (form.Username != "thanh")
            {
                ModelState.AddModelError("Username", "Username is not the admin!");
                return View(form);
            }
            return Content("Form is valid");
        }
	}
}