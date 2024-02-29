using CNPMNC_REPORT1.Models.PhongChieuPhim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryRoomType
{
    public abstract class RoomTypeFactory
    {
        public static List<LoaiPC> allLPC { get; set; }

        public abstract List<LoaiPC> CreateLPC();
    }
}