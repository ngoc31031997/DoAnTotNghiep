using ProjectUEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectUEF.Controllers
{
    public class LienHeClientController : Controller
    {
        private DoAnTotNghiepEntities db = new DoAnTotNghiepEntities();
        // GET: LienHeClient
        public ActionResult Create()
        {
            ViewBag.KhoaID = new SelectList(db.Khoas, "ID", "TenKhoa");
            return View();
        }
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
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            
            return View();
        }
    }
}