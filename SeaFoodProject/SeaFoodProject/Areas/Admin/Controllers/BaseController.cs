using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaFoodProject.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        public bool checkLogin()
        {
            HttpCookie staffCookie = Request.Cookies["staffCookie"];
            if (staffCookie != null)
            {
                Session["CV"] = staffCookie.Values["CV"].ToString();
                Session["idStaff"] = staffCookie.Values["idStaff"].ToString();
                Session["staffFullname"] = staffCookie.Values["staffFullname"].ToString();
                //Yes userId is found. Mission accomplished.
                return true;
            }
            else return false;
        }
    }
}