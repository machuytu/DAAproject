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

namespace ProjectDAA1.Areas.Admin.Controllers
{
    public class thongbaosController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/thongbaos
        public async Task<ActionResult> Index()
        {
            var thongbaos = db.thongbaos.Include(t => t.taikhoan);
            return View(await thongbaos.ToListAsync());
        }

        // GET: Admin/thongbaos/Details/5
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

        // GET: Admin/thongbaos/Create
        public ActionResult Create()
        {
            ViewBag.idtk = new SelectList(db.taikhoans.Where(x => x.idsv == null), "idtk", "matk");
            return View();
        }

        // POST: Admin/thongbaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(thongbao thongbao, HttpPostedFileBase file)
        {
            
            if (ModelState.IsValid && file.ContentLength > 0)
            {
                var tb = new thongbao();
                string _FileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/Uploadfile/"), _FileName);
                thongbao.file = "~/Uploadfile/" + _FileName;
                tb = thongbao;
                db.thongbaos.Add(thongbao);
                file.SaveAs(_path);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Message = "File Uploaded Fail!!";

            ViewBag.idtk = new SelectList(db.taikhoans.Where(x => x.idsv == null), "idtk", "matk", thongbao.idtk);
            return View(thongbao);
        }

        // GET: Admin/thongbaos/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            ViewBag.idtk = new SelectList(db.taikhoans.Where(x => x.idsv == null), "idtk", "matk", thongbao.idtk);
            return View(thongbao);
        }

        // POST: Admin/thongbaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(thongbao thongbao, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Uploadfile/"), _FileName);
                    thongbao.file = "~/Uploadfile/" + _FileName;
                    file.SaveAs(_path);
                }
                db.Entry(thongbao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idtk = new SelectList(db.taikhoans.Where(x => x.idsv == null), "idtk", "matk", thongbao.idtk);
            return View(thongbao);
        }

        // GET: Admin/thongbaos/Delete/5
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

        [HttpPost]
        public async Task<ActionResult> Deleteuser(int id)
        {
            try
            {
                thongbao thongbao = await db.thongbaos.FindAsync(id);
                db.thongbaos.Remove(thongbao);
                await db.SaveChangesAsync();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/thongbaos/Delete/5
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
