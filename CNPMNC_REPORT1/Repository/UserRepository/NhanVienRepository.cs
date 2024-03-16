using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.SQLData;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Repository.UserRepository
{
    public class NhanVienRepository : AUserRepository
    {
        public NhanVienRepository()
        {
            sqlUser = SQLUser.Instance;
            nv = new NhanVien();
        }

        public override List<T> GetList()
        {
            throw new NotImplementedException();
        }

        public override bool Login(string name, string pass)
        {
            user = new NhanVien();
            bool check = sqlUser.KiemTraThongTinDangNhap(name, pass, user.UserType);

            return check;
        }


        public override bool Logup(string name, string pass, string email)
        {
            throw new NotImplementedException();
        }
    }
}