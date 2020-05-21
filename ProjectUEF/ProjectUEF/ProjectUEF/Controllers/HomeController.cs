using ProjectUEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectUEF.Controllers
{

    public class HomeController : Controller
    {
        private DoAnTotNghiepEntities db = new DoAnTotNghiepEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tintuc()
        {
            return PartialView(db.TinTucs.ToList());
        }
        public ActionResult Slider()
        {
            return PartialView(db.Sliders.ToList());
        }

    }
}