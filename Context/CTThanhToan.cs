//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopOnline.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTThanhToan
    {
        public int id { get; set; }
        public Nullable<int> ThanhToanID { get; set; }
        public string MaSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<double> Dongia { get; set; }
    
        public virtual ThanhToan ThanhToan { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
