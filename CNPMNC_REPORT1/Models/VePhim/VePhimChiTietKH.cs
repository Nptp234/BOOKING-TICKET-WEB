using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.VePhim
{
    public class VePhimChiTietKH
    {
        public string MaVe { get; set; }
        public string TenPhim { get; set; }
        public string TenPC { get; set; }
        public string TenGheVG { get; set; }
        public string NgayDat { get; set; }
        public string NgayLC { get; set; }
        public string GioXC { get; set; }

        public VePhimChiTietKH() { }

        public VePhimChiTietKH(string maVe, string tenPhim, string tenPC, string tenGheVG, string ngayDat, string ngayLC, string gioXC)
        {
            MaVe = maVe;
            TenPhim = tenPhim;
            TenPC = tenPC;
            TenGheVG = tenGheVG;
            NgayDat = ngayDat;
            NgayLC = ngayLC;
            GioXC = gioXC;
        }
    }
}