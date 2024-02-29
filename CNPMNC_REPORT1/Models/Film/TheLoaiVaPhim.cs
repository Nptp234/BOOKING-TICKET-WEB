using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.Film
{
    public class TheLoaiVaPhim
    {
        public string MaTLP { get; set; }
        public string MaPhim { get; set; }
        public string MaTL { get; set; }

        public TheLoaiVaPhim() { }
        public TheLoaiVaPhim(string maPhim, string maTL)
        {
            MaPhim = maPhim;
            MaTL = maTL;
        }
        public TheLoaiVaPhim(string maTLP, string maPhim, string maTL)
        {
            MaTLP = maTLP;
            MaPhim = maPhim;
            MaTL = maTL;
        }
    }
}