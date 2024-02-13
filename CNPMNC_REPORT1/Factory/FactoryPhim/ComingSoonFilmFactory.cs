using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory
{
    public class ComingSoonFilmFactory : PhimFactory
    {
        ConvertDateTime convertDateTime;

        public override List<Phim> CreatePhim()
        {
            convertDateTime = new ConvertDateTime();
            List<Phim> dsPhim = new List<Phim>();

            SingletonPhim singletonPhim = SingletonPhim.Instance;
            List<Phim> lsPhim = singletonPhim.CreatePhim();

            foreach (Phim phim in lsPhim)
            {
                if (convertDateTime.ConvertToDateTime(phim.NgayCongChieu, "yyyy-MM-dd").Date > DateTime.Now.Date)
                {
                    dsPhim.Add(phim);
                }
            }

            return dsPhim;
        }
    }
}