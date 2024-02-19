using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.User
{
    public class PhanQuyenNV
    {
        public string MaPQ { get; set; }
        public string MaLNV { get; set; }
        public string MaNV { get; set; }

        public PhanQuyenNV() { }

        public PhanQuyenNV(string maLNV, string maNV)
        {
            MaLNV = maLNV;
            MaNV = maNV;
        }

        public PhanQuyenNV(string maPQ, string maLNV, string maNV)
        {
            MaPQ = maPQ;
            MaLNV = maLNV;
            MaNV = maNV;
        }
    }
}