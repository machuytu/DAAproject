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
    public class lopcnsController : AuthController
    {
        private MyDatabaseEntities9 db = new MyDatabaseEntities9();

        // GET: Admin/lopcns
        public async Task<ActionResult> Index()
        {
            var lopcns = db.lopcns.Include(l => l.giangvien).Include(l => l.khoa);
            return View(await lopcns.ToListAsync());
        }

        // GET: Admin/lopcns/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lopcn lopcn = await db.lopcns.FindAsync(id);
            if (lopcn == null)
            {
                return HttpNotFound();
            }
            return View(lopcn);
        }

        // GET: Admin/lopcns/Create
        public ActionResult Create()
        {
            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "tenvama");
            ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenvama");
            return View();
        }

        // POST: Admin/lopcns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idlopcn,malopcn,idgv,idkhoa,nienkhoa")] lopcn lopcn)
        {
            if (ModelState.IsValid)
            {
                db.lopcns.Add(lopcn);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "tenvama", lopcn.idgv);
            ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenvama", lopcn.idkhoa);
            return View(lopcn);
        }

        // GET: Admin/lopcns/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lopcn lopcn = await db.lopcns.FindAsync(id);
            if (lopcn == null)
            {
                return HttpNotFound();
            }
            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "hoten", lopcn.idgv);
            ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenkhoa", lopcn.idkhoa);
            return View(lopcn);
        }

        // POST: Admin/lopcns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idlopcn,malopcn,idgv,idkhoa,nienkhoa")] lopcn lopcn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lopcn).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idgv = new SelectList(db.giangviens, "idgv", "hoten", lopcn.idgv);
            ViewBag.idkhoa = new SelectList(db.khoas, "idkhoa", "tenkhoa", lopcn.idkhoa);
            return View(lopcn);
        }

        // GET: Admin/lopcns/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lopcn lopcn = await db.lopcns.FindAsync(id);
            if (lopcn == null)
            {
                return HttpNotFound();
            }
            return View(lopcn);
        }

        // POST: Admin/lopcns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            lopcn lopcn = await db.lopcns.FindAsync(id);
            db.lopcns.Remove(lopcn);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Deleteuser(int id)
        {
            try
            {
                lopcn lopcn = await db.lopcns.FindAsync(id);
                db.lopcns.Remove(lopcn);
                await db.SaveChangesAsync();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetGVList (int idkhoa)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<giangvien> rs = db.giangviens.Where(x => x.idkhoa == idkhoa).ToList();
            return Json(rs,JsonRequestBehavior.AllowGet);
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
