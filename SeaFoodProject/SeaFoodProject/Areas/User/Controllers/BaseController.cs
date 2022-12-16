using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaFoodProject.Areas.User.Controllers
{
    public class BaseController : Controller
    {
        public bool checkLogin()
        {
            HttpCookie userCookie = Request.Cookies["userCookie"];
            if (userCookie != null)
            {
                Session["idUser"] = userCookie.Values["userid"].ToString();
                Session["Email"] = userCookie.Values["Email"].ToString();
                Session["sdt"] = userCookie.Values["sdt"].ToString();
                Session["Addr"] = userCookie.Values["Addr"].ToString();
                Session["Fullname"] = userCookie.Values["Fullname"].ToString();
                //Yes userId is found. Mission accomplished.
                return true;
            }
            else return false;
        }
    }
}