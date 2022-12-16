using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaFoodProject.Areas.User.Controllers
{
    public class HomeController : BaseController
    {
        // GET: User/Home
        seafoodEntities db = new seafoodEntities();
        public ActionResult Index()
        {
            checkLogin();
            var hsan = db.HAISANs.ToList();
            return View(hsan);
        }
    }
}