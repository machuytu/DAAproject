using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectDAA1.Models;
using System.Configuration;
using ProjectDAA1.Common;

namespace ProjectDAA1.Controllers
{
    public class LoginController : Controller
    {
        private MyDatabaseEntities5 db = new MyDatabaseEntities5();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = db.taikhoan.SingleOrDefault(x => x.matk == model.UserName);
                if (result == null)
                {
                    ViewBag.thunhat = "khong ton tai";
                }
                else
                    if (result.password1 != model.Password)
                {
                    ViewBag.thuhai = "sai password";
                }
                else
                {
                    if (result.nhom == "Sinh viên")
                    {
                        ViewBag.thuba = "la sinh vien";
                    }
                    else
                    if (result.nhom == "Giảng viên")
                    {
                        ViewBag.thutu = "la giang vien";
                    }
                    else
                    {
                        ViewBag.thunam = "la quan tri vien";
                    }
                }


                //var reullsult = dao.Login(model.UserName, HashPassword.MD5Hash(model.Password));
                //var result =
                //if (result == 1)
                //{
                //    var user = dao.GetById(model.UserName);
                //    var userSession = new UserLogin();
                //    userSession.UserName = user.UserName;
                //    userSession.UserID = user.ID;
                //    Session.Add(CommonConstants.USER_SESSION, userSession);
                //    return Redirect("/");
                //}
                //else if (result == 0)
                //{
                //    ModelState.AddModelError("", "Tai Khoan Khong Ton Tai");
                //}
                //else if (result == -1)
                //{
                //    ModelState.AddModelError("", "Tai Khoan Dang Bi Khoa");
                //}
                //else if (result == -2)
                //{
                //    ModelState.AddModelError("", "Mat Khau Khong Dung");
                //}
                //else
                //{
                //    ModelState.AddModelError("", "Đăng nhập không đúng");
                //}
            }
            return View(model);
        }
    }
}