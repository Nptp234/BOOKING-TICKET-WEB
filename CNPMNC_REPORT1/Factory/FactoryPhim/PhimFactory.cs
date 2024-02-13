using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNPMNC_REPORT1.Models;

namespace CNPMNC_REPORT1.Factory
{
    public abstract class PhimFactory
    {
        public static List<Phim> allPhim { get; set; }

        public abstract List<Phim> CreatePhim();
    }
}