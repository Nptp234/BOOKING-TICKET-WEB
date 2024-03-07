using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory
{
    public class MostWatchingFilmFactory : PhimFactory
    {
        public override List<Phim> CreatePhim()
        {
            int luotMua = 0;
            List<Phim> dsPhim = new List<Phim>();

            SingletonPhim singletonPhim = SingletonPhim.Instance;
            List<Phim> lsPhim = singletonPhim.CreatePhim();
             
            foreach (Phim phim in lsPhim)
            {
                luotMua = int.Parse(phim.LuotMua);
                if (luotMua >= 10)
                {
                    dsPhim.Add(phim);
                }
            }

            return dsPhim;
        }
    }
}