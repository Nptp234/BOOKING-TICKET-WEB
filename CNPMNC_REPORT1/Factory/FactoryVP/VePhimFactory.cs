using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryVP
{
    public abstract class VePhimFactory
    {
        public static List<VeP> allVeP{ get; set; }

        public abstract List<VeP> CreateVeP();
    }
}