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
    public class giangviensController : Controller
    {
        private MyDatabaseEntities5 db = new MyDatabaseEntities5();

        // GET: Admin/giangviens
        public ActionResult Index()
        {
            return View(db.giangvien.ToList());
        }

        // GET: Admin/giangviens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangvien giangvien = db.giangvien.Find(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            return View(giangvien);
        }

        // GET: Admin/giangviens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/giangviens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "magv,hoten,gioitinh,ngaysinh,diachi,quequan,sdt,capbac,email,ngayvaolam")] giangvien giangvien)
        {
            if (ModelState.IsValid)
            {
                db.giangvien.Add(giangvien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(giangvien);
        }

        // GET: Admin/giangviens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangvien giangvien = db.giangvien.Find(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            return View(giangvien);
        }

        // POST: Admin/giangviens/Edit/5
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

        // GET: Admin/giangviens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangvien giangvien = db.giangvien.Find(id);
            if (giangvien == null)
            {
                return HttpNotFound();
            }
            return View(giangvien);
        }

        // POST: Admin/giangviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            giangvien giangvien = db.giangvien.Find(id);
            db.giangvien.Remove(giangvien);
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
