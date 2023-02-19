using ShopOnline.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class NhaSanXuatController : Controller
    {
        ShopOnlineEntities objShopOnlineEntities = new ShopOnlineEntities();
        // GET: Admin/NhaSanXuat
        public ActionResult NhaSanXuat(string SearchString)
        {
            var lstSanPham = new List<NhaSanXuat>();

            if (!string.IsNullOrEmpty(SearchString))
            {
                lstSanPham = objShopOnlineEntities.NhaSanXuats.Where(n => n.TenNSX.Contains(SearchString)).ToList();
            }
            else
            {
                lstSanPham = objShopOnlineEntities.NhaSanXuats.ToList();
            }

            lstSanPham = lstSanPham.OrderByDescending(n => n.MaNSX).ToList();
            return View(lstSanPham);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Create(NhaSanXuat objNhaSanXuat)
        {
            //Load dropdownlist nhà cung cấp và dropdownlist loại sp, mã nhà sx
            objShopOnlineEntities.NhaSanXuats.Add(objNhaSanXuat);
            objShopOnlineEntities.SaveChanges();
            return RedirectToAction("NhaSanXuat");

        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var objNhaSanXuat = objShopOnlineEntities.NhaSanXuats.Where(n => n.MaNSX == id).FirstOrDefault();           
            return View(objNhaSanXuat);
        }


        [HttpPost]
        public ActionResult Edit(string id, NhaSanXuat objNhaSanXuat)
        {
            objShopOnlineEntities.Entry(objNhaSanXuat).State = EntityState.Modified;
            objShopOnlineEntities.SaveChanges();
            return RedirectToAction("NhaSanXuat");
        }


        [HttpGet]
        public ActionResult Delete(string id)
        {
            var objNhaSanXuat = objShopOnlineEntities.NhaSanXuats.Where(n => n.MaNSX == id).FirstOrDefault();
            return View(objNhaSanXuat);
        }


        [HttpPost]
        public ActionResult Delete(string id, NhaSanXuat objDelete)
        {
            var objNhaSanXuat = objShopOnlineEntities.NhaSanXuats.Where(n => n.MaNSX == id).FirstOrDefault();
            objShopOnlineEntities.NhaSanXuats.Remove(objNhaSanXuat);
            objShopOnlineEntities.SaveChanges();
            return RedirectToAction("NhaSanXuat");
        }

        public ActionResult Detais(string id)
        {
            var objNhaSanXuat = objShopOnlineEntities.NhaSanXuats.Where(n => n.MaNSX == id).FirstOrDefault();
            return View(objNhaSanXuat);
        }
    }
}