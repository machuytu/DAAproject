using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectDAA1.Models;
using System.Configuration;
using ProjectDAA1.Common;
using System.Drawing.Printing;
using ProjectDAA1;
namespace ProjectDAA1.Controllers
{
    public class LoginController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();
        // GET: Login
        public async System.Threading.Tasks.Task<ActionResult> Login()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            
            if (session == null)
                return View();
            else
            {
                taikhoan user = await db.taikhoans.FindAsync(session.UserName);

                if (user.nhom == "Sinh viên" || user.nhom == "Giảng viên")
                    return RedirectToAction("Index", "Home");
                else
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Login(LoginModel model)
        {
            var result = db.taikhoans.SingleOrDefault(x => x.matk == model.UserName);
            if (ModelState.IsValid)
            {
                if (result == null)
                {
                    ViewData["ErrorMessage"] = "Tài khoản không đúng";
                    return View();
                }
                else if (result.password != model.Password)
                {
                    ViewData["ErrorMessage"] = "Mật khẩu không đúng";
                    return View();
                }
                else
                {
                    if (result.nhom == "Sinh viên")
                    {
                        //var user = dao.GetById(model.UserName);
                        taikhoan user = await db.taikhoans.FindAsync(model.UserName);
                        var userSession = new UserLogin();
                        userSession.UserName = user.matk;
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        return Redirect("/");
                    }
                    else if (result.nhom == "Giảng viên")
                    {
                        taikhoan user = await db.taikhoans.FindAsync(model.UserName);
                        var userSession = new UserLogin();
                        userSession.UserName = user.matk;
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        return Redirect("/");
                    }
                    else
                    {
                        taikhoan user = await db.taikhoans.FindAsync(model.UserName);
                        var userSession = new UserLogin();
                        userSession.UserName = user.matk;
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
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
        [Route("Logout")]
        public ActionResult Logout()
        {
            Session[Common.CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

    }
}