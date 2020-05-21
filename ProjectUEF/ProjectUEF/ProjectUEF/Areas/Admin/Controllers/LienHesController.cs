using System;
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
    public class LienHesController : Controller
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
        // GET: Admin/LienHes
        public ActionResult Index(int? page, string searchString)
        {
            var data = (from s in db.LienHes select s);
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
                dataDMTT = data.OrderByDescending(s => s.ID).Skip(start).Take(limit).Where(a => a.HoVaTen.Contains(searchString));
            }
            return View(dataDMTT.ToList());
        }

        // GET: Admin/LienHes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienHe lienHe = db.LienHes.Find(id);
            if (lienHe == null)
            {
                return HttpNotFound();
            }
            return View(lienHe);
        }

        // GET: Admin/LienHes/Create
        public ActionResult Create()
        {
            ViewBag.KhoaID = new SelectList(db.Khoas, "ID", "TenKhoa");
            return View();
        }

        // POST: Admin/LienHes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HoVaTen,Email,SDT,KhoaID,NoiDung,ThongTin")] LienHe lienHe)
        {
            if (ModelState.IsValid)
            {
                db.LienHes.Add(lienHe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KhoaID = new SelectList(db.Khoas, "ID", "TenKhoa", lienHe.KhoaID);
            return View(lienHe);
        }

        // GET: Admin/LienHes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienHe lienHe = db.LienHes.Find(id);
            if (lienHe == null)
            {
                return HttpNotFound();
            }
            ViewBag.KhoaID = new SelectList(db.Khoas, "ID", "TenKhoa", lienHe.KhoaID);
            return View(lienHe);
        }

        // POST: Admin/LienHes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,HoVaTen,Email,SDT,KhoaID,NoiDung,ThongTin")] LienHe lienHe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lienHe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KhoaID = new SelectList(db.Khoas, "ID", "TenKhoa", lienHe.KhoaID);
            return View(lienHe);
        }

        // GET: Admin/LienHes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienHe lienHe = db.LienHes.Find(id);
            if (lienHe == null)
            {
                return HttpNotFound();
            }
            return View(lienHe);
        }

        // POST: Admin/LienHes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LienHe lienHe = db.LienHes.Find(id);
            db.LienHes.Remove(lienHe);
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
