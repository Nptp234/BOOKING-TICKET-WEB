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

            List<Phim> lsPhim = singletonPhim.CreatePhim();

            DateTime now = DateTime.Now.Date;

            foreach (Phim phim in lsPhim)
            {
                // Chuyển đổi ngày công chiếu sang kiểu DateTime
                DateTime ngayCongChieu = convertDateTime.ConvertToDateTime(phim.NgayCongChieu);

                // So sánh ngày công chiếu với ngày hiện tại
                if (ngayCongChieu == now)
                {
                    dsPhim.Add(phim);
                }
            }

            return dsPhim;
        }
    }
}