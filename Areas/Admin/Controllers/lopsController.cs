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
    public class lopsController : Controller
    {
        private MyDatabaseEntities5 db = new MyDatabaseEntities5();

        // GET: Admin/lops
        public ActionResult Index()
        {
            var lop = db.lop.Include(l => l.giangvien).Include(l => l.mon);
            return View(lop.ToList());
        }

        // GET: Admin/lops/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lop lop = db.lop.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // GET: Admin/lops/Create
        public ActionResult Create()
        {
            ViewBag.magv = new SelectList(db.giangvien, "magv", "hoten");
            ViewBag.mamon = new SelectList(db.mon, "mamon", "tenmon");
            return View();
        }

        // POST: Admin/lops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "malop,magv,mamon,thu,tietbd,tietkt,hocky,namhoc")] lop lop)
        {
            if (ModelState.IsValid)
            {
                db.lop.Add(lop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.magv = new SelectList(db.giangvien, "magv", "hoten", lop.magv);
            ViewBag.mamon = new SelectList(db.mon, "mamon", "tenmon", lop.mamon);
            return View(lop);
        }

        // GET: Admin/lops/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lop lop = db.lop.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            ViewBag.magv = new SelectList(db.giangvien, "magv", "hoten", lop.magv);
            ViewBag.mamon = new SelectList(db.mon, "mamon", "tenmon", lop.mamon);
            return View(lop);
        }

        // POST: Admin/lops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "malop,magv,mamon,thu,tietbd,tietkt,hocky,namhoc")] lop lop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.magv = new SelectList(db.giangvien, "magv", "hoten", lop.magv);
            ViewBag.mamon = new SelectList(db.mon, "mamon", "tenmon", lop.mamon);
            return View(lop);
        }

        // GET: Admin/lops/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lop lop = db.lop.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // POST: Admin/lops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            lop lop = db.lop.Find(id);
            db.lop.Remove(lop);
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
