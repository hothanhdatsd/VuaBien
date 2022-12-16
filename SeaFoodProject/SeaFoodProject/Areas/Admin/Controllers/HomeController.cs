using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaFoodProject.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            checkLogin();
            if (Session["idStaff"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}