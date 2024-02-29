using CNPMNC_REPORT1.Models.PhongChieuPhim;
using CNPMNC_REPORT1.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryRoomType
{
    public class CreateAllLPC : RoomTypeFactory
    {
        public override List<LoaiPC> CreateLPC()
        {
            List<LoaiPC> dsLPC = new List<LoaiPC>();

            SingletonRoomType singletonLPC = SingletonRoomType.Instance;
            List<LoaiPC> lsLPC = singletonLPC.CreateLPC();

            foreach (LoaiPC lpc in lsLPC)
            {
                dsLPC.Add(lpc);
            }

            return dsLPC;
        }
    }
}