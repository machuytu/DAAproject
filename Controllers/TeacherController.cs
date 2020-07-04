using ProjectDAA1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                var result = db.lops.Where(x => x.idgv == id);
                return View(await result.ToListAsync());

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

            //using (var db = new MyDatabaseEntities9())
            //{
            var result = db.hocs.Where(x => x.idlop == id).Select(x => x.sinhvien);
            return View(await result.ToListAsync());
            //}
        }

        [HttpGet]
        public async Task<ActionResult> dsSVLopCN(int id)
        {

            var result = db.sinhviens.Where(x => x.idlopcn == id);
            return View(await result.ToListAsync());
        }


        [HttpGet]
        public async Task<ActionResult> GetTKB()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                using (var db = new MyDatabaseEntities9())
                {
                    var dkhp = db.dangkyhocphans.OrderByDescending(x => x.iddkhp);
                    return View(await dkhp.ToListAsync());
                }
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
        public ActionResult GetBangDiem(int id)
        {
            var query = db.lops.Where(x => x.idlop == id).Select(x => x.mon).FirstOrDefault();
            var result = db.hocs.Where(x => x.idlop == id)
                .Select(x => new BangDiem
                {
                    idsv = x.idsv,
                    masv = x.sinhvien.masv,
                    hoten = x.sinhvien.hoten,
                    diemqt = x.diemqt,
                    diemth = x.diemth,
                    diemgk = x.diemgk,
                    diemck = x.diemck,
                    diemtb = x.diemtb,
                });
            return View( new DBContext()
            {
                id = id,
                mon = query,
                dsbd = result.ToList()
            });;
        }
    }
}