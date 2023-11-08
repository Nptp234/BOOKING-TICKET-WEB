using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models
{
    public class Phim
    {
		string maPhim;
		string tenPhim;
		string tomTatP;
        string ngayCongChieu;
        string thoiLuongP;
        string luotMua;
        string luotThich;
        string hinhAnh;
        string trailer;
        string giaPhim;
        string maGHT;

        public Phim()
        {
        }

        public string MaPhim { get => maPhim; set => maPhim = value; }
        public string TenPhim { get => tenPhim; set => tenPhim = value; }
        public string TomTatP { get => tomTatP; set => tomTatP = value; }
        public string NgayCongChieu { get => ngayCongChieu; set => ngayCongChieu = value; }
        public string ThoiLuongP { get => thoiLuongP; set => thoiLuongP = value; }
        public string LuotMua { get => luotMua; set => luotMua = value; }
        public string LuotThich { get => luotThich; set => luotThich = value; }
        public string HinhAnh { get => hinhAnh; set => hinhAnh = value; }
        public string Trailer { get => trailer; set => trailer = value; }
        public string GiaPhim { get => giaPhim; set => giaPhim = value; }
        public string MaGHT { get => maGHT; set => maGHT = value; }
    }
}