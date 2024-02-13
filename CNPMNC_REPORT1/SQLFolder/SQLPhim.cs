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
        SingletonPhim singletonPhim;
        public SQLPhim()
        {
            sQLConnection = SQLConnection.Instance;
            singletonPhim = SingletonPhim.Instance;
        }

        public bool ThemPhim(Phim phim)
        {
            string query = $"INSERT INTO PHIM VALUES({phim.TenPhim}, {phim.TomTatP}, {phim.NgayCongChieu}, {phim.ThoiLuongP}, {phim.LuotMua}, {phim.LuotThich}, {phim.HinhAnh}, {phim.Trailer}, {phim.GiaPhim}, {phim.MaGHT})";
            singletonPhim.ResetInstance();
            return ThucHienTruyVan(query);
        }

        public bool CapNhatP(string MaPhim, string tenCot, string duLieuMoi)
        {
            string query = $"UPDATE PHIM SET {tenCot} = {duLieuMoi} WHERE MaPhim = {MaPhim}";
            singletonPhim.ResetInstance();
            return ThucHienTruyVan(query);
        }

        //xóa có thể không thành công vì lỗi khóa ngoại
        public bool XoaPhim(string MaPhim)
        {
            string query = $"DELETE FROM PHIM WHERE MaPhim = {MaPhim}";
            singletonPhim.ResetInstance();
            return ThucHienTruyVan(query);
        }

    }
}