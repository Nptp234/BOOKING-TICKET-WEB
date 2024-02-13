using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryLoaiPhim
{
    public abstract class LoaiPhimFactory
    {
        public static List<LoaiPhim> allLoaiP { get; set; }
        public abstract List<LoaiPhim> CreateLoaiP();
    }
}