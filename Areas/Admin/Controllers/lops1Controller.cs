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
    public class lops1Controller : Controller
    {
        private MyDatabaseEntities8 db = new MyDatabaseEntities8();

        // GET: Admin/lops1
        public ActionResult Index()
        {
            var lops = db.lops.Include(l => l.dangkyhocphan).Include(l => l.giangvien).Include(l => l.mon);
            return View(lops.ToList());
        }

        // GET: Admin/lops1/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lop lop = db.lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // GET: Admin/lops1/Create
        public ActionResult Create()
        {
            ViewBag.madkhp = new SelectList(db.dangkyhocphans, "madkhp", "namhoc");
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten");
            ViewBag.mamon = new SelectList(db.mons, "mamon", "tenmon");
            return View();
        }

        // POST: Admin/lops1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "malop,magv,mamon,thu,tietbd,tietkt,madkhp")] lop lop)
        {
            if (ModelState.IsValid)
            {
                db.lops.Add(lop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.madkhp = new SelectList(db.dangkyhocphans, "madkhp", "namhoc", lop.madkhp);
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", lop.magv);
            ViewBag.mamon = new SelectList(db.mons, "mamon", "tenmon", lop.mamon);
            return View(lop);
        }

        // GET: Admin/lops1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lop lop = db.lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            ViewBag.madkhp = new SelectList(db.dangkyhocphans, "madkhp", "namhoc", lop.madkhp);
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", lop.magv);
            ViewBag.mamon = new SelectList(db.mons, "mamon", "tenmon", lop.mamon);
            return View(lop);
        }

        // POST: Admin/lops1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "malop,magv,mamon,thu,tietbd,tietkt,madkhp")] lop lop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.madkhp = new SelectList(db.dangkyhocphans, "madkhp", "namhoc", lop.madkhp);
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", lop.magv);
            ViewBag.mamon = new SelectList(db.mons, "mamon", "tenmon", lop.mamon);
            return View(lop);
        }

        // GET: Admin/lops1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lop lop = db.lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // POST: Admin/lops1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            lop lop = db.lops.Find(id);
            db.lops.Remove(lop);
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
