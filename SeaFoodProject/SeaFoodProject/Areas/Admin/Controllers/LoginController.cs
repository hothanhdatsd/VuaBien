using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaFoodProject.Areas.Admin.Controllers
{
    public class LoginController : BaseController
    {
        seafoodEntities db = new seafoodEntities();
        // GET: Admin/Login
        public ActionResult Index()
        {
            checkLogin();
            if (Session["idStaff"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string USERNAME, string PASS_WORD, string login_remember)
        {
            if (ModelState.IsValid)
            {
                var data = db.NHANVIENs.Where(s => s.USERNAME.Equals(USERNAME) && s.PASS_WORD.Equals(PASS_WORD)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["staffFullname"] = data.FirstOrDefault().HOTEN;
                    Session["CV"] = data.FirstOrDefault().CHUCVU.TENCV;
                    Session["idStaff"] = data.FirstOrDefault().MANV;
                    if (login_remember == "true")
                    {
                        HttpCookie staffCookie = new HttpCookie("staffCookie");
                        staffCookie.Values.Add("idStaff", data.FirstOrDefault().MANV);
                        staffCookie.Values.Add("CV", data.FirstOrDefault().CHUCVU.TENCV);
                        staffCookie.Values.Add("staffFullname", data.FirstOrDefault().HOTEN);
                        staffCookie.Expires = DateTime.Now.AddMinutes(120);
                        Response.Cookies.Add(staffCookie);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Response.Cookies["staffCookie"].Expires = DateTime.Now.AddDays(-1);

            return RedirectToAction("Index", "Login");

        }
    }
}