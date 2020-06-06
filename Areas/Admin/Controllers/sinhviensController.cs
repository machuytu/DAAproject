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
    public class sinhviensController : Controller
    {
        private MyDatabaseEntities5 db = new MyDatabaseEntities5();

        // GET: Admin/sinhviens
        public ActionResult Index()
        {
            var sinhvien = db.sinhvien.Include(s => s.lopcn);
            return View(sinhvien.ToList());
        }

        // GET: Admin/sinhviens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sinhvien sinhvien = db.sinhvien.Find(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            return View(sinhvien);
        }

        // GET: Admin/sinhviens/Create
        public ActionResult Create()
        {
            ViewBag.malopcn = new SelectList(db.lopcn, "malopcn", "magv");
            return View();
        }

        // POST: Admin/sinhviens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "masv,hoten,gioitinh,ngaysinh,malopcn,diachi,quequan,sdt,bachoc")] sinhvien sinhvien)
        {
            if (ModelState.IsValid)
            {
                db.sinhvien.Add(sinhvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.malopcn = new SelectList(db.lopcn, "malopcn", "magv", sinhvien.malopcn);
            return View(sinhvien);
        }

        // GET: Admin/sinhviens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sinhvien sinhvien = db.sinhvien.Find(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.malopcn = new SelectList(db.lopcn, "malopcn", "magv", sinhvien.malopcn);
            return View(sinhvien);
        }

        // POST: Admin/sinhviens/Edit/5
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
            ViewBag.malopcn = new SelectList(db.lopcn, "malopcn", "magv", sinhvien.malopcn);
            return View(sinhvien);
        }

        // GET: Admin/sinhviens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sinhvien sinhvien = db.sinhvien.Find(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            return View(sinhvien);
        }

        // POST: Admin/sinhviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            sinhvien sinhvien = db.sinhvien.Find(id);
            db.sinhvien.Remove(sinhvien);
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
