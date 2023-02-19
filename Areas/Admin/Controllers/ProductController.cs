using PagedList;
using ShopOnline.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ShopOnlineEntities objShopOnlineEntities = new ShopOnlineEntities();
        // GET: Admin/Product
        public ActionResult Product(string SearchString)
        {
            
            var lstSanPham = new List<SanPham>();  
            
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstSanPham = objShopOnlineEntities.SanPhams.Where(n => n.TenSP.Contains(SearchString)).ToList();
            }    
            else
            {
                lstSanPham = objShopOnlineEntities.SanPhams.ToList();
            }
                    
            lstSanPham = lstSanPham.OrderByDescending(n => n.MaSP).ToList();
            return View(lstSanPham);           
        }


        [HttpGet]
        public ActionResult Create()
        {            
           ViewBag.MaLoaiSP = new SelectList(objShopOnlineEntities.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaNSX = new SelectList(objShopOnlineEntities.NhaSanXuats.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");           
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(SanPham objSanPham, HttpPostedFileBase HinhChinh, HttpPostedFileBase Hinh1, HttpPostedFileBase Hinh2, HttpPostedFileBase Hinh3)
        {
            //Load dropdownlist nhà cung cấp và dropdownlist loại sp, mã nhà sx
            ViewBag.MaLoaiSP = new SelectList(objShopOnlineEntities.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaNSX = new SelectList(objShopOnlineEntities.NhaSanXuats.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");


            //ktra hình ảnh tồn tại trong csdl
            
            if (HinhChinh != null)
            {
                if (HinhChinh.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhChinh.FileName);  //Lấy tên hình
                    var path = Path.Combine(Server.MapPath("~/Asset/img"), fileName);  //lấy hình đưa vào folder HinhAnhSP

                    //lấy hình đưa vào folder
                    HinhChinh.SaveAs(path);
                    objSanPham.HinhChinh = fileName;  //lưu vào sp
                }
            }
            if (Hinh1 != null)
            {
                if (Hinh1.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Hinh1.FileName);  //Lấy tên hình
                    var path = Path.Combine(Server.MapPath("~/Asset/img"), fileName);   //lấy hình đưa vào folder HinhAnhSP

                    //lấy hình đưa vào folder
                    Hinh1.SaveAs(path);
                    objSanPham.Hinh1 = fileName;  //lưu vào sp
                }
            }
            if (Hinh2 != null)
            {
                if (Hinh2.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Hinh2.FileName);  //Lấy tên hình
                    var path = Path.Combine(Server.MapPath("~/Asset/img"), fileName);   //lấy hình đưa vào folder HinhAnhSP

                    //lấy hình đưa vào folder
                    Hinh2.SaveAs(path);
                    objSanPham.Hinh2 = fileName;  //lưu vào sp
                }
            }
            if (Hinh3 != null)
            {
                if (Hinh3.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Hinh3.FileName);  //Lấy tên hình
                    var path = Path.Combine(Server.MapPath("~/Asset/img"), fileName);   //lấy hình đưa vào folder HinhAnhSP

                    //lấy hình đưa vào folder
                    Hinh3.SaveAs(path);
                    objSanPham.Hinh3 = fileName;  //lưu vào sp
                }
            }


            objShopOnlineEntities.SanPhams.Add(objSanPham);
            objShopOnlineEntities.SaveChanges();

            return RedirectToAction("Product");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var objSanPham = objShopOnlineEntities.SanPhams.Where(n => n.MaSP == id).FirstOrDefault();
            ViewBag.MaLoaiSP = TaoDanhSachLoaiSP(objSanPham.MaLoaiSP);

            ViewBag.MaNSX = TaoDanhSachMaNSX(objSanPham.MaNSX);

            return View(objSanPham);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(string id, SanPham objSanPham , HttpPostedFileBase HinhChinh, HttpPostedFileBase Hinh1, HttpPostedFileBase Hinh2, HttpPostedFileBase Hinh3)
        {

            ViewBag.MaLoaiSP = new SelectList(objShopOnlineEntities.LoaiSanPhams, "MaLoaiSP", "TenLoaiSP", objSanPham.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(objShopOnlineEntities.NhaSanXuats, "MaNSX", "TenNSX" , objSanPham.MaNSX);

            if (HinhChinh != null)
            {
                string path = Server.MapPath("~/Asset/img/");
                string Ten = null;
                HinhChinh.SaveAs(path + HinhChinh.FileName);
                Ten = HinhChinh.FileName;

                if (!string.IsNullOrEmpty(objSanPham.HinhChinh))
                {
                    string pathAndFname = Server.MapPath($"~/Asset/img/{objSanPham.HinhChinh}");
                    if (System.IO.File.Exists(pathAndFname))
                        System.IO.File.Delete(pathAndFname);
                }
                objSanPham.HinhChinh = Ten;
            }

            if (Hinh1 != null)
            {
                string path = Server.MapPath("~/Asset/img/");
                string Ten = null;
                Hinh1.SaveAs(path + Hinh1.FileName);
                Ten = Hinh1.FileName;

                if (!string.IsNullOrEmpty(objSanPham.Hinh1))
                {
                    string pathAndFname = Server.MapPath($"~/Asset/img/{objSanPham.Hinh1}");
                    if (System.IO.File.Exists(pathAndFname))
                        System.IO.File.Delete(pathAndFname);
                }
                objSanPham.Hinh1 = Ten;
            }

            if (Hinh2 != null)
            {
                string path = Server.MapPath("~/Asset/img/");
                string Ten = null;
                Hinh2.SaveAs(path + Hinh2.FileName);
                Ten = Hinh2.FileName;

                if (!string.IsNullOrEmpty(objSanPham.Hinh2))
                {
                    string pathAndFname = Server.MapPath($"~/Asset/img/{objSanPham.Hinh2}");
                    if (System.IO.File.Exists(pathAndFname))
                        System.IO.File.Delete(pathAndFname);
                }
                objSanPham.Hinh2 = Ten;
            }

            if (Hinh3 != null)
            {
                string path = Server.MapPath("~/Asset/img/");
                string Ten = null;
                HinhChinh.SaveAs(path + Hinh3.FileName);
                Ten = Hinh3.FileName;

                if (!string.IsNullOrEmpty(objSanPham.Hinh3))
                {
                    string pathAndFname = Server.MapPath($"~/Asset/img/{objSanPham.Hinh3}");
                    if (System.IO.File.Exists(pathAndFname))
                        System.IO.File.Delete(pathAndFname);
                }
                objSanPham.Hinh3 = Ten;
            }


            objShopOnlineEntities.Entry(objSanPham).State = EntityState.Modified;
            objShopOnlineEntities.SaveChanges();
            return RedirectToAction("Product");
        }


        [HttpGet]
        public ActionResult Delete(string id)
        {
            var objSanPham = objShopOnlineEntities.SanPhams.Where(n => n.MaSP == id).FirstOrDefault();
            return View(objSanPham);
        }

        [HttpPost]
        public ActionResult Delete(string id, SanPham objDelete)
        {
            var objSanPham = objShopOnlineEntities.SanPhams.Where(n => n.MaSP == id).FirstOrDefault();
            objShopOnlineEntities.SanPhams.Remove(objSanPham);
            objShopOnlineEntities.SaveChanges();
            return RedirectToAction("Product");
        }

        public ActionResult Detais(string id)
        {
            var objSanPham = objShopOnlineEntities.SanPhams.Where(n => n.MaSP == id).FirstOrDefault();
            return View(objSanPham);
        }

        private SelectList TaoDanhSachMaNSX(string IDChon)
        {
     
          var items =  objShopOnlineEntities.NhaSanXuats.Select(p => new { p.MaNSX, TinhTrang = p.TenNSX }).ToList();
            var result = new SelectList(items, "MaNSX" , "TinhTrang", selectedValue: IDChon);
            return result;
        }
        private SelectList TaoDanhSachLoaiSP(string IDChon)
        {            
            var items = objShopOnlineEntities.LoaiSanPhams.Select(p => new { p.MaLoaiSP, TinhTrang = p.TenLoaiSP }).ToList();
            var result = new SelectList(items, "MaLoaiSP", "TinhTrang", selectedValue: IDChon);
            return result;
        }
    }
}