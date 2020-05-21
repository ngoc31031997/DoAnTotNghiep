﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectUEF.Models;
using System.IO;
using ImageResizer;

namespace ProjectUEF.Areas.Admin.Controllers
{
    public class LoaiCongTrinhKhoaHocsController : Controller
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
        // GET: Admin/LoaiCongTrinhKhoaHocs
        public ActionResult Index(int? page, string searchString)
        {
            var data = (from s in db.LoaiCongTrinhKhoaHocs select s);
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
                dataDMTT = data.OrderByDescending(s => s.ID).Skip(start).Take(limit).Where(a => a.TenLoaiCongTrinhKhoaHoc.Contains(searchString));
            }
            return View(dataDMTT.ToList());
        }

        // GET: Admin/LoaiCongTrinhKhoaHocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiCongTrinhKhoaHoc loaiCongTrinhKhoaHoc = db.LoaiCongTrinhKhoaHocs.Find(id);
            if (loaiCongTrinhKhoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(loaiCongTrinhKhoaHoc);
        }

        // GET: Admin/LoaiCongTrinhKhoaHocs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiCongTrinhKhoaHocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenLoaiCongTrinhKhoaHoc")] LoaiCongTrinhKhoaHoc loaiCongTrinhKhoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.LoaiCongTrinhKhoaHocs.Add(loaiCongTrinhKhoaHoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiCongTrinhKhoaHoc);
        }

        // GET: Admin/LoaiCongTrinhKhoaHocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiCongTrinhKhoaHoc loaiCongTrinhKhoaHoc = db.LoaiCongTrinhKhoaHocs.Find(id);
            if (loaiCongTrinhKhoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(loaiCongTrinhKhoaHoc);
        }

        // POST: Admin/LoaiCongTrinhKhoaHocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenLoaiCongTrinhKhoaHoc")] LoaiCongTrinhKhoaHoc loaiCongTrinhKhoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiCongTrinhKhoaHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiCongTrinhKhoaHoc);
        }

        // GET: Admin/LoaiCongTrinhKhoaHocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiCongTrinhKhoaHoc loaiCongTrinhKhoaHoc = db.LoaiCongTrinhKhoaHocs.Find(id);
            if (loaiCongTrinhKhoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(loaiCongTrinhKhoaHoc);
        }

        // POST: Admin/LoaiCongTrinhKhoaHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiCongTrinhKhoaHoc loaiCongTrinhKhoaHoc = db.LoaiCongTrinhKhoaHocs.Find(id);
            db.LoaiCongTrinhKhoaHocs.Remove(loaiCongTrinhKhoaHoc);
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
