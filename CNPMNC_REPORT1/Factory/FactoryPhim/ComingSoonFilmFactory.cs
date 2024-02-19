﻿using CNPMNC_REPORT1.Models;
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

            DateTime now = DateTime.Now.Date;

            foreach (Phim phim in lsPhim)
            {
                // Chuyển đổi ngày công chiếu sang kiểu DateTime
                DateTime ngayCongChieu = convertDateTime.ConvertToDateTime(phim.NgayCongChieu);

                // So sánh ngày công chiếu với ngày hiện tại
                if (ngayCongChieu > now)
                {
                    dsPhim.Add(phim);
                }
            }

            return dsPhim;
        }
    }
}