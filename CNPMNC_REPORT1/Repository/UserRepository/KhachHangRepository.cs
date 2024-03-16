using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.SQLData;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Repository.UserRepository
{
    public class KhachHangRepository : AUserRepository
    {

        public KhachHangRepository()
        {
            sqlUser = SQLUser.Instance;
            kh = new KhachHang();
        }

        public override List<T> GetList()
        {
            throw new NotImplementedException();
        }

        public override bool Login(string name, string pass)
        {
            SQLUser sQLUser = SQLUser.Instance;
            UserAccount userAccount;

            userAccount = new KhachHang();

            bool check = sQLUser.KiemTraThongTinDangNhap(name, pass, userAccount.UserType);

            return check;
        }


        public override bool Logup(string name, string pass, string email)
        {
            if (name != null && pass != null && email != null)
            {
                if (sqlUser.KiemTraTenNDKH(name) && sqlUser.KiemTraEmailNDKH(email))
                {
                    kh.TenTKKH = name;
                    kh.MatKhauKH = pass;
                    kh.EmailKH = email;
                    kh.DiemThuongKH = "0";
                    kh.TrangThaiTKKH = "Actived";
                    kh.MaLoaiKH = "1";

                    return sqlUser.ThemKH(kh);
                }
                else return false;
            }
            else return false;
        }

        public KhachHang GetKH()
        {
            return sqlUser.KH;
        }

        public void CapNhatKH(string name, string pass, string email)
        {
            SQLUser sQLUser = SQLUser.Instance;
            KhachHang updateKH = new KhachHang();
            updateKH = sqlUser.KH;
            updateKH.TenTKKH = name;
            updateKH.EmailKH = email;
            updateKH.MatKhauKH = pass;

            if (name != null)
            {
                sQLUser.CapNhatTenNDKH(updateKH);
            }

            if (email != null)
            {
                sQLUser.CapNhatEmailKH(updateKH);
            }

            if (pass != null)
            {
                sQLUser.CapNhatMatKhauKH(updateKH);
            }
        }
    }
}