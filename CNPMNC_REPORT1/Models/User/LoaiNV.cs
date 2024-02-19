using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.User
{
    public class LoaiNV
    {
        public string MaLNV { get; set; }
        public string TenLNV { get; set; }

        public LoaiNV() { }

        public LoaiNV(string tenLNV)
        {
            TenLNV = tenLNV;
        }

        public LoaiNV(string maLNV, string tenLNV)
        {
            MaLNV = maLNV;
            TenLNV = tenLNV;
        }

    }
}