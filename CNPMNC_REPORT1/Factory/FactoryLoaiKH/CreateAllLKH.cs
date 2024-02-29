using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryLoaiKH
{
    public class CreateAllLKH : LoaiKHFactory
    {
        public override List<LoaiKH> CreateLoaiKH()
        {
            List<LoaiKH> ds = new List<LoaiKH>();

            SingletonLKH singleton = SingletonLKH.Instance;
            List<LoaiKH> ls = singleton.CreateLoaiKH();

            foreach (LoaiKH tlp in ls)
            {
                ds.Add(tlp);
            }

            return ds;
        }
    }
}