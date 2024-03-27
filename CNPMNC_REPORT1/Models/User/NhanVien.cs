using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.User
{
    public class NhanVien : UserAccount
    {
        public string MaNV { get; set; }
        public string HoTenNV { get; set; }
        public string Email { get; set; }
        public string MatKhauNV { get; set; }
        public string TrangThaiTKNV { get; set; }
        public string UserType { get { return "NhanVien"; } }
        public string UsernameUS { get { return HoTenNV; } set { UsernameUS = HoTenNV; } }
        public string PasswordUS { get { return MatKhauNV; } set { PasswordUS = MatKhauNV; } }
        public string EmailUS { get { return Email; } set { EmailUS = Email; } }


        public NhanVien() { }

        public NhanVien(string hoTenNV, string email, string matKhauNV, string trangThaiTKNV)
        {
            HoTenNV = hoTenNV;
            Email = email;
            MatKhauNV = matKhauNV;
            TrangThaiTKNV = trangThaiTKNV;
        }

        public NhanVien(string maNV, string hoTenNV, string email, string matKhauNV, string trangThaiTKNV)
        {
            MaNV = maNV;
            HoTenNV = hoTenNV;
            Email = email;
            MatKhauNV = matKhauNV;
            TrangThaiTKNV = trangThaiTKNV;
        }
    }
}