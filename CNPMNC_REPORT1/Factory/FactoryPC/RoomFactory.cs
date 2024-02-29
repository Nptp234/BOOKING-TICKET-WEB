using CNPMNC_REPORT1.Models.PhongChieuPhim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryPC
{
    public abstract class RoomFactory
    {
        public static List<PhongChieu> allPC { get; set; }

        public abstract List<PhongChieu> CreatePC();
    }
}