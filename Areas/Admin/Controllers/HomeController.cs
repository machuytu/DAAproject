using ProjectDAA1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectDAA1.Areas.Admin.Controllers
{
    
    public class HomeController : AdminAuthController
    {
        // GET: Admin/Home
        
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Create()
        {
            return View();
        }
    }
}