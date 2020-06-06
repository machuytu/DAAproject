using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectDAA1.Models;

namespace ProjectDAA1.Areas.Admin.Controllers
{
    public class sinhviens1Controller : Controller
    {
        private MyDatabaseEntities8 db = new MyDatabaseEntities8();

        // GET: Admin/sinhviens1
        public ActionResult Index()
        {
            var sinhviens = db.sinhviens.Include(s => s.lopcn);
            return View(sinhviens.ToList());
        }

        // GET: Admin/sinhviens1/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sinhvien sinhvien = db.sinhviens.Find(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            return View(sinhvien);
        }

        // GET: Admin/sinhviens1/Create
        public ActionResult Create()
        {
            ViewBag.malopcn = new SelectList(db.lopcns, "malopcn", "magv");
            return View();
        }

        // POST: Admin/sinhviens1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "masv,hoten,gioitinh,ngaysinh,malopcn,diachi,quequan,sdt,bachoc")] sinhvien sinhvien)
        {
            if (ModelState.IsValid)
            {
                db.sinhviens.Add(sinhvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.malopcn = new SelectList(db.lopcns, "malopcn", "magv", sinhvien.malopcn);
            return View(sinhvien);
        }

        // GET: Admin/sinhviens1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sinhvien sinhvien = db.sinhviens.Find(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.malopcn = new SelectList(db.lopcns, "malopcn", "magv", sinhvien.malopcn);
            return View(sinhvien);
        }

        // POST: Admin/sinhviens1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "masv,hoten,gioitinh,ngaysinh,malopcn,diachi,quequan,sdt,bachoc")] sinhvien sinhvien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinhvien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.malopcn = new SelectList(db.lopcns, "malopcn", "magv", sinhvien.malopcn);
            return View(sinhvien);
        }

        // GET: Admin/sinhviens1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sinhvien sinhvien = db.sinhviens.Find(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            return View(sinhvien);
        }

        // POST: Admin/sinhviens1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            sinhvien sinhvien = db.sinhviens.Find(id);
            db.sinhviens.Remove(sinhvien);
            db.SaveChanges();
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
