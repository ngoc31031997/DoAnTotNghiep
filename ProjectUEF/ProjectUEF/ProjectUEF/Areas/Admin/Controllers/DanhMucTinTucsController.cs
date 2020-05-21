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

namespace ProjectUEF.Areas.Admin.Controllers
{
    public class DanhMucTinTucsController : Controller
    {
        private DoAnTotNghiepEntities db = new DoAnTotNghiepEntities();

        // GET: Admin/DanhMucTinTucs
        public ActionResult Index(int? page, string searchString)
        {
            var data = (from s in db.DanhMucTinTucs select s);
            if(page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            int limit = 5;
            int start =(int) (page - 1) * limit;
            int total = data.Count();
            ViewBag.total = total;
            ViewBag.pageCurrent = page;
            float numPage = (float)total / limit;
            ViewBag.numPage = (int)Math.Ceiling(numPage);
            var dataDMTT = data.OrderByDescending(s => s.ID).Skip(start).Take(limit);
            if (!String.IsNullOrEmpty(searchString))
            {
                dataDMTT = data.OrderByDescending(s => s.ID).Skip(start).Take(limit).Where(a => a.TenDanhMuc.Contains(searchString));
            }
            return View(dataDMTT.ToList());
        }
        // GET: Admin/DanhMucTinTucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucTinTuc danhMucTinTuc = db.DanhMucTinTucs.Find(id);
            if (danhMucTinTuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMucTinTuc);
        }

        // GET: Admin/DanhMucTinTucs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DanhMucTinTucs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenDanhMuc")] DanhMucTinTuc danhMucTinTuc)
        {
            if (ModelState.IsValid)
            {
                db.DanhMucTinTucs.Add(danhMucTinTuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danhMucTinTuc);
        }

        // GET: Admin/DanhMucTinTucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucTinTuc danhMucTinTuc = db.DanhMucTinTucs.Find(id);
            if (danhMucTinTuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMucTinTuc);
        }

        // POST: Admin/DanhMucTinTucs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenDanhMuc")] DanhMucTinTuc danhMucTinTuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhMucTinTuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danhMucTinTuc);
        }

        // GET: Admin/DanhMucTinTucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucTinTuc danhMucTinTuc = db.DanhMucTinTucs.Find(id);
            if (danhMucTinTuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMucTinTuc);
        }

        // POST: Admin/DanhMucTinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DanhMucTinTuc danhMucTinTuc = db.DanhMucTinTucs.Find(id);
            db.DanhMucTinTucs.Remove(danhMucTinTuc);
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
