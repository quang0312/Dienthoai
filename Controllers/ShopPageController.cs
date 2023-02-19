using PagedList;
using ShopOnline.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class ShopPageController : Controller
    {
        ShopOnlineEntities objShopOnlineEntities = new ShopOnlineEntities();
        // GET: ShopPage
        public ActionResult ShopPage(int? page)
        {

            var lstSanPham = objShopOnlineEntities.SanPhams.ToList();
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(lstSanPham.ToPagedList(pageNumber, pageSize));
        }

        // GET: ShopPage/Details/5
        public ActionResult Details(string id)
        {
            var objProduct = objShopOnlineEntities.SanPhams.Where(n => n.MaSP == id).FirstOrDefault();

            return View(objProduct);
           
        }

        // GET: ShopPage/Create
        public ActionResult ProductSamSung(string id, int? page)
        {
            var lstProductSamSung = objShopOnlineEntities.SanPhams.Where(n => n.MaNSX == id).ToList();
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(lstProductSamSung.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LoaiSP(string id, int? page)
        {
            var lstLoaiSP = objShopOnlineEntities.SanPhams.Where(n => n.MaLoaiSP == id).ToList();
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(lstLoaiSP.ToPagedList(pageNumber, pageSize));
        }

        // POST: ShopPage/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopPage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShopPage/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopPage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShopPage/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
