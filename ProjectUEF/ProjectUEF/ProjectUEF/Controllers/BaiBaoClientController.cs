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
    public class BaiBaoClientController : Controller
    {
        private DoAnTotNghiepEntities db = new DoAnTotNghiepEntities();

        // GET: BaiBaoClient
        public ActionResult Index(int? page, string searchString)
        {
                var data = (from s in db.BaiBaos select s);
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
                    dataDMTT = data.OrderByDescending(s => s.ID).Skip(start).Take(limit).Where(a => a.TenBaiBao.Contains(searchString));
                }
                return View(dataDMTT.ToList());
        }

        // GET: BaiBaoClient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiBao baiBao = db.BaiBaos.Find(id);
            if (baiBao == null)
            {
                return HttpNotFound();
            }
            return View(baiBao);
        }

        // GET: BaiBaoClient/Create
        public ActionResult Create()
        {
            ViewBag.CapDoDeTaiID = new SelectList(db.CapDoDeTais, "ID", "CapDo");
            ViewBag.DanhMucBaiBaoID = new SelectList(db.DanhMucBaiBaos, "ID", "TenDanhMuc");
            ViewBag.LoaiNghienCuuID = new SelectList(db.LoaiNghienCuus, "ID", "TenLoaiNghienCuu");
            ViewBag.LyLichKhoaHocID = new SelectList(db.LyLichKhoaHocs, "ID", "MaQuanLy");
            ViewBag.TongDiem = new SelectList(db.TongDiems, "ID", "ID");
            ViewBag.VaiTroID = new SelectList(db.VaiTroes, "ID", "TenVaiTro");
            return View();
        }

        // POST: BaiBaoClient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DonVi,GhiChu,TenBaiBao,TenTapChi,ThoiGianSanXuat,ChiSoISSNVaISBN,ThuocDanhMuc,SoLuongTacGia,QuyDoiTieuChuan,TongDiem,DanhMucBaiBaoID,VaiTroID,CapDoDeTaiID,LoaiNghienCuuID,LyLichKhoaHocID")] BaiBao baiBao)
        {
            if (ModelState.IsValid)
            {
                db.BaiBaos.Add(baiBao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CapDoDeTaiID = new SelectList(db.CapDoDeTais, "ID", "CapDo", baiBao.CapDoDeTaiID);
            ViewBag.DanhMucBaiBaoID = new SelectList(db.DanhMucBaiBaos, "ID", "TenDanhMuc", baiBao.DanhMucBaiBaoID);
            ViewBag.LoaiNghienCuuID = new SelectList(db.LoaiNghienCuus, "ID", "TenLoaiNghienCuu", baiBao.LoaiNghienCuuID);
            ViewBag.LyLichKhoaHocID = new SelectList(db.LyLichKhoaHocs, "ID", "MaQuanLy", baiBao.LyLichKhoaHocID);
            ViewBag.TongDiem = new SelectList(db.TongDiems, "ID", "ID", baiBao.TongDiem);
            ViewBag.VaiTroID = new SelectList(db.VaiTroes, "ID", "TenVaiTro", baiBao.VaiTroID);
            return View(baiBao);
        }

        // GET: BaiBaoClient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiBao baiBao = db.BaiBaos.Find(id);
            if (baiBao == null)
            {
                return HttpNotFound();
            }
            ViewBag.CapDoDeTaiID = new SelectList(db.CapDoDeTais, "ID", "CapDo", baiBao.CapDoDeTaiID);
            ViewBag.DanhMucBaiBaoID = new SelectList(db.DanhMucBaiBaos, "ID", "TenDanhMuc", baiBao.DanhMucBaiBaoID);
            ViewBag.LoaiNghienCuuID = new SelectList(db.LoaiNghienCuus, "ID", "TenLoaiNghienCuu", baiBao.LoaiNghienCuuID);
            ViewBag.LyLichKhoaHocID = new SelectList(db.LyLichKhoaHocs, "ID", "MaQuanLy", baiBao.LyLichKhoaHocID);
            ViewBag.TongDiem = new SelectList(db.TongDiems, "ID", "ID", baiBao.TongDiem);
            ViewBag.VaiTroID = new SelectList(db.VaiTroes, "ID", "TenVaiTro", baiBao.VaiTroID);
            return View(baiBao);
        }

        // POST: BaiBaoClient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DonVi,GhiChu,TenBaiBao,TenTapChi,ThoiGianSanXuat,ChiSoISSNVaISBN,ThuocDanhMuc,SoLuongTacGia,QuyDoiTieuChuan,TongDiem,DanhMucBaiBaoID,VaiTroID,CapDoDeTaiID,LoaiNghienCuuID,LyLichKhoaHocID")] BaiBao baiBao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baiBao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CapDoDeTaiID = new SelectList(db.CapDoDeTais, "ID", "CapDo", baiBao.CapDoDeTaiID);
            ViewBag.DanhMucBaiBaoID = new SelectList(db.DanhMucBaiBaos, "ID", "TenDanhMuc", baiBao.DanhMucBaiBaoID);
            ViewBag.LoaiNghienCuuID = new SelectList(db.LoaiNghienCuus, "ID", "TenLoaiNghienCuu", baiBao.LoaiNghienCuuID);
            ViewBag.LyLichKhoaHocID = new SelectList(db.LyLichKhoaHocs, "ID", "MaQuanLy", baiBao.LyLichKhoaHocID);
            ViewBag.TongDiem = new SelectList(db.TongDiems, "ID", "ID", baiBao.TongDiem);
            ViewBag.VaiTroID = new SelectList(db.VaiTroes, "ID", "TenVaiTro", baiBao.VaiTroID);
            return View(baiBao);
        }

        // GET: BaiBaoClient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiBao baiBao = db.BaiBaos.Find(id);
            if (baiBao == null)
            {
                return HttpNotFound();
            }
            return View(baiBao);
        }

        // POST: BaiBaoClient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiBao baiBao = db.BaiBaos.Find(id);
            db.BaiBaos.Remove(baiBao);
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
