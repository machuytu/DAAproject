using ProjectDAA1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectDAA1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        MyDatabaseEntities9 db = new MyDatabaseEntities9();
       
        [HttpGet]
        public async Task<ActionResult> dsLop()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var id = session.idgv;
                var dsdkhp = db.lops
                    .Where(x => x.idgv == id)
                    .Select(x => x.dangkyhocphan)
                    //.OrderBy(x=>x.iddkhp)
                    .Distinct()
                    .OrderByDescending(x => x.iddkhp);
                var result = db.lops.Where(x => x.idgv == id);
                return View(new DBContext() {
                    dsdkhp = await dsdkhp.ToListAsync(),
                    dslop = await result.ToListAsync(),
                });

            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        [HttpGet]
        public async Task<ActionResult> dsLopCN()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var id = session.idgv;

                var result = db.lopcns.Where(x => x.idgv == id);
                return View(await result.ToListAsync());

            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        [HttpGet]
        public async Task<ActionResult> dsSVLop(int id)
        {
            var result = db.hocs.Where(x => x.idlop == id).Select(x => x.sinhvien);
            return View(await result.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> dsSVLopCN(int id)
        {

            var result = db.sinhviens.Where(x => x.idlopcn == id);
            return View(await result.ToListAsync());
        }

        public async Task<ActionResult> doimatkhau()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var idgv = session.idgv;
                taikhoan taikhoan = db.taikhoans.Where(x => x.idgv == idgv).SingleOrDefault();
                return View(taikhoan);
            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> doimatkhau(taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taikhoan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Redirect("/giangvien/thongtincanhan");
            }
            return View(taikhoan);
        }

        public async Task<ActionResult> thongtincanhan()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var idgv = session.idgv;
                giangvien giangvien = await db.giangviens.FindAsync(idgv);
                return View(giangvien);
            }
            else
            {
                return RedirectToRoute("login");
            }
        }
        public async Task<ActionResult> suathongtin(int id)
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var idgv = session.idgv;
                var result = db.giangviens.Where(x => x.idgv == idgv).SingleOrDefault();
                giangvien giangvien = await db.giangviens.FindAsync(idgv);
                ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenkhoa", giangvien.idkhoa);
                return View(giangvien);
            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> suathongtin(HttpPostedFileBase file, giangvien giangvien)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("/Assets/images"), _FileName);
                    giangvien.hinhanh = "/Assets/images/" + _FileName;
                    file.SaveAs(_path);
                }
                db.Entry(giangvien).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Redirect("/giangvien/thongtincanhan");
            }
            ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenkhoa", giangvien.idkhoa);
            return View(giangvien);
        }

        [HttpGet]
        public async Task<ActionResult> GetTKB()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                ViewBag.gv = db.giangviens.Where(x => x.idgv == session.idgv).FirstOrDefault();
                var dkhp = db.dangkyhocphans.OrderByDescending(x => x.iddkhp);
                return View(await dkhp.ToListAsync());
            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        [HttpGet]
        public JsonResult ajaxTKB(int iddkhp, int idgv)
        {
            using (var db = new MyDatabaseEntities9())
            {
                var tkb = db.lops.Where(x => x.iddkhp == iddkhp && x.idgv == idgv)
                    .Select(x => new
                    {
                        malop = x.malop,
                        thu = x.thu,
                        tietbd = x.tietbd,
                        tietkt = x.tietkt,
                        tenmon = x.mon.tenmon,
                        tclt = x.mon.tclt,
                        tcth = x.mon.tcth,
                    }).ToList();
                return new JsonResult { Data = tkb, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetBangDiem(int id)
        {
            var mon = await db.lops.Where(x => x.idlop == id).Select(x => x.mon).FirstOrDefaultAsync();
            var result = db.hocs.Where(x => x.idlop == id);
            ViewBag.id = id;
            if (TempData["Temp"] != null)
            {
                ViewBag.thanhphan = TempData["Temp"].ToString();
            };
            ViewBag.mon = mon;
            return View(await result.ToListAsync());
        }
    }
}