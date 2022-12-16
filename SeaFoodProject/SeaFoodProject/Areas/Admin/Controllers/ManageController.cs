using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace SeaFoodProject.Areas.Admin.Controllers
{
    public class ManageController : Controller
    {
        // GET: Admin/Manage
        seafoodEntities db = new seafoodEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult member()
        {
            var kh = db.KHACHHANGs.ToList();
            return View(kh);
        }


        /*NhanVien*/
        public ActionResult getlistNHANVIEN(int? page)
        {
            if (page == null) page = 1;

            var list = (from s in db.NHANVIENs select s).OrderBy(x => x.MANV);

            int pageSize = 5;

            int pageNumber = (page ?? 1);

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult addNHANVIEN()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addNHANVIEN(NHANVIEN nv)
        {
            NHANVIEN n_nv = db.NHANVIENs.Find(nv.MANV);
            if (n_nv != null)
            {
                return Content("Đã tồn tại MÃ Nhân Viên");
            }
            else
            {
                db.NHANVIENs.Add(nv);
                db.SaveChanges();
                return RedirectToAction("getlistNHANVIEN");
            }
        }

        public ActionResult editNHANVIEN(string id)
        {
            NHANVIEN s_nv = db.NHANVIENs.Find(id);
            return View(s_nv);
        }

        [HttpPost]

        public ActionResult editNHANVIEN(NHANVIEN nv)
        {
            db.Entry(nv).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("getlistNHANVIEN");
        }

        public ActionResult detailNHANVIEN(string id)
        {
            NHANVIEN dt_nv = db.NHANVIENs.Find(id);
            return View(dt_nv);
        }

        public ActionResult delNHANVIEN(string id)
        {
            NHANVIEN del_nv = db.NHANVIENs.Find(id);
            db.NHANVIENs.Remove(del_nv);
            db.SaveChanges();
            return RedirectToAction("getlistNHANVIEN");
        }
        /*NhanVienEnd*/
        /*Haisan*/
        public ActionResult list_HAISAN(int? page)
            {
                if (page == null) page = 1;

                var list = (from s in db.HAISANs select s).OrderBy(x => x.MSHAISAN);

                int pageSize = 5;

                int pageNumber = (page ?? 1);

                return View(list.ToPagedList(pageNumber, pageSize));
            }

            public ActionResult add_HAISAN()
            {
                return View();
            }

            [HttpPost]
            public ActionResult add_HAISAN(HAISAN hs)
            {
                HAISAN add_hs = db.HAISANs.Find(hs.MSHAISAN);
                if (add_hs != null)
                {
                    return Content("Đã tồn tại Hải Sản");
                }
                else
                    db.HAISANs.Add(hs);
                db.SaveChanges();
                return RedirectToAction("list_HAISAN");
            }

            public ActionResult edit_HAISAN(string id)
            {
                HAISAN e_HS = db.HAISANs.Find(id);
                return View(e_HS);
            }

            [HttpPost]

            public ActionResult edit_HAISAN(HAISAN hs)
            {
                db.Entry(hs).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("list_HAISAN");
            }

            public ActionResult detail_HAISAN(string id)
            {
                HAISAN dt_HS = db.HAISANs.Find(id);
                return View(dt_HS);
            }

            public ActionResult delete_HAISAN(string id)
            {
                HAISAN d_HS = db.HAISANs.Find(id);
                db.HAISANs.Remove(d_HS);
                db.SaveChanges();
                return RedirectToAction("list_HAISAN");
            }
        
        /*haisanend*/
        public ActionResult order()
        {
            var ord = db.DONDATs.ToList();
            return View(ord);
        }

        public ActionResult bill()
        {
            var ord = db.HOADONs.ToList();
            return View(ord);
        }

        public ActionResult type()
        {
            var type = db.LOAIHAISANs.ToList();
            return View(type);
        }


        public JsonResult updateStateOrder(string id_state, int id_order)
        {
            var order = db.DONDATs.FirstOrDefault(s => s.MADON == id_order);
            order.MATT = id_state;
            db.SaveChanges();
            //neu trang thai giao hang thanh cong
            if (id_state == "MTT001")
            {

                //tao hoa don
                var bill = new HOADON();
                bill.MAKH = order.MAKH;
                var kh = db.KHACHHANGs.FirstOrDefault(s => s.MAKH == bill.MAKH);
                kh.TONGDON = kh.TONGDON + 1;
                bill.MANV = Session["idStaff"].ToString();
                bill.NGAYLAP = DateTime.Now;
                bill.TONGTIEN = order.TONGTIEN;
                db.HOADONs.Add(bill);
                db.SaveChanges();

                //chinh sua so luong ton kho
                foreach (var item in order.CHITIETDONDATs)
                {
                    //them chi tiet hoa don
                    var chitiet = new CHITIETHOADON();
                    chitiet.MSHAISAN = item.MSHAISAN;
                    chitiet.MAHD = bill.MAHD;
                    chitiet.SOLUONG = item.SOLUONG;
                    chitiet.GIATIEN = item.GIATIEN;
                    db.CHITIETHOADONs.Add(chitiet);
                    var product = db.HAISANs.FirstOrDefault(s => s.MSHAISAN == item.MSHAISAN);
                    product.SOLUONG = product.SOLUONG - item.SOLUONG;
                    db.SaveChanges();
                }

               
                

            }
            

            return Json(new
            {
                state = 200
            });
        }

        public ActionResult statistical()
        {
            double?[] arr = new double?[12];
            for (int i = 0; i <= 11; i++)
            {
                arr[i] = 0.0;
                var bill = db.HOADONs.ToList();
                foreach (var item in bill)
                {
                    int month = DateTime.Parse(item.NGAYLAP.ToString()).Month;
                    if ((i + 1) == month)
                    {
                        arr[i] += item.TONGTIEN;
                    }
                }

            }



            ViewBag.tke = arr;
            return View();
        }
    }
}