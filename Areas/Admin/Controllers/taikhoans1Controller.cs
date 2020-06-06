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
    public class taikhoans1Controller : Controller
    {
        private MyDatabaseEntities8 db = new MyDatabaseEntities8();

        // GET: Admin/taikhoans1
        public ActionResult Index()
        {
            return View(db.taikhoans.ToList());
        }

        // GET: Admin/taikhoans1/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            taikhoan taikhoan = db.taikhoans.Find(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan);
        }

        // GET: Admin/taikhoans1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/taikhoans1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "matk,password1,password2,nhom")] taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.taikhoans.Add(taikhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taikhoan);
        }

        // GET: Admin/taikhoans1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            taikhoan taikhoan = db.taikhoans.Find(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan);
        }

        // POST: Admin/taikhoans1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "matk,password1,password2,nhom")] taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taikhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taikhoan);
        }

        // GET: Admin/taikhoans1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            taikhoan taikhoan = db.taikhoans.Find(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan);
        }

        // POST: Admin/taikhoans1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            taikhoan taikhoan = db.taikhoans.Find(id);
            db.taikhoans.Remove(taikhoan);
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
