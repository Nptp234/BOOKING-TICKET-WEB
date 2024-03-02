using CNPMNC_REPORT1.Factory.FactoryPhim;
using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryYT
{
    public class CreateListLikedUser : YeuThichFactory
    {
        public override List<YeuThich> CreateYT()
        {
            YeuThich yt = new YeuThich();
            List<YeuThich> ds = new List<YeuThich>();

            SingletonYT singleton = SingletonYT.Instance;
            List<YeuThich> ls = singleton.CreateYT();

            PhimFactory phimFac = new CreateAllPhim();
            List<Phim> lsPhim = phimFac.CreatePhim();

            foreach (YeuThich tlp in ls)
            {
                Phim phim = lsPhim.Find(x => x.MaPhim == tlp.MaPhim);

                yt = new YeuThich(phim);
                ds.Add(yt);
            }

            return ds;
        }
    }
}