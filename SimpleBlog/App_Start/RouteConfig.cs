using SimpleBlog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var namespaces = new[] { typeof(PostsController).Namespace };

            //learn how to write my own route
            //name of the route
            routes.MapRoute("Home", "", new { controller = "Posts", action = "Index" }, namespaces);
            //controller is like class, action is like method
            routes.MapRoute("Login", "login", new { controller = "Auth", action = "Login" }, namespaces);
        }
    }
}