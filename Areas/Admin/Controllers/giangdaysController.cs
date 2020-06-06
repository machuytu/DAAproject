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
    public class giangdaysController : Controller
    {
        private MyDatabaseEntities5 db = new MyDatabaseEntities5();

        // GET: Admin/giangdays
        public ActionResult Index()
        {
            var giangday = db.giangday.Include(g => g.lop).Include(g => g.sinhvien);
            return View(giangday.ToList());
        }

        // GET: Admin/giangdays/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangday giangday = db.giangday.Find(id);
            if (giangday == null)
            {
                return HttpNotFound();
            }
            return View(giangday);
        }

        // GET: Admin/giangdays/Create
        public ActionResult Create()
        {
            ViewBag.malop = new SelectList(db.lop, "malop", "magv");
            ViewBag.masv = new SelectList(db.sinhvien, "masv", "hoten");
            return View();
        }

        // POST: Admin/giangdays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "masv,malop,diemgk,diemck,diemtb")] giangday giangday)
        {
            if (ModelState.IsValid)
            {
                db.giangday.Add(giangday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.malop = new SelectList(db.lop, "malop", "magv", giangday.malop);
            ViewBag.masv = new SelectList(db.sinhvien, "masv", "hoten", giangday.masv);
            return View(giangday);
        }

        // GET: Admin/giangdays/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangday giangday = db.giangday.Find(id);
            if (giangday == null)
            {
                return HttpNotFound();
            }
            ViewBag.malop = new SelectList(db.lop, "malop", "magv", giangday.malop);
            ViewBag.masv = new SelectList(db.sinhvien, "masv", "hoten", giangday.masv);
            return View(giangday);
        }

        // POST: Admin/giangdays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "masv,malop,diemgk,diemck,diemtb")] giangday giangday)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giangday).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.malop = new SelectList(db.lop, "malop", "magv", giangday.malop);
            ViewBag.masv = new SelectList(db.sinhvien, "masv", "hoten", giangday.masv);
            return View(giangday);
        }

        // GET: Admin/giangdays/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangday giangday = db.giangday.Find(id);
            if (giangday == null)
            {
                return HttpNotFound();
            }
            return View(giangday);
        }

        // POST: Admin/giangdays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            giangday giangday = db.giangday.Find(id);
            db.giangday.Remove(giangday);
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
