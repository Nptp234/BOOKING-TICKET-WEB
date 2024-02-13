using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.User;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.SQLData
{
    public class SQLUser : SQLObject
    {
        private UserAccount User;

        private static SQLUser instance;
        private static readonly object lockObject = new object();

        private SQLUser()
        {
            sQLConnection = SQLConnection.Instance;
        }

        public static SQLUser Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SQLUser();
                    }
                    return instance;
                }
            }
        }

        private void SetUser(UserAccount user)
        {
            User = user;
        }

        public UserAccount GetUser()
        {
            return User;
        }

        public void ResetInstance()
        {
            instance = null;
        }

        public bool KiemTraThongTinDangNhap(string name, string pass, string type)
        {
            switch (type)
            {
                case "KhachHang":
                    return KiemTraDNKhachHang(name, pass);

                case "NhanVien":
                    return KiemTraDNNhanVien(name, pass);

                default: return false;
            }
        }

        public bool KiemTraDNKhachHang(string name, string pass)
        {
            string query = $"SELECT * FROM KHACHHANG WHERE TenTKKH = '{name}' AND MatKhauKH = '{pass}'";

            List<KhachHang> lsKH = new List<KhachHang>();

            lsKH = LayDS<KhachHang>(query);

            if (lsKH.Count > 0)
            {
                SetUser(lsKH[0]);
                return true;
            }
            else return false;
        }

        public bool KiemTraDNNhanVien(string name, string pass)
        {
            string query = $"SELECT * FROM NHANVIEN WHERE Email = '{name}' AND MatKhauNV = '{pass}'";

            List<NhanVien> lsNV = new List<NhanVien>();

            lsNV = LayDS<NhanVien>(query);

            if (lsNV.Count > 0)
            {
                SetUser(lsNV[0]);
                return true;
            }
            else return false;
        }

    }
}