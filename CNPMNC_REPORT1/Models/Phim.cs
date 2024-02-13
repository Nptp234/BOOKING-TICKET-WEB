using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models
{
    public class Phim
    {
        public string MaPhim { get; set; }
        public string TenPhim { get; set; }
        public string TomTatP { get; set; }
        public string NgayCongChieu { get; set; }
        public string ThoiLuongP { get; set; }
        public string LuotMua { get; set; }
        public string LuotThich { get; set; }
        public string HinhAnh { get; set; }
        public string Trailer { get; set; }
        public string GiaPhim { get; set; }
        public string MaGHT { get; set; }
        public string TenGHTP { get; set; }

        public Phim() { }

        public Phim(string tenPhim, string tomTatP, string ngayCongChieu, string thoiLuongP, string luotMua, string luotThich, string hinhAnh, string trailer, string giaPhim, string maGHT)
        {
            TenPhim = tenPhim;
            TomTatP = tomTatP;
            NgayCongChieu = ngayCongChieu;
            ThoiLuongP = thoiLuongP;
            LuotMua = luotMua;
            LuotThich = luotThich;
            HinhAnh = hinhAnh;
            Trailer = trailer;
            GiaPhim = giaPhim;
            MaGHT = maGHT;
            TenGHTP = "";
        }

        public Phim(string maPhim, string tenPhim, string tomTatP, string ngayCongChieu, string thoiLuongP, string luotMua, string luotThich, string hinhAnh, string trailer, string giaPhim, string maGHT)
        {
            MaPhim = maPhim;
            TenPhim = tenPhim;
            TomTatP = tomTatP;
            NgayCongChieu = ngayCongChieu;
            ThoiLuongP = thoiLuongP;
            LuotMua = luotMua;
            LuotThich = luotThich;
            HinhAnh = hinhAnh;
            Trailer = trailer;
            GiaPhim = giaPhim;
            MaGHT = maGHT;
            TenGHTP = "";
        }
    }
}