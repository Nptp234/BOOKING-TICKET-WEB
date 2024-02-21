using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.VePhim
{
    public class VeGhe
    {
        public string MaVG { get; set; }
        public string MaVe { get; set; }
        public string TenGheVG { get; set; }
        public string TrangThaiVG { get; set; }

        public VeGhe() { }

        public VeGhe(string maVe, string tenGheVG, string trangThaiVG)
        {
            MaVe = maVe;
            TenGheVG = tenGheVG;
            TrangThaiVG = trangThaiVG;
        }

        public VeGhe(string maVG, string maVe, string tenGheVG, string trangThaiVG) : this(maVG, maVe, tenGheVG)
        {
            TrangThaiVG = trangThaiVG;
        }
    }
}