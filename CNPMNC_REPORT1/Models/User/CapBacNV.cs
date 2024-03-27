using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.User
{
    public class CapBacNV
    {
        public string MaCB { get; set; }
        public string MaNV { get; set; }

        public CapBacNV() { }
        public CapBacNV(string maCB, string maNV)
        {
            MaCB = maCB;
            MaNV = maNV;
        }
    }
}