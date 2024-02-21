using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.PhongChieuPhim
{
    public class LoaiPC
    {
        public string MaLPC { get; set; }
        public string TenLPC { get; set; }
        public string MoTaLPC { get; set; }

        public LoaiPC() { }

        public LoaiPC(string tenLPC, string moTaLPC)
        {
            this.TenLPC = tenLPC;
            this.MoTaLPC = moTaLPC;
        }
  
        public LoaiPC(string maLPC, string tenLPC, string moTaLPC) : this(maLPC, tenLPC)
        {
            MoTaLPC = moTaLPC;
        }
    }
}