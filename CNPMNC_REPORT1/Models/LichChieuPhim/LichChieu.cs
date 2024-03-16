using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.LichChieuPhim
{
    public class LichChieu
    {
        public string MaLC { get; set; }
        public string NgayLC { get; set; }
        public string TrangThaiLC { get; set; }
        public string SLVeDat { get; set; }
        public string MaXC { get; set; }
        public string MaPC { get; set; }
        public string MaPhim { get; set; }

        public LichChieu() { }

        public LichChieu(string ngayLC, string trangThaiLC, string sLVeDat, string maXC, string maPC, string maPhim)
        {
            NgayLC = ngayLC;
            TrangThaiLC = trangThaiLC;
            SLVeDat = sLVeDat;
            MaXC = maXC;
            MaPC = maPC;
            MaPhim = maPhim;
        }

        public LichChieu(string maLC, string ngayLC, string trangThaiLC, string sLVeDat, string maXC, string maPC, string maPhim)
        {
            MaLC = maLC;
            NgayLC = ngayLC;
            TrangThaiLC = trangThaiLC;
            SLVeDat = sLVeDat;
            MaXC = maXC;
            MaPC = maPC;
            MaPhim = maPhim;
        }
    }
}