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
    public class lopcns1Controller : Controller
    {
        private MyDatabaseEntities8 db = new MyDatabaseEntities8();

        // GET: Admin/lopcns1
        public ActionResult Index()
        {
            var lopcns = db.lopcns.Include(l => l.giangvien);
            return View(lopcns.ToList());
        }

        // GET: Admin/lopcns1/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lopcn lopcn = db.lopcns.Find(id);
            if (lopcn == null)
            {
                return HttpNotFound();
            }
            return View(lopcn);
        }

        // GET: Admin/lopcns1/Create
        public ActionResult Create()
        {
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten");
            return View();
        }

        // POST: Admin/lopcns1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "malopcn,magv,nienkhoa")] lopcn lopcn)
        {
            if (ModelState.IsValid)
            {
                db.lopcns.Add(lopcn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", lopcn.magv);
            return View(lopcn);
        }

        // GET: Admin/lopcns1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lopcn lopcn = db.lopcns.Find(id);
            if (lopcn == null)
            {
                return HttpNotFound();
            }
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", lopcn.magv);
            return View(lopcn);
        }

        // POST: Admin/lopcns1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "malopcn,magv,nienkhoa")] lopcn lopcn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lopcn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.magv = new SelectList(db.giangviens, "magv", "hoten", lopcn.magv);
            return View(lopcn);
        }

        // GET: Admin/lopcns1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lopcn lopcn = db.lopcns.Find(id);
            if (lopcn == null)
            {
                return HttpNotFound();
            }
            return View(lopcn);
        }

        // POST: Admin/lopcns1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            lopcn lopcn = db.lopcns.Find(id);
            db.lopcns.Remove(lopcn);
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
