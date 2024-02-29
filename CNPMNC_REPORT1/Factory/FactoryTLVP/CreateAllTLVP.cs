using CNPMNC_REPORT1.Models.Film;
using CNPMNC_REPORT1.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryTLVP
{
    public class CreateAllTLVP : TLVPFactory
    {
        public override List<TheLoaiVaPhim> CreateTLVP()
        {
            List<TheLoaiVaPhim> ds = new List<TheLoaiVaPhim>();

            SingletonTLVP singleton = SingletonTLVP.Instance;
            List<TheLoaiVaPhim> ls = singleton.CreateTLVP();

            foreach (TheLoaiVaPhim tlp in ls)
            {
                ds.Add(tlp);
            }

            return ds;
        }
    }
}