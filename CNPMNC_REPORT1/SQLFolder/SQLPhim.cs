using CNPMNC_REPORT1.Singleton;
using CNPMNC_REPORT1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNPMNC_REPORT1.Factory;

namespace CNPMNC_REPORT1.SQLData
{
    public class SQLPhim : SQLObject
    {
        public SQLPhim()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public List<Phim> GetList()
        {
            return LayDS<Phim>("SELECT * FROM PHIM;");
        }

        public bool ThemPhim(Phim phim)
        {
            string query = $"INSERT INTO PHIM VALUES(N'{phim.TenPhim}', N'{phim.TomTatP}', '{phim.NgayCongChieu}', '{phim.ThoiLuongP}', '{phim.LuotMua}', '{phim.LuotThich}', '{phim.HinhAnh}', '{phim.Trailer}', '{phim.GiaPhim}', '{phim.MaGHT}')";

            return ThucHienTruyVan(query);
        }

        public bool CapNhatP(Phim phim)
        {
            string query = $"UPDATE PHIM SET TenPhim = N'{phim.TenPhim}', TomTatP = N'{phim.TomTatP}', NgayCongChieu = '{phim.NgayCongChieu}', " +
                $"ThoiLuongP = '{phim.ThoiLuongP}', LuotMua = '{phim.LuotMua}', LuotThich = '{phim.LuotThich}', HinhAnh = '{phim.HinhAnh}', " +
                $"Trailer = '{phim.Trailer}', GiaPhim = '{phim.GiaPhim}', MaGHT = '{phim.MaGHT}' WHERE MaPhim = '{phim.MaPhim}'";
            
            return ThucHienTruyVan(query);
        }

        //xóa có thể không thành công vì lỗi khóa ngoại
        public bool XoaPhim(string MaPhim)
        {
            string query = $"DELETE FROM PHIM WHERE MaPhim = {MaPhim}";
            
            return ThucHienTruyVan(query);
        }

        public string GetMaxIDFilm()
        {
            string query = "SELECT MAX(MAPHIM) FROM PHIM";
            List<Phim> phim = LayDS<Phim>(query);
            return phim[0].MaPhim;
        }

    }
}