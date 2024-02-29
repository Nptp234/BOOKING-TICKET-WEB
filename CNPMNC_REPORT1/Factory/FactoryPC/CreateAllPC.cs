using CNPMNC_REPORT1.Models.PhongChieuPhim;
using CNPMNC_REPORT1.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryPC
{
    public class CreateAllPC : RoomFactory
    {
        public override List<PhongChieu> CreatePC()
        {
            List<PhongChieu> dsPC = new List<PhongChieu>();

            SingletonPC singletonPC = SingletonPC.Instance;
            List<PhongChieu> lsPC = singletonPC.CreatePC();

            foreach (PhongChieu pc in lsPC)
            {
                dsPC.Add(pc);
            }

            return dsPC;
        }
    }
}