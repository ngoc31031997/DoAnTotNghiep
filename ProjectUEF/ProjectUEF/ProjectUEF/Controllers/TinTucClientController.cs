using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectUEF.Models;
using PagedList;

namespace ProjectUEF.Controllers
{
    public class TinTucClientController : Controller
    {
        private DoAnTotNghiepEntities db = new DoAnTotNghiepEntities();

        // GET: TinTucClient
        public ActionResult Index()
        {
            var tinTucs = db.TinTucs.Include(t => t.DanhMucTinTuc);
            return View(tinTucs.ToList());
        }
        
        // GET: TinTucClient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: TinTucClient/Create
        public ActionResult Create()
        {
            ViewBag.DanhMucTinTucID = new SelectList(db.DanhMucTinTucs, "ID", "TenDanhMuc");
            return View();
        }

        // POST: TinTucClient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DanhMucTinTucID,TieuDe,MoTa,NoiDung,NgayTao,NgayChinhSua,HinhAnh")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                db.TinTucs.Add(tinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DanhMucTinTucID = new SelectList(db.DanhMucTinTucs, "ID", "TenDanhMuc", tinTuc.DanhMucTinTucID);
            return View(tinTuc);
        }

        // GET: TinTucClient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.DanhMucTinTucID = new SelectList(db.DanhMucTinTucs, "ID", "TenDanhMuc", tinTuc.DanhMucTinTucID);
            return View(tinTuc);
        }

        // POST: TinTucClient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DanhMucTinTucID,TieuDe,MoTa,NoiDung,NgayTao,NgayChinhSua,HinhAnh")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DanhMucTinTucID = new SelectList(db.DanhMucTinTucs, "ID", "TenDanhMuc", tinTuc.DanhMucTinTucID);
            return View(tinTuc);
        }

        // GET: TinTucClient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: TinTucClient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TinTuc tinTuc = db.TinTucs.Find(id);
            db.TinTucs.Remove(tinTuc);
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
