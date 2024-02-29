using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.PhongChieuPhim
{
    public class PhongChieu
    {
        public string MaPC { get; set; }
        public string TenPC { get; set; }
        public string SLGheThuong { get; set; }
        public string SLGheVIP { get; set; }
        public string MaLPC { get; set; }

        public PhongChieu() { }

        public PhongChieu(string tenPC, string sLGheThuong, string sLGheVIP, string maLPC)
        {
            TenPC = tenPC;
            SLGheThuong = sLGheThuong;
            SLGheVIP = sLGheVIP;
            MaLPC = maLPC;
        }

        public PhongChieu(string maPC, string tenPC, string sLGheThuong, string sLGheVIP, string maLPC)
        {
            MaPC = maPC;
            TenPC = tenPC;
            SLGheThuong = sLGheThuong;
            SLGheVIP = sLGheVIP;
            MaLPC = maLPC;
        }
    }
}