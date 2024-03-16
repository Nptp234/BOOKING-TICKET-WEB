using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.Models.VePhim;
using CNPMNC_REPORT1.SQLFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Repository.VePRepository
{
    public class AVePRepository
    {
        protected SQLVePhim sQLVe;

        public AVePRepository()
        {
            sQLVe = new SQLVePhim();
        }

        public List<VeP> LayTTVePTuMaKH(string maKH)
        {
            return sQLVe.LayDanhSachVePhimTuMaKH(maKH);
        }

        public List<VePhimChiTietKH> LayVePhimChiTietKH(string ten)
        {
            return sQLVe.LayVePhimChoTTKH(ten);
        }
    }
}