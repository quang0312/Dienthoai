using ShopOnline.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class LoaiSPController : Controller
    {

        ShopOnlineEntities objShopOnlineEntities = new ShopOnlineEntities();
        // GET: Admin/LoaiSP
        public ActionResult LoaiSP()
        {
            var lstLoaiSP = objShopOnlineEntities.LoaiSanPhams.ToList();
            return View(lstLoaiSP);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Create(LoaiSanPham loaisp)
        {
            //Load dropdownlist nhà cung cấp và dropdownlist loại sp, mã nhà sx
            objShopOnlineEntities.LoaiSanPhams.Add(loaisp);
            objShopOnlineEntities.SaveChanges();
            return RedirectToAction("LoaiSP");
        }


        [HttpGet]
        public ActionResult Edit(string id)
        {
            var loaisp = objShopOnlineEntities.LoaiSanPhams.Where(n => n.MaLoaiSP == id).FirstOrDefault();
            return View(loaisp);
        }


        [HttpPost]
        public ActionResult Edit(string id, LoaiSanPham loaisp)
        {
            objShopOnlineEntities.Entry(loaisp).State = EntityState.Modified;
            objShopOnlineEntities.SaveChanges();
            return RedirectToAction("LoaiSP");
        }


        [HttpGet]
        public ActionResult Delete(string id)
        {
            var loaisp = objShopOnlineEntities.LoaiSanPhams.Where(n => n.MaLoaiSP == id).FirstOrDefault();
            return View(loaisp);
        }


        [HttpPost]
        public ActionResult Delete(string id, LoaiSanPham objDelete)
        {
            var loaisp = objShopOnlineEntities.LoaiSanPhams.Where(n => n.MaLoaiSP == id).FirstOrDefault();
            objShopOnlineEntities.LoaiSanPhams.Remove(loaisp);
            objShopOnlineEntities.SaveChanges();
            return RedirectToAction("LoaiSP");
        }

        public ActionResult Detais(string id)
        {
            var loaisp = objShopOnlineEntities.LoaiSanPhams.Where(n => n.MaLoaiSP == id).FirstOrDefault();
            return View(loaisp);
        }
    }
}