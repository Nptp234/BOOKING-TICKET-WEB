using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models
{
    public class VeP
    {
        public string MaVe { get; set; }
        public string NgayDat { get; set; }
        public string TrangThaiThanhToan { get; set; }
        public string TrangThaiHetHan { get; set; }
        public string SLGhe { get; set; }
        public string GiaVe { get; set; }
        public string MaLC { get; set; }
        public string MaKH { get; set; }

        public VeP() { }

        public VeP(string ngayDat, string trangThaiThanhToan, string trangThaiHetHan, string sLGhe, string giaVe, string maLC, string maKH)
        {
            NgayDat = ngayDat;
            TrangThaiThanhToan = trangThaiThanhToan;
            TrangThaiHetHan = trangThaiHetHan;
            SLGhe = sLGhe;
            GiaVe = giaVe;
            MaLC = maLC;
            MaKH = maKH;
        }

        public VeP(string maVe, string ngayDat, string trangThaiThanhToan, string trangThaiHetHan, string sLGhe, string giaVe, string maLC, string maKH)
        {
            MaVe = maVe;
            NgayDat = ngayDat;
            TrangThaiThanhToan = trangThaiThanhToan;
            TrangThaiHetHan = trangThaiHetHan;
            SLGhe = sLGhe;
            GiaVe = giaVe;
            MaLC = maLC;
            MaKH = maKH;
        }
    }
}