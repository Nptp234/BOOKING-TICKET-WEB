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
        public SQLYeuThich()
        {
            sQLConnection = SQLConnection.Instance;
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


    }
}