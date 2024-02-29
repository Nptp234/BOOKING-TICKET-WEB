using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models
{
    public class LoaiPhim
    {
        public string MaTL { get; set; }
        public string TenTL { get; set; }
        public string MoTaTL { get; set; }

        public LoaiPhim() { }

        public LoaiPhim(string tenTL, string moTaTL)
        {
            TenTL = tenTL;
            MoTaTL = moTaTL;
        }

        public LoaiPhim(string maTL, string tenTL, string moTaTL)
        {
            MaTL = maTL;
            TenTL = tenTL;
            MoTaTL = moTaTL;
        }
    }
}