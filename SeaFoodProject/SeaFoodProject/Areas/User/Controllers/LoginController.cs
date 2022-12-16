using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaFoodProject.Areas.User.Controllers
{
    public class LoginController : BaseController
    {
        seafoodEntities db = new seafoodEntities();
        // GET: User/Login
        public ActionResult Index()
        {
            checkLogin();
            if (Session["idUser"] != null)
            {
                return RedirectToAction("Index", "Home");
            } else
            {
                return View();
            }
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string USERNAME, string PASS_WORD, string login_remember)
        {
            if (ModelState.IsValid)
            {
                var data = db.KHACHHANGs.Where(s => s.USERNAME.Equals(USERNAME) && s.PASS_WORD.Equals(PASS_WORD)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["Fullname"] = data.FirstOrDefault().HOTEN;
                    Session["Email"] = data.FirstOrDefault().EMAIL;
                    Session["sdt"] = data.FirstOrDefault().SDT;
                    Session["Addr"] = data.FirstOrDefault().DIACHI;
                    Session["idUser"] = data.FirstOrDefault().MAKH;
                    if (login_remember == "true")
                    {
                        HttpCookie userCookie = new HttpCookie("userCookie");
                        userCookie.Values.Add("userid", data.FirstOrDefault().MAKH);
                        userCookie.Values.Add("sdt", data.FirstOrDefault().SDT);
                        userCookie.Values.Add("Email", data.FirstOrDefault().EMAIL);

                        userCookie.Values.Add("Addr", data.FirstOrDefault().DIACHI);

                        userCookie.Values.Add("Fullname", data.FirstOrDefault().HOTEN);
                        userCookie.Expires = DateTime.Now.AddMinutes(120);
                        Response.Cookies.Add(userCookie);
                    }
                } else
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
            Response.Cookies["userCookie"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index", "Home");

        }

        public ActionResult changePassword()
        {
            if ((string.IsNullOrEmpty(Session["idUser"] as string)))
            {
                return RedirectToAction("Index");
            }
                var id = Session["idUser"].ToString();
            var User = db.KHACHHANGs.FirstOrDefault(s => s.MAKH == id);
            if (User != null)
            {
                return View(User);
            }
            else
            {
                RedirectToAction("Index");
            }

            return View();
        }

        
        [HttpPost]
        public ActionResult changePassword(string oldpassword, string password, string repassword)
        {
            var id = Session["idUser"].ToString();
            var User = db.KHACHHANGs.FirstOrDefault(s => s.MAKH == id);
            if (oldpassword != User.PASS_WORD)
            {
                ModelState.AddModelError("error", "Mật khẩu cũ không đúng!");
                return View("changePassword");
            }
            if (password != repassword)
            {
                return HttpNotFound();
            }
            if (password != null)
            {
                User.PASS_WORD = password;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult acb()
        {
            var list = db.HAISANs.ToList();
            return View(list);
        }
    }
}