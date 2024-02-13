using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLData
{
    public class SQLUser : SQLObject
    {
        public SQLUser()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public bool KiemTraThongTinDangNhap(string name, string pass, string type)
        {
            string query="";

            switch (type)
            {
                case "KhachHang":
                    query = $"SELECT TenTKKH FROM KHACHHANG WHERE TenTKKH = {name} AND MatKhauKH = {pass};";
                    break;

                case "NhanVien":
                    query = $"SELECT Email FROM NHANVIEN WHERE Email = {name} AND MatKhauNV = {pass};";
                    break;

                default: return false;
            }

            bool check = ThucHienTruyVan(query);
            if (check)
            {
                return true;
            }
            else return false;
        }
    }
}