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
    public class CongTrinhKhoaHocClientController : Controller
    {
        private DoAnTotNghiepEntities db = new DoAnTotNghiepEntities();

        // GET: CongTrinhKhoaHocClient
        public ActionResult Index()
        {
            var congTrinhKhoaHocs = db.CongTrinhKhoaHocs.Include(c => c.LoaiCongTrinhKhoaHoc).Include(c => c.LoaiCongTrinhKhoaHoc1).Include(c => c.LyLichKhoaHoc);
            return View(congTrinhKhoaHocs.ToList());
        }

        // GET: CongTrinhKhoaHocClient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongTrinhKhoaHoc congTrinhKhoaHoc = db.CongTrinhKhoaHocs.Find(id);
            if (congTrinhKhoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(congTrinhKhoaHoc);
        }

        // GET: CongTrinhKhoaHocClient/Create
        public ActionResult Create()
        {
            ViewBag.LoaiCongTrinhKhoaHocID = new SelectList(db.LoaiCongTrinhKhoaHocs, "ID", "TenLoaiCongTrinhKhoaHoc");
            ViewBag.LoaiCongTrinhKhoaHocID = new SelectList(db.LoaiCongTrinhKhoaHocs, "ID", "TenLoaiCongTrinhKhoaHoc");
            ViewBag.LyLichKhoaHocID = new SelectList(db.LyLichKhoaHocs, "ID", "MaQuanLy");
            return View();
        }

        // POST: CongTrinhKhoaHocClient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LyLichKhoaHocID,TenSanPhamVN,TenSanPhamEN,TenTacGiaVN,TenTacGiaEN,TenTapChiVN,TenTapChiEN,SoHieuISSN,Nam,MinhChung,GhiChu,LoaiCongTrinhKhoaHocID,isDuyet,NgayDuyet,NguoiDuyet,NgayTao,NgayCapNhat")] CongTrinhKhoaHoc congTrinhKhoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.CongTrinhKhoaHocs.Add(congTrinhKhoaHoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiCongTrinhKhoaHocID = new SelectList(db.LoaiCongTrinhKhoaHocs, "ID", "TenLoaiCongTrinhKhoaHoc", congTrinhKhoaHoc.LoaiCongTrinhKhoaHocID);
            ViewBag.LoaiCongTrinhKhoaHocID = new SelectList(db.LoaiCongTrinhKhoaHocs, "ID", "TenLoaiCongTrinhKhoaHoc", congTrinhKhoaHoc.LoaiCongTrinhKhoaHocID);
            ViewBag.LyLichKhoaHocID = new SelectList(db.LyLichKhoaHocs, "ID", "MaQuanLy", congTrinhKhoaHoc.LyLichKhoaHocID);
            return View(congTrinhKhoaHoc);
        }

        // GET: CongTrinhKhoaHocClient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongTrinhKhoaHoc congTrinhKhoaHoc = db.CongTrinhKhoaHocs.Find(id);
            if (congTrinhKhoaHoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiCongTrinhKhoaHocID = new SelectList(db.LoaiCongTrinhKhoaHocs, "ID", "TenLoaiCongTrinhKhoaHoc", congTrinhKhoaHoc.LoaiCongTrinhKhoaHocID);
            ViewBag.LoaiCongTrinhKhoaHocID = new SelectList(db.LoaiCongTrinhKhoaHocs, "ID", "TenLoaiCongTrinhKhoaHoc", congTrinhKhoaHoc.LoaiCongTrinhKhoaHocID);
            ViewBag.LyLichKhoaHocID = new SelectList(db.LyLichKhoaHocs, "ID", "MaQuanLy", congTrinhKhoaHoc.LyLichKhoaHocID);
            return View(congTrinhKhoaHoc);
        }

        // POST: CongTrinhKhoaHocClient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LyLichKhoaHocID,TenSanPhamVN,TenSanPhamEN,TenTacGiaVN,TenTacGiaEN,TenTapChiVN,TenTapChiEN,SoHieuISSN,Nam,MinhChung,GhiChu,LoaiCongTrinhKhoaHocID,isDuyet,NgayDuyet,NguoiDuyet,NgayTao,NgayCapNhat")] CongTrinhKhoaHoc congTrinhKhoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(congTrinhKhoaHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiCongTrinhKhoaHocID = new SelectList(db.LoaiCongTrinhKhoaHocs, "ID", "TenLoaiCongTrinhKhoaHoc", congTrinhKhoaHoc.LoaiCongTrinhKhoaHocID);
            ViewBag.LoaiCongTrinhKhoaHocID = new SelectList(db.LoaiCongTrinhKhoaHocs, "ID", "TenLoaiCongTrinhKhoaHoc", congTrinhKhoaHoc.LoaiCongTrinhKhoaHocID);
            ViewBag.LyLichKhoaHocID = new SelectList(db.LyLichKhoaHocs, "ID", "MaQuanLy", congTrinhKhoaHoc.LyLichKhoaHocID);
            return View(congTrinhKhoaHoc);
        }

        // GET: CongTrinhKhoaHocClient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CongTrinhKhoaHoc congTrinhKhoaHoc = db.CongTrinhKhoaHocs.Find(id);
            if (congTrinhKhoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(congTrinhKhoaHoc);
        }

        // POST: CongTrinhKhoaHocClient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CongTrinhKhoaHoc congTrinhKhoaHoc = db.CongTrinhKhoaHocs.Find(id);
            db.CongTrinhKhoaHocs.Remove(congTrinhKhoaHoc);
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
