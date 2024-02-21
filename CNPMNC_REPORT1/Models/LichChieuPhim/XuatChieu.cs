using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.LichChieuPhim
{
    public class XuatChieu
    {
        public string MaXC { get; set; }
        public string CaXC { get; set; }
        public string GioXC { get; set; }

        public XuatChieu() { }

        public XuatChieu(string caXC, string gioXC)
        {
            CaXC = caXC;
            GioXC = gioXC;
        }

        public XuatChieu(string maXC, string caXC, string gioXC) : this(maXC, caXC)
        {
            GioXC = gioXC;
        }
    }
}