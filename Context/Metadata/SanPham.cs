using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Context
{
    [MetadataTypeAttribute(typeof(SanPhamMetadata))]
    public partial class SanPham
    {
        internal sealed class SanPhamMetadata
        {
            public string MaSP { get; set; }

            [DisplayName("Mã loại sản phẩm")]
            public string MaLoaiSP { get; set; }

            [DisplayName("Mã nhà sản xuất")]
            public string MaNSX { get; set; }

            [DisplayName("Tên sản phẩm")]
            public string TenSP { get; set; }

            [DisplayName("Mô tả")]
            public string MoTa { get; set; }

            [DisplayName("Hình Chính")]
            public string HinhChinh { get; set; }

            [DisplayName("Hình 1")]
            public string Hinh1 { get; set; }

            [DisplayName("Hình 2")]
            public string Hinh2 { get; set; }

            [DisplayName("Hình 3")]
            public string Hinh3 { get; set; }

            public string Hinh4 { get; set; }

            [DisplayName("Giá")]
            public Nullable<int> Gia { get; set; }

            [DisplayName("Số lượng đã bán")]
            public Nullable<int> SoLuongdaban { get; set; }

            [DisplayName("Lượt view")]
            public Nullable<int> LuotView { get; set; }

            [DisplayName("Tình Trạng")]
            public string TinhTrang { get; set; }
        }
    }
}