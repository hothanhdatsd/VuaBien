using SeaFoodProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeaFoodProject.Areas.User.Controllers
{
    public class ProductController : Controller
    {
        seafoodEntities db = new seafoodEntities();
        // GET: User/Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult search(string name = "")
        {
            var haisans = db.HAISANs.Where(s => s.CHITIETLOAI.Contains(name)).Select(s => new
            {
                Name = s.CHITIETLOAI,
                Price = s.GIATIEN,
                Loai = s.LOAIHAISAN.TENLOAI,
                SL = s.SOLUONG,
                DVT = s.DONVITINH.TENDVT,
                HINHANH = s.HINHANH,
                MHS = s.MSHAISAN
            });
            return Json(haisans, JsonRequestBehavior.AllowGet);
        }

        public ActionResult cartItem()
        {
           
            return View();
        }


        [HttpPost]
        public JsonResult AddtoCart(string id)
        {
            List<CartItem> listcartItem;
            if (Session["ShoppingCart"] == null)
            {
                listcartItem = new List<CartItem>();
                listcartItem.Add(new CartItem
                {
                    qty = 1,
                    ProductOrder = db.HAISANs.FirstOrDefault(s => s.MSHAISAN == id)
                });
                Session["ShoppingCart"] = listcartItem;
            } else
            {
                bool flag = false;
                listcartItem = (List<CartItem>)Session["ShoppingCart"];
                foreach (CartItem item in listcartItem)
                {
                    if (item.ProductOrder.MSHAISAN == id)
                    {
                        item.qty++; 
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                    listcartItem.Add(new CartItem { qty = 1, ProductOrder = db.HAISANs.FirstOrDefault(s => s.MSHAISAN == id) });

                Session["ShoppingCart"] = listcartItem;          
            }
            int cartcount = 0;
            List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
            foreach (CartItem item in ls)
            {
                cartcount += item.qty;
            }
            return Json(new { ItemAmount = cartcount });
        }


        public JsonResult AddToOrder()
        {
            double? sum = 0.0;
            if (Session["ShoppingCart"] != null)
            {
                List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
                if (ls.Count <= 0)
                {
                    return Json(new
                    {
                        state = "fail"
                    });
                }
                foreach (var item in ls)
                {
                    sum += item.qty * item.ProductOrder.GIATIEN;
                }
                //tao don dat
                var dondat = new DONDAT();
                dondat.MAKH = Session["idUser"].ToString();
                dondat.MATT = "MTT002";
                dondat.THOIGIAN = DateTime.Now;
                dondat.TONGTIEN = sum;
                db.DONDATs.Add(dondat);
                db.SaveChanges();

                //tao chi tiet don dat

                foreach (var item in ls)
                {
                var chitietdondat = new CHITIETDONDAT();
                    chitietdondat.MADON = dondat.MADON;
                    chitietdondat.MSHAISAN = item.ProductOrder.MSHAISAN;
                    chitietdondat.SOLUONG = item.qty;
                    chitietdondat.GIATIEN = item.qty * item.ProductOrder.GIATIEN;
                    db.CHITIETDONDATs.Add(chitietdondat);
                    db.SaveChanges();
                }

            } else
            {
                //neu khong co session
            }

            


            return Json(new { state = "success"});
        }



        [HttpPost]
        public JsonResult updateQty(string id, int qty)
        {
            var qtyy = 0;
            double? giaa = 0.00;
            double? summ = 0.00;
            if (Session["ShoppingCart"] != null)
            {
                List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
                var hsan = ls.FirstOrDefault(s => s.ProductOrder.MSHAISAN == id);
                hsan.qty = qty;
                qtyy = hsan.qty;
                giaa = qtyy * hsan.ProductOrder.GIATIEN;
                foreach (var item in ls)
                {
                    summ += item.qty * item.ProductOrder.GIATIEN;
                };
            }
            else
            {
                //neu khong co session
            }
            

            return Json(new
            {
                id = id,
                qty =  qtyy,
                gia = giaa,
                sum = summ
            });
        }

        public ActionResult OrderList()
        {
            if ((string.IsNullOrEmpty(Session["idUser"] as string)))
            {
                return RedirectToAction("Index", "Login");
            }
            var id = Session["idUser"].ToString();
            var kh = db.KHACHHANGs.FirstOrDefault(s => s.MAKH == id);
            var dondat = kh.DONDATs.ToList();

            return View(dondat);
        }

        public ActionResult OrderDetail(int id)
        {
            var don = db.DONDATs.FirstOrDefault(s => s.MADON == id);
            var chitietdon = don.CHITIETDONDATs.ToList();

            return View(chitietdon);
        }


        public JsonResult deletecartItem(string id)
        {
            double? summ = 0.0;
            List<CartItem> ls = (List<CartItem>)Session["ShoppingCart"];
            var hsan = ls.FirstOrDefault(s => s.ProductOrder.MSHAISAN == id);
            ls.Remove(hsan);

            foreach (var item in ls)
            {
                summ += item.qty * item.ProductOrder.GIATIEN;
            };
            return Json(new
            {
                id = id,
                sum = summ,
                state = "success"
            });
        }
    }
}