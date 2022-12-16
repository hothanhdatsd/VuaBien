using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaFoodProject.Areas.User.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        seafoodEntities db=new seafoodEntities();
        // GET: User/ChiTietSanPham
        public ActionResult detailSanPham(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            HAISAN chitiet = db.HAISANs.Find(id);
            if (chitiet == null)
            {
                return HttpNotFound();
            }
                return View(chitiet);
            
        }
    }
}