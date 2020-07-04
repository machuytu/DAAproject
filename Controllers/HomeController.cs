using ProjectDAA1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
                var listthongbaochung = db.thongbaos.Where(x => x.tag == "Thông Báo Chung")
                    .OrderByDescending(x => x.thoigiandang).Take(10).ToList();
                ViewBag.thongBaoChung = listthongbaochung;
            }
            else
            {
                var listthongbaochung = db.thongbaos.Where(x => x.tag == "THÔNG BÁO CHUNG")
                    .OrderByDescending(x => x.thoigiandang).Take(10).ToList();
                ViewBag.thongBaoChung = listthongbaochung;
                var listthongnghibu = db.thongbaos.Where(x => x.tag == "THÔNG BÁO NGHỈ, BÙ")
                    .OrderByDescending(x => x.thoigiandang).Take(10).ToList();
                ViewBag.listthongnghibu = listthongnghibu;
            }
            return View();
        }

        public FileResult Download(string fileName)
        {
            var name = fileName.Split('/');
            byte[] fileBytes = System.IO.File.ReadAllBytes(fileName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, name[0]);
        }

        public async Task<ActionResult> Details(int? id)
        {
            thongbao thongbao = await db.thongbaos.FindAsync(id);
            if (thongbao == null)
            {
                return HttpNotFound();
            }
            return View(thongbao);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var datenow = DateTime.Now;
            ViewBag.Viewdkhp =1;
            var listkhoahoc = db.dangkyhocphans.Where(x => x.thoigianbd <= datenow && x.thoigiankt >= datenow).ToList();
            if (listkhoahoc.Count() > 0)
            {
                ViewBag.Viewdkhp = listkhoahoc.SingleOrDefault();
            }
            return PartialView();
        }
        
    }
}