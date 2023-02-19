using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Context
{
    public class ItemGioHang
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhChinh { get; set; }
        public ItemGioHang() { }

        //constructor theo id (dùng cho trường hợp chỉ có sl=1)
        public ItemGioHang(string iMaSP)
        {
            using (ShopOnlineEntities objShopOnlineEntities = new ShopOnlineEntities())
            {
                this.MaSP = iMaSP;
                SanPham sp = objShopOnlineEntities.SanPhams.Single(n => n.MaSP == iMaSP);
                this.TenSP = sp.TenSP;
                this.HinhChinh = sp.HinhChinh;
                this.Gia = sp.Gia.Value;  //kiểu decimal dùng value để lấy gtri
                this.SoLuong = 1;
                this.ThanhTien = Gia * SoLuong;
            }
        }

        public ItemGioHang(string iMaSP, int sl)
        {
            using (ShopOnlineEntities objShopOnlineEntities = new ShopOnlineEntities())
            {
                this.MaSP = iMaSP;
                SanPham sp = objShopOnlineEntities.SanPhams.Single(n => n.MaSP == iMaSP);
                this.TenSP = sp.TenSP;
                this.HinhChinh = sp.HinhChinh;
                this.Gia = sp.Gia.Value;
                this.SoLuong = sl;
                this.ThanhTien = Gia * SoLuong;
            }
        }

        
    }
}