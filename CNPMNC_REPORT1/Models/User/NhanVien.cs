using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.User
{
    public class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTenNV { get; set; }
        public string Email { get; set; }
        public string MatKhauNV { get; set; }
        public string TrangThaiTKNV { get; set; }
        public string UserType { get { return "NhanVien"; } }

        public NhanVien() { }

        public NhanVien(string hoTenNV, string email, string matKhauNV, string trangThaiTKNV)
        {
            HoTenNV = hoTenNV;
            Email = email;
            MatKhauNV = matKhauNV;
            TrangThaiTKNV = trangThaiTKNV;
        }

        public NhanVien(string maNV, string hoTenNV, string email, string matKhauNV, string trangThaiTKNV) : this(maNV, hoTenNV, email, matKhauNV)
        {
            TrangThaiTKNV = trangThaiTKNV;
        }
    }
}