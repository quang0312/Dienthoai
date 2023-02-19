
using ShopOnline.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        ShopOnlineEntities objShopOnlineEntities = new ShopOnlineEntities();
        // GET: Admin/DonHang
        public ActionResult Index()
        {
            var lstDonHang = objShopOnlineEntities.ThanhToans.ToList();
            return View(lstDonHang);
            
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Create(ThanhToan loaisp)
        {
            //Load dropdownlist nhà cung cấp và dropdownlist loại sp, mã nhà sx
            objShopOnlineEntities.ThanhToans.Add(loaisp);
            objShopOnlineEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var loaisp = objShopOnlineEntities.ThanhToans.Where(n => n.id == id).FirstOrDefault();
            return View(loaisp);
        }


        [HttpPost]
        public ActionResult Edit(int id, ThanhToan loaisp)
        {
            objShopOnlineEntities.Entry(loaisp).State = EntityState.Modified;
            objShopOnlineEntities.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var loaisp = objShopOnlineEntities.ThanhToans.Where(n => n.id == id).FirstOrDefault();
            return View(loaisp);
        }


        [HttpPost]
        public ActionResult Delete(int id, ThanhToan objDelete)
        {
            var loaisp = objShopOnlineEntities.ThanhToans.Where(n => n.id == id).FirstOrDefault();
            objShopOnlineEntities.ThanhToans.Remove(loaisp);
            objShopOnlineEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Detais(int id)
        {
            var loaisp = objShopOnlineEntities.ThanhToans.Where(n => n.id == id).FirstOrDefault();
            return View(loaisp);
        }
    }
}