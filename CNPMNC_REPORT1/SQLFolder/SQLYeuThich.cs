using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLFolder
{
    public class SQLYeuThich : SQLObject
    {
        SQLUser user;

        public SQLYeuThich()
        {
            sQLConnection = SQLConnection.Instance;
            user = SQLUser.Instance;
        }

        public List<YeuThich> GetList()
        {
            return LayDS<YeuThich>($"SELECT * FROM YEUTHICH WHERE MaKH = '{user.KH.MaKH}';");
        }

        public bool ThemYT(YeuThich yt)
        {
            string query = $"INSERT INTO YEUTHICH VALUES('{yt.MaPhim}', '{yt.MaKH}')";
            return ThucHienTruyVan(query);
        }

        public bool XoaYT(YeuThich yt)
        {
            string query = $"DELETE FROM YEUTHICH WHERE MaKH = '{yt.MaKH}' AND MaPhim = '{yt.MaPhim}'";
            return ThucHienTruyVan(query);
        }

        public bool KiemTraTrungYT(YeuThich yt)
        {
            string query = $"SELECT * FROM YEUTHICH WHERE MaKH = '{yt.MaKH}' AND MaPhim = '{yt.MaPhim}'";
            List<YeuThich> ls = LayDS<YeuThich>(query);

            if (ls.Count > 0)
            {
                return true;
            }
            else return false;
        }

    }
}