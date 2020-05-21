using ProjectUEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectUEF.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private DoAnTotNghiepEntities db = new DoAnTotNghiepEntities();
        // GET: Admin/Login
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {

            if (ModelState.IsValid)
            {

                var obj = db.Users.Where(a => a.TenDangNhap.Equals(user.TenDangNhap) && a.MatKhau.Equals(user.MatKhau)).FirstOrDefault();
                
                if (obj != null)
                {
                    Session["ID"] = obj.ID.ToString();
                    Session["TenDangNhap"] = obj.TenDangNhap.ToString();
                    return RedirectToAction("Index", "Default");
                }
            }
            return RedirectToAction("DangNhap");
        }
    }
}