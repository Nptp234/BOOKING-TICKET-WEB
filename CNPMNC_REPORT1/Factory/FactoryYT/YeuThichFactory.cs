using CNPMNC_REPORT1.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryYT
{
    public abstract class YeuThichFactory
    {
        public static List<YeuThich> allYT { get; set; }

        public abstract List<YeuThich> CreateYT();
    }
}