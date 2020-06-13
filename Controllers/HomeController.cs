﻿using ProjectDAA1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectDAA1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();
        public ActionResult Index()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session == null)
            {
                ViewBag.thongBaoChung = "";
            }
            else
            {
                ViewBag.thongBaoRieng = "";
            }
            return View();
        }
        [ChildActionOnly]
        public ActionResult Header()
        {
            var datenow = DateTime.Now;
            ViewBag.Viewdkhp =1;
            var listkhoahoc = db.dangkyhocphans.Where(x => x.thoigianbd <= datenow && x.thoigiankt >= datenow).ToList();
            if (listkhoahoc.Count() > 0)
            {
                ViewBag.Viewdkhp = 0;
            }
            return PartialView();
        }
        
    }
}