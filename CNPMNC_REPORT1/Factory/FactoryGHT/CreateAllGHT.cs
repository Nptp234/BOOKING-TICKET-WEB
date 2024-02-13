using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryGHT
{
    public class CreateAllGHT : GHTFactory
    {
        public override List<GioiHanTuoi> CreateGHT()
        {
            List<GioiHanTuoi> dsGHT = new List<GioiHanTuoi>();

            SingletonGHT singletonGHT = SingletonGHT.Instance;
            List<GioiHanTuoi> lsGHT = singletonGHT.CreateGHT();

            foreach (GioiHanTuoi GHT in lsGHT)
            {
                dsGHT.Add(GHT);
            }

            return dsGHT;
        }
    }
}