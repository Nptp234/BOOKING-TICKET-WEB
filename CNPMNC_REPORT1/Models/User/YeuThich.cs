using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.User
{
    public class YeuThich
    {
        public string MaPhim { get; set; }
        public string MaKH { get; set; }
        public Phim ThongTinPhim { get; set; }

        public YeuThich() { }

        public YeuThich(Phim phim)
        {
            ThongTinPhim = phim;
        }

        public YeuThich(string maPhim)
        {
            MaPhim = maPhim;
        }

        public YeuThich(string maPhim, string maKH)
        {
            MaPhim = maPhim;
            MaKH = maKH;
        }

        public YeuThich(string maPhim, string maKH, Phim phim)
        {
            MaPhim = maPhim;
            MaKH = maKH;
            ThongTinPhim = phim;
        }
    }
}