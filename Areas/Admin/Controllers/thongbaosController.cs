using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectDAA1;

namespace ProjectDAA1.Areas.Admin.Controllers
{
    public class thongbaosController : Controller
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/thongbaos
        public async Task<ActionResult> Index()
        {
            var thongbaos = db.thongbaos.Include(t => t.taikhoan);
            return View(await thongbaos.ToListAsync());
        }

        // GET: Admin/thongbaos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thongbao thongbao = await db.thongbaos.FindAsync(id);
            if (thongbao == null)
            {
                return HttpNotFound();
            }
            return View(thongbao);
        }

        // GET: Admin/thongbaos/Create
        public ActionResult Create()
        {
            ViewBag.matk = new SelectList(db.taikhoans, "matk", "password");
            return View();
        }

        // POST: Admin/thongbaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "matb,matk,tag,noidung,tieude,thoigiandang")] thongbao thongbao)
        {
            if (ModelState.IsValid)
            {
                db.thongbaos.Add(thongbao);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.matk = new SelectList(db.taikhoans, "matk", "password", thongbao.matk);
            return View(thongbao);
        }

        // GET: Admin/thongbaos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thongbao thongbao = await db.thongbaos.FindAsync(id);
            if (thongbao == null)
            {
                return HttpNotFound();
            }
            ViewBag.matk = new SelectList(db.taikhoans, "matk", "password", thongbao.matk);
            return View(thongbao);
        }

        // POST: Admin/thongbaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "matb,matk,tag,noidung,tieude,thoigiandang")] thongbao thongbao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongbao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.matk = new SelectList(db.taikhoans, "matk", "password", thongbao.matk);
            return View(thongbao);
        }

        // GET: Admin/thongbaos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thongbao thongbao = await db.thongbaos.FindAsync(id);
            if (thongbao == null)
            {
                return HttpNotFound();
            }
            return View(thongbao);
        }

        // POST: Admin/thongbaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            thongbao thongbao = await db.thongbaos.FindAsync(id);
            db.thongbaos.Remove(thongbao);
            await db.SaveChangesAsync();
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
