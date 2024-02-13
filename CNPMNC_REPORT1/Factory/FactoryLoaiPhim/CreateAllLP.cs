using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryLoaiPhim
{
    public class CreateAllLP : LoaiPhimFactory
    {
        public override List<LoaiPhim> CreateLoaiP()
        {
            List<LoaiPhim> dsLoaiP = new List<LoaiPhim>();

            SingletonLP singletonLP = SingletonLP.Instance;
            List<LoaiPhim> lsLP = singletonLP.CreateLoaiP();

            foreach (LoaiPhim lp in lsLP)
            {
                dsLoaiP.Add(lp);
            }

            return dsLoaiP;
        }
    }
}