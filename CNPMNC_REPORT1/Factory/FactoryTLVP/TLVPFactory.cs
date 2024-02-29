using CNPMNC_REPORT1.Models.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryTLVP
{
    public abstract class TLVPFactory
    {
        public static List<TheLoaiVaPhim> allTLVP { get; set; }

        public abstract List<TheLoaiVaPhim> CreateTLVP();
    }
}