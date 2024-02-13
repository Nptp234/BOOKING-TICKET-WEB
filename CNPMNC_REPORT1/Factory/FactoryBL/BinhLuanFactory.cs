using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNPMNC_REPORT1.Models;

namespace CNPMNC_REPORT1.Factory.FactoryBL
{
    public abstract class BinhLuanFactory
    {
        public static List<BinhLuan> allBL { get; set; }
        public abstract List<BinhLuan> CreateBL();
    }
}