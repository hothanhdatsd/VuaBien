using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaFoodProject.Areas.User.Controllers
{
    public class UserController : BaseController
    {
        seafoodEntities db = new seafoodEntities();
        // GET: User/User
        public ActionResult Index()
        {
            if ((string.IsNullOrEmpty(Session["idUser"] as string)))
            {
                return RedirectToAction("Index", "Login");
            }
            var id = Session["idUser"].ToString();
            var user = db.KHACHHANGs.FirstOrDefault(s => s.MAKH == id);
            return View(user);
        }

        public ActionResult UpdateProfile()
        {
            if ((string.IsNullOrEmpty(Session["idUser"] as string)))
            {
                return RedirectToAction("Index", "Login");
            }
            var id = Session["idUser"].ToString();
            var user = db.KHACHHANGs.FirstOrDefault(s => s.MAKH == id);


            return View(user);
        }


        [HttpPost, ActionName("UpdateProfile")]
        public JsonResult UpdateProfile2(string UserFullName, string UserAddr, string UserNumber, string UserMail)
        {
            if ((string.IsNullOrEmpty(Session["idUser"] as string)))
            {
                return Json(new
                {
                    state = 404
                });
            }
            var id = Session["idUser"].ToString();
            var user = db.KHACHHANGs.FirstOrDefault(s => s.MAKH == id);
            user.HOTEN = UserFullName;
            user.DIACHI = UserAddr;
            user.SDT = UserNumber;
            user.EMAIL = UserMail;
            Session["Addr"] = UserAddr;
            Session["Fullname"] = UserFullName;
            Session["sdt"] = UserNumber;
            db.SaveChanges();
            return Json(new { 
                state = 200
            });
        }


    }
}