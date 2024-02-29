using CNPMNC_REPORT1.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryLoaiKH
{
    public abstract class LoaiKHFactory
    {
        public static List<LoaiKH> allLKH { get; set; }
        public abstract List<LoaiKH> CreateLoaiKH();
    }
}