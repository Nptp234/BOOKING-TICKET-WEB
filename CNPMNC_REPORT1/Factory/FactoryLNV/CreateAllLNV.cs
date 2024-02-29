using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryLNV
{
    public class CreateAllLNV : LNVFactory
    {
        public override List<LoaiNV> CreateLNV()
        {
            List<LoaiNV> ds = new List<LoaiNV>();

            SingletonLNV singleton = SingletonLNV.Instance;
            List<LoaiNV> ls = singleton.CreateLNV();

            foreach (LoaiNV tlp in ls)
            {
                ds.Add(tlp);
            }

            return ds;
        }
    }
}