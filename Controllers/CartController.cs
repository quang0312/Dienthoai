using ShopOnline.common;
using ShopOnline.Context;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class CartController : Controller
    {
        ShopOnlineEntities objShopOnlineEntities = new ShopOnlineEntities();
        // GET: Cart
        public ActionResult Index()
        {

            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            List<ItemGioHang> lstGioHang = LayGioHang();                  
            return View(lstGioHang);
            
        }

        public ActionResult GioHangPartial()
        {
            if (TinhTongSoLuong() == 0) //ktra soluong giỏ hàng
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            //Hiển thị tổng tiền và sl sp lên trên icon giỏ hàng
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();
        }

        public ActionResult AddToCart(string id, string strURL)
        {
            SanPham sp = objShopOnlineEntities.SanPhams.SingleOrDefault(n => n.MaSP == id);  //lấy sp với masp tương ứng
            if (sp == null)  //nếu lấy sai masp
            {
                //Trang đường dẫn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
             
            //Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = LayGioHang();

            //trường hợp nếu 1 sp đã tồn tại trong giỏ hàng
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == id);  //ktra sp có trong lst đã tạo hay ko
            if (spCheck != null)
            {
                //ktra số lg tồn trc khi cho kh mua hàng
                if (sp.SoLuongdaban < spCheck.SoLuong)
                {
                    return View("ThongBao");
                }  
                //nếu sp đã có trong list thì khi thêm vào giỏ hàng sẽ tăng số lượng lên
                spCheck.SoLuong++; 
                //và đơn giá sẽ tăng theo giá sp * sl tương ứng
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.Gia;
                return Redirect(strURL);
            }

            //nếu chưa tồn tại thì thêm vào list
            ItemGioHang itemGH = new ItemGioHang(id);
            if (sp.SoLuongdaban < itemGH.SoLuong) //ktra số lg tồn trc khi cho kh mua hàng
                return View("ThongBao");
            lstGioHang.Add(itemGH);
            return Redirect(strURL);
        }

        //chỉnh sửa giỏ hàng
        [HttpGet]
        public ActionResult SuaGioHang(string id)
        {
            //ktra session giỏ hàng có tồn tại ko
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");   //quay về trang chủ
            }
            //ktra sp có tồn tại trong csdl ko
            SanPham sp = objShopOnlineEntities.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                //Trang đường dẫn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            //Lấy list giỏ hàng từ session
            List<ItemGioHang> lstGioHang = LayGioHang();

            //ktra xem sp đó có tồn tại trong giỏ hàng hay ko
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == id);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Cart");
            }
            //Lấy list giỏ hàng tạo giao diện
                ViewBag.GioHang = lstGioHang;
            //nếu tồn tại thì
            return View(spCheck);
        }


        //Cập nhật giỏ hàng
        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang itemGH)
        {                      
            //update số lg trong session giỏ hàng
            //bc1: Lấy list giỏ hàng từ sesssion giỏ hàng
            List<ItemGioHang> lstGH = LayGioHang();
            //bc2: lấy sp cần update từ trong list giỏ hàng
            ItemGioHang itemGHUpdate = lstGH.Find(n => n.MaSP == itemGH.MaSP);  //pt find dùng để tìm các trường mong muốn
            //bc3: update lại số lg và thành tiền
            itemGHUpdate.SoLuong = itemGH.SoLuong;
            itemGHUpdate.ThanhTien = itemGHUpdate.SoLuong * itemGHUpdate.Gia;
            return RedirectToAction("Index");
        }


        public ActionResult XoaGioHang(string id)
        {
            //ktra session giỏ hàng có tồn tại ko
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //ktra sp có tồn tại trong csdl ko
            SanPham sp = objShopOnlineEntities.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                //Trang đường dẫn ko hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            //Lấy list giỏ hàng từ session
            List<ItemGioHang> lstGioHang = LayGioHang();
            //ktra xem sp đó có tồn tại trong giỏ hàng hay ko
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == id);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Cart");
            }
            //Xóa item trong giỏ hàng
            lstGioHang.Remove(spCheck);
            return RedirectToAction("Index");
        }


        public List<ItemGioHang> LayGioHang()
        {
            //nếu giỏ hàng đã tồn tại
            List<ItemGioHang> lstGioHang = Session["Cart"] as List<ItemGioHang>; //lưu giỏ hàng vào session giohang để quản lý
            if (lstGioHang == null)
            {
                //Nếu session giỏ hàng ko tồn tại thì khởi tạo giỏ hàng
                lstGioHang = new List<ItemGioHang>();
                Session["Cart"] = lstGioHang;
            }
            return lstGioHang;
        }

        public double TinhTongSoLuong()
        {
            //lấy giỏ hàng
            List<ItemGioHang> lstGioHang = Session["Cart"] as List<ItemGioHang>;
            if (lstGioHang == null)  //nếu chưa có list giỏ hàng thì trả về gtri = 0
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong); //trả về tổng số lượng của list giỏ hàng
        }

        //tính tổng tiền
        public decimal TinhTongTien()
        {
            //lấy giỏ hàng
            List<ItemGioHang> lstGioHang = Session["Cart"] as List<ItemGioHang>;
            if (lstGioHang == null) //nếu chưa có list giỏ hàng thì trả về gtri = 0
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);    //trả về tổng tiền của list giỏ hàng
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (objShopOnlineEntities != null)
                    objShopOnlineEntities.Dispose();
                objShopOnlineEntities.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Payment()
        {
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            List<ItemGioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
           if (Session["id"] == null)
            {
                return RedirectToAction("Login", "Home");

            }
            else
            {
                //Lấy thông tin từ giỏ hàng từ biến session
                var lstCart = (List<ItemGioHang>)Session["Cart"];
                //gán dữ liệu cho bảng ThanhToan
                ThanhToan objTT = new ThanhToan();
                objTT.Name = shipName;
                objTT.UserID = int.Parse(Session["id"].ToString());
                objTT.NgayTao = DateTime.Now;
                objTT.Email = email;
                objTT.DiaChi = address;
                objTT.Status = 1;
                objShopOnlineEntities.ThanhToans.Add(objTT);
                //LƯU THông tin vào bảng ThanhToan
                objShopOnlineEntities.SaveChanges();

                //Lấy id vừa tạo để lưu vào bảng CTThanhToan
                int orderID = objTT.id;

                List<CTThanhToan> lstThanhToan = new List<CTThanhToan>();
                foreach(var item in lstCart)
                {
                    CTThanhToan obj = new CTThanhToan();
                    obj.SoLuong = item.SoLuong;
                    obj.Dongia = (double?)item.Gia;
                    obj.id = orderID;
                    obj.MaSP = item.MaSP;
                    lstThanhToan.Add(obj);
                }
                objShopOnlineEntities.CTThanhToans.AddRange(lstThanhToan);
                objShopOnlineEntities.SaveChanges();

               
                ViewBag.TongTien = TinhTongTien();
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);                
                content = content.Replace("{{Total}}", ViewBag.TongTien.ToString("#,## VNĐ"));
                
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                // Để Gmail cho phép SmtpClient kết nối đến server SMTP của nó với xác thực 
                //là tài khoản gmail của bạn, bạn cần thiết lập tài khoản email của bạn như sau:
                //Vào địa chỉ https://myaccount.google.com/security  Ở menu trái chọn mục Bảo mật, sau đó tại mục Quyền truy cập 
                //của ứng dụng kém an toàn phải ở chế độ bật
                //  Đồng thời tài khoản Gmail cũng cần bật IMAP
                //Truy cập địa chỉ https://mail.google.com/mail/#settings/fwdandpop

                new MailHelper().SendMail(email, "Đơn hàng mới từ Shop Minh Quang", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ Shop Minh Quang", content);

                return Redirect("/Cart/Success");
            }
            

        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult UnSuccess()
        {
            return View();
        }
    }     
   
}