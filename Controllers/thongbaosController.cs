using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectDAA1;
using System.IO;

namespace ProjectDAA1.Controllers
{
    public class thongbaosController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: thongbaos
        public async Task<ActionResult> Index()
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                var id = session.idgv;
                var thongbaos = db.thongbaos.Where(x => x.taikhoan.idgv == id).Include(t => t.taikhoan);
                return View(await thongbaos.ToListAsync());
            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        // GET: thongbaos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thongbao thongbao = await db.thongbaos.FindAsync(id);
            if (thongbao == null)
            {
                return HttpNotFound();
            }
            return View(thongbao);
        }

        // GET: thongbaos/Create
        public ActionResult Create()
        {
           
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                ViewBag.idtk = new SelectList(db.taikhoans, "idtk", "matk");
                return View();
            }
            else
            {
                return RedirectToRoute("login");
            }
        }

        // POST: thongbaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HttpPostedFileBase file, thongbao thongbao)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
                    var idgv = session.idgv;
                    thongbao.idtk = db.thongbaos.Where(x => x.taikhoan.idgv == idgv).Select(x => x.idtk).FirstOrDefault();
                    var tb = new thongbao();
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("/Uploadfile/"), _FileName);
                    thongbao.file = "/Uploadfile/" + _FileName;
                    db.thongbaos.Add(thongbao);
                    file.SaveAs(_path);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else if (file == null)
                {
                    var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
                    var idgv = session.idgv;
                    thongbao.idtk = db.thongbaos.Where(x => x.taikhoan.idgv == idgv).Select(x => x.idtk).FirstOrDefault();
                    db.thongbaos.Add(thongbao);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            } 

            ViewBag.idtk = new SelectList(db.taikhoans, "idtk", "matk", thongbao.idtk);
            return View(thongbao);
        }

        // GET: thongbaos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
            if (session != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                thongbao thongbao = await db.thongbaos.FindAsync(id);
                if (thongbao == null)
                {
                    return HttpNotFound();
                }
                ViewBag.idtk = new SelectList(db.taikhoans, "idtk", "matk", thongbao.idtk);
                return View(thongbao);
            }
            else
            {
                return RedirectToRoute("login");
            }
            
        }

        // POST: thongbaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HttpPostedFileBase file, thongbao thongbao)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("/Uploadfile/"), _FileName);
                    thongbao.file = "/Uploadfile/" + _FileName;
                    file.SaveAs(_path);
                }
                var session = (UserLogin)Session[ProjectDAA1.Common.CommonConstants.USER_SESSION];
                var idgv = session.idgv;
                thongbao.taikhoan.idgv = idgv;
                db.Entry(thongbao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idtk = new SelectList(db.taikhoans, "idtk", "matk", thongbao.idtk);
            return View(thongbao);
        }

        // GET: thongbaos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thongbao thongbao = await db.thongbaos.FindAsync(id);
            if (thongbao == null)
            {
                return HttpNotFound();
            }
            return View(thongbao);
        }

        // POST: thongbaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            thongbao thongbao = await db.thongbaos.FindAsync(id);
            db.thongbaos.Remove(thongbao);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
