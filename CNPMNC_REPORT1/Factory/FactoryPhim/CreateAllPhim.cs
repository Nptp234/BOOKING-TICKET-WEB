using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryPhim
{
    public class CreateAllPhim : PhimFactory
    {
        public override List<Phim> CreatePhim()
        {
            List<Phim> dsPhim = new List<Phim>();

            SingletonPhim singletonP = SingletonPhim.Instance;
            List<Phim> lsPhim = singletonP.CreatePhim();

            foreach (Phim phim in lsPhim)
            {
                dsPhim.Add(phim);
            }

            return dsPhim;
        }
    }
}