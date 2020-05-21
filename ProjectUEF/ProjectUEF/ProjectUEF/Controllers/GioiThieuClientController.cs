using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectUEF.Models;

namespace ProjectUEF.Controllers
{
    public class GioiThieuClientController : Controller
    {
        private DoAnTotNghiepEntities db = new DoAnTotNghiepEntities();

        // GET: GioiThieuClient
        public ActionResult Index()
        {
            return View(db.GioiThieux.FirstOrDefault());
        }

        // GET: GioiThieuClient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioiThieu gioiThieu = db.GioiThieux.Find(id);
            if (gioiThieu == null)
            {
                return HttpNotFound();
            }
            return View(gioiThieu);
        }

        // GET: GioiThieuClient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GioiThieuClient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TieuDe,NgayTao,NgayChinhSua,NoiDung,HinhAnh")] GioiThieu gioiThieu)
        {
            if (ModelState.IsValid)
            {
                db.GioiThieux.Add(gioiThieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gioiThieu);
        }

        // GET: GioiThieuClient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioiThieu gioiThieu = db.GioiThieux.Find(id);
            if (gioiThieu == null)
            {
                return HttpNotFound();
            }
            return View(gioiThieu);
        }

        // POST: GioiThieuClient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TieuDe,NgayTao,NgayChinhSua,NoiDung,HinhAnh")] GioiThieu gioiThieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gioiThieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gioiThieu);
        }

        // GET: GioiThieuClient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioiThieu gioiThieu = db.GioiThieux.Find(id);
            if (gioiThieu == null)
            {
                return HttpNotFound();
            }
            return View(gioiThieu);
        }

        // POST: GioiThieuClient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GioiThieu gioiThieu = db.GioiThieux.Find(id);
            db.GioiThieux.Remove(gioiThieu);
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
