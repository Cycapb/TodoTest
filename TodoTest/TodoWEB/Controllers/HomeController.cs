using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using TodoWEB.Abstract;
using TodoWEB.Concrete;
using TodoWEB.Infrastructure;

namespace TodoWEB.Controllers
{
    public class HomeController : Controller
    {
        [BasicAuthentication]
        public ActionResult Index()
        {
            return View();
        }
    }
}