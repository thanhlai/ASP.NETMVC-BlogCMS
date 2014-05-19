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
	}
}