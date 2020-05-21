using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImageResizer;
using ProjectUEF.Models;

namespace ProjectUEF.Areas.Admin.Controllers
{
    public class CongTrinhKhoaHocsController : Controller
    {
        private DoAnTotNghiepEntities db = new DoAnTotNghiepEntities();
        //Upload image
        private string pathFile = "/FILEs/GioiThieux/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/Images/";
        private string fileName = "";
        public string UploadCreateDir(HttpPostedFileBase upload)
        {
            var versions = new Dictionary<string, string>();
            versions.Add("Cat", "width=1500&height=600&format=jpeg&quality=80");
            fileName = Path.GetFileName(upload.FileName);

            bool exsits = System.IO.Directory.Exists(Server.MapPath(pathFile));
            if (!exsits)
                System.IO.Directory.CreateDirectory(Server.MapPath(pathFile));
            var path = Path.Combine(Server.MapPath(pathFile), fileName);
            upload.SaveAs(path);
            ImageBuilder.Current.Build(path, path, new ResizeSettings(versions["Cat"]));
            return pathFile + fileName;
        }
        // GET: Admin/CongTrinhKhoaHocs
        public ActionResult Index(int? page, string searchString)
        {
            var data = (from s in db.CongTrinhKhoaHocs select s);
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            int limit = 5;
            int start = (int)(page - 1) * limit;
            int total = data.Count();
            ViewBag.total = total;
            ViewBag.pageCurrent = page;
            float numPage = (float)total / limit;
            ViewBag.numPage = (int)Math.Ceiling(numPage);
            var dataDMTT = data.OrderByDescending(s => s.ID).Skip(start).Take(limit);
            if (!String.IsNullOrEmpty(searchString))
            {
                dataDMTT = data.OrderByDescending(s => s.ID).Skip(start).Take(limit).Where(a => a.TenSanPhamVN.Contains(searchString));
            }
            return View(dataDMTT.ToList());
        }

        // GET: Admin/CongTrinhKhoaHocs/Details/5
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

        // GET: Admin/CongTrinhKhoaHocs/Create
        public ActionResult Create()
        {
            ViewBag.LoaiCongTrinhKhoaHocID = new SelectList(db.LoaiCongTrinhKhoaHocs, "ID", "TenLoaiCongTrinhKhoaHoc");
            ViewBag.LoaiCongTrinhKhoaHocID = new SelectList(db.LoaiCongTrinhKhoaHocs, "ID", "TenLoaiCongTrinhKhoaHoc");
            ViewBag.LyLichKhoaHocID = new SelectList(db.LyLichKhoaHocs, "ID", "MaQuanLy");
            return View();
        }

        // POST: Admin/CongTrinhKhoaHocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LyLichKhoaHocID,TenSanPhamVN,TenSanPhamEN,TenTacGiaVN,TenTacGiaEN,TenTapChiVN,TenTapChiEN,SoHieuISSN,Nam,MinhChung,GhiChu,LoaiCongTrinhKhoaHocID,isDuyet,NgayDuyet,NguoiDuyet,NgayTao,NgayCapNhat")] CongTrinhKhoaHoc congTrinhKhoaHoc, HttpPostedFileBase Image)
        {
            try
            {
                if (Image != null)
                {
                    congTrinhKhoaHoc.MinhChung = UploadCreateDir(Image);
                }
                else
                {
                    return Redirect("/Admin/CongTrinhKhoaHocs/Index");
                }
                db.CongTrinhKhoaHocs.Add(congTrinhKhoaHoc);
                db.SaveChanges();
                return Redirect("/Admin/CongTrinhKhoaHocs/Index");
            }
            catch
            {
                ViewBag.LoaiCongTrinhKhoaHocID = new SelectList(db.LoaiCongTrinhKhoaHocs, "ID", "TenLoaiCongTrinhKhoaHoc", congTrinhKhoaHoc.LoaiCongTrinhKhoaHocID);
                ViewBag.LoaiCongTrinhKhoaHocID = new SelectList(db.LoaiCongTrinhKhoaHocs, "ID", "TenLoaiCongTrinhKhoaHoc", congTrinhKhoaHoc.LoaiCongTrinhKhoaHocID);
                ViewBag.LyLichKhoaHocID = new SelectList(db.LyLichKhoaHocs, "ID", "MaQuanLy", congTrinhKhoaHoc.LyLichKhoaHocID);
                return View(congTrinhKhoaHoc);
            }
        }

        // GET: Admin/CongTrinhKhoaHocs/Edit/5
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

        // POST: Admin/CongTrinhKhoaHocs/Edit/5
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

        // GET: Admin/CongTrinhKhoaHocs/Delete/5
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

        // POST: Admin/CongTrinhKhoaHocs/Delete/5
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
