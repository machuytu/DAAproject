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
    public class giangviens1Controller : Controller
    {
        private MyDatabaseEntities8 db = new MyDatabaseEntities8();

        // GET: Admin/giangviens1
        public ActionResult Index()
        {
            return View(db.giangviens.ToList());
        }

        // GET: Admin/giangviens1/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangvien giangvien = db.giangviens.Find(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            return View(giangvien);
        }

        // GET: Admin/giangviens1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/giangviens1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "magv,hoten,gioitinh,ngaysinh,diachi,quequan,sdt,capbac,email,ngayvaolam")] giangvien giangvien)
        {
            if (ModelState.IsValid)
            {
                db.giangviens.Add(giangvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(giangvien);
        }

        // GET: Admin/giangviens1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangvien giangvien = db.giangviens.Find(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            return View(giangvien);
        }

        // POST: Admin/giangviens1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "magv,hoten,gioitinh,ngaysinh,diachi,quequan,sdt,capbac,email,ngayvaolam")] giangvien giangvien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giangvien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(giangvien);
        }

        // GET: Admin/giangviens1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangvien giangvien = db.giangviens.Find(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            return View(giangvien);
        }

        // POST: Admin/giangviens1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            giangvien giangvien = db.giangviens.Find(id);
            db.giangviens.Remove(giangvien);
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
