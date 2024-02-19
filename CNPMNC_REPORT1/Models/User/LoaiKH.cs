using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.User
{
    public class LoaiKH
    {
        public string MaLoaiKH { get; set; }
        public string TenLKH { get; set; }
        public string ChietKhau { get; set; }

        public LoaiKH() { }

        public LoaiKH(string tenLKH, string chietKhau)
        {
            TenLKH = tenLKH;
            ChietKhau = chietKhau;
        }

        public LoaiKH(string maLoaiKH, string tenLKH, string chietKhau)
        {
            MaLoaiKH = maLoaiKH;
            TenLKH = tenLKH;
            ChietKhau = chietKhau;
        }
    }
}