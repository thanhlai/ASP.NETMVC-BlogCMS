using SimpleBlog.Infastructure;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Areas.Admin.ViewModels
{
    public class PostsIndex
    {
        public PagedData<Post> Posts { get; set; }
    }
}