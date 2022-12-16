using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaFoodProject.Areas.User.Controllers
{
    public class LogUpController : Controller
    {
        seafoodEntities db = new seafoodEntities();
        // GET: User/LogUp
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(KHACHHANG KH)
        {
            if (ModelState.IsValid)
            {
                
                    KH.MAKH = KH.USERNAME + "a";
                    KH.DIACHI = "KingOcena121212";
                    KH.SDT = "0126789";
                    KH.TONGDON = 0;
                    KH.HOTEN = KH.USERNAME + "KingOcean";
                    db.KHACHHANGs.Add(KH);
                    db.SaveChanges();
                    return Redirect("/User/Home/Index");
               
            }
            else
            {
                return Redirect("/User/Logup/Index");
            }
            
        }
    }
}