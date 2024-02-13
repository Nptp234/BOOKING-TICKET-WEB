using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.User
{
    public class KhachHang : UserAccount
    {
        public string MaKH { get; set; }
        public string TenTKKH { get; set; }
        public string MatKhauKH { get; set; }
        public string EmailKH { get; set; }
        public string DiemThuongKH { get; set; }
        public string TrangThaiTKKH { get; set; }
        public string MaLoaiKH { get; set; }
        public string UserType { get { return "KhachHang"; } }

        public KhachHang() { }

        public KhachHang(string tenTKKH, string matKhauKH, string emailKH, string diemThuongKH, string trangThaiTKKH, string maLoaiKH)
        {
            TenTKKH = tenTKKH;
            MatKhauKH = matKhauKH;
            EmailKH = emailKH;
            DiemThuongKH = diemThuongKH;
            TrangThaiTKKH = trangThaiTKKH;
            MaLoaiKH = maLoaiKH;
        }

        public KhachHang(string maKH, string tenTKKH, string matKhauKH, string emailKH, string diemThuongKH, string trangThaiTKKH, string maLoaiKH) : this(maKH, tenTKKH, matKhauKH, emailKH, diemThuongKH, trangThaiTKKH)
        {
            MaLoaiKH = maLoaiKH;
        }
    }
}