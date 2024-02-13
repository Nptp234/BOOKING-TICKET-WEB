using CNPMNC_REPORT1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory
{
    public class NowShowingFilmFactory : PhimFactory
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
                // Kiểm tra nếu ngày công chiếu trùng khớp với ngày hiện tại
                if (convertDateTime.ConvertToDateTime(phim.NgayCongChieu, "dd-MM-yyyy") == convertDateTime.ConvertToDateTime(DateTime.Now.Date.ToString(), "dd-MM-yyyy"))
                {
                    dsPhim.Add(phim);
                }
            }

            return dsPhim;
        }
    }
}