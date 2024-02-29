using CNPMNC_REPORT1.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryLNV
{
    public abstract class LNVFactory
    {
        public static List<LoaiNV> allLNV { get; set; }

        public abstract List<LoaiNV> CreateLNV();
    }
}