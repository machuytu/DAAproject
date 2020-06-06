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
    public class giangdays1Controller : Controller
    {
        private MyDatabaseEntities8 db = new MyDatabaseEntities8();

        // GET: Admin/giangdays1
        public ActionResult Index()
        {
            var giangdays = db.giangdays.Include(g => g.lop).Include(g => g.sinhvien);
            return View(giangdays.ToList());
        }

        // GET: Admin/giangdays1/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangday giangday = db.giangdays.Find(id);
            if (giangday == null)
            {
                return HttpNotFound();
            }
            return View(giangday);
        }

        // GET: Admin/giangdays1/Create
        public ActionResult Create()
        {
            ViewBag.malop = new SelectList(db.lops, "malop", "magv");
            ViewBag.masv = new SelectList(db.sinhviens, "masv", "hoten");
            return View();
        }

        // POST: Admin/giangdays1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "masv,malop,diemgk,diemck,diemtb")] giangday giangday)
        {
            if (ModelState.IsValid)
            {
                db.giangdays.Add(giangday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.malop = new SelectList(db.lops, "malop", "magv", giangday.malop);
            ViewBag.masv = new SelectList(db.sinhviens, "masv", "hoten", giangday.masv);
            return View(giangday);
        }

        // GET: Admin/giangdays1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangday giangday = db.giangdays.Find(id);
            if (giangday == null)
            {
                return HttpNotFound();
            }
            ViewBag.malop = new SelectList(db.lops, "malop", "magv", giangday.malop);
            ViewBag.masv = new SelectList(db.sinhviens, "masv", "hoten", giangday.masv);
            return View(giangday);
        }

        // POST: Admin/giangdays1/Edit/5
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
            ViewBag.malop = new SelectList(db.lops, "malop", "magv", giangday.malop);
            ViewBag.masv = new SelectList(db.sinhviens, "masv", "hoten", giangday.masv);
            return View(giangday);
        }

        // GET: Admin/giangdays1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            giangday giangday = db.giangdays.Find(id);
            if (giangday == null)
            {
                return HttpNotFound();
            }
            return View(giangday);
        }

        // POST: Admin/giangdays1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            giangday giangday = db.giangdays.Find(id);
            db.giangdays.Remove(giangday);
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
