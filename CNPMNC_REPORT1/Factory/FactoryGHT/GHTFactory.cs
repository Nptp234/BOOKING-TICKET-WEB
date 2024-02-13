using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryGHT
{
    public abstract class GHTFactory
    {
        public static List<GioiHanTuoi> allGHT { get; set; }
        public abstract List<GioiHanTuoi> CreateGHT();
    }
}