﻿using System;
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
    public class GioiThieuxController : Controller
    {
        private DoAnTotNghiepEntities db = new DoAnTotNghiepEntities();
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
        // GET: Admin/GioiThieu
        public ActionResult Index(int? page, string searchString)
        {
            var data = (from s in db.GioiThieux select s);
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
                dataDMTT = data.OrderByDescending(s => s.ID).Skip(start).Take(limit).Where(a => a.TieuDe.Contains(searchString));
            }
            return View(dataDMTT.ToList());
        }

        // GET: Admin/GioiThieu/Details/5
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

        // GET: Admin/GioiThieu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/GioiThieu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TieuDe,NgayTao,NgayChinhSua,NoiDung,HinhAnh")] GioiThieu gioiThieu, HttpPostedFileBase Image)
        {
            try
            {
                if (Image != null)
                {
                    gioiThieu.HinhAnh = UploadCreateDir(Image);
                }
                else
                {
                    return Redirect("/Admin/GioiThieux/Index");
                }
                db.GioiThieux.Add(gioiThieu);
                db.SaveChanges();
                return Redirect("/Admin/GioiThieux/Index");
            }
            catch
            {
                return View(gioiThieu);
            }
        }

        // GET: Admin/GioiThieu/Edit/5
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

        // POST: Admin/GioiThieu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TieuDe,NgayTao,NgayChinhSua,NoiDung,HinhAnh")] GioiThieu gioiThieu, HttpPostedFileBase Image)
        {
            try
            {
                if (Image != null)
                {
                    gioiThieu.HinhAnh = UploadCreateDir(Image);

                }
                db.Entry(gioiThieu).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Admin/GioiThieux/Index");
            }
            catch
            {
                return View(gioiThieu);
            }

        }

        // GET: Admin/GioiThieu/Delete/5
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

        // POST: Admin/GioiThieu/Delete/5
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
