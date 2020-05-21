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
    public class CapDoDeTaisController : Controller
    {
        private DoAnTotNghiepEntities db = new DoAnTotNghiepEntities();
        // GET: Admin/CapDoDeTais
        public ActionResult Index(int? page, string searchString)
        {
            var data = (from s in db.CapDoDeTais select s);
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
                dataDMTT = data.OrderByDescending(s => s.ID).Skip(start).Take(limit).Where(a => a.CapDo.Contains(searchString));
            }
            return View(dataDMTT.ToList());
        }

        // GET: Admin/CapDoDeTais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CapDoDeTai capDoDeTai = db.CapDoDeTais.Find(id);
            if (capDoDeTai == null)
            {
                return HttpNotFound();
            }
            return View(capDoDeTai);
        }

        // GET: Admin/CapDoDeTais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CapDoDeTais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CapDo")] CapDoDeTai capDoDeTai)
        {
            if (ModelState.IsValid)
            {
                db.CapDoDeTais.Add(capDoDeTai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(capDoDeTai);
        }

        // GET: Admin/CapDoDeTais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CapDoDeTai capDoDeTai = db.CapDoDeTais.Find(id);
            if (capDoDeTai == null)
            {
                return HttpNotFound();
            }
            return View(capDoDeTai);
        }

        // POST: Admin/CapDoDeTais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CapDo")] CapDoDeTai capDoDeTai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(capDoDeTai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(capDoDeTai);
        }

        // GET: Admin/CapDoDeTais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CapDoDeTai capDoDeTai = db.CapDoDeTais.Find(id);
            if (capDoDeTai == null)
            {
                return HttpNotFound();
            }
            return View(capDoDeTai);
        }

        // POST: Admin/CapDoDeTais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CapDoDeTai capDoDeTai = db.CapDoDeTais.Find(id);
            db.CapDoDeTais.Remove(capDoDeTai);
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
