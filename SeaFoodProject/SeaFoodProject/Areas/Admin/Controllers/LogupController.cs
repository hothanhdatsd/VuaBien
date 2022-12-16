using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaFoodProject.Areas.Admin.Controllers
{
    public class LogupController : Controller
    {
        seafoodEntities db=new seafoodEntities();
        // GET: Admin/Logup
        public ActionResult Index()
        {
            return View();
        }
    }
}