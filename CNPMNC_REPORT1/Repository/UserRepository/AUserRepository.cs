using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.SQLData;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Repository.UserRepository
{
    public abstract class AUserRepository
    {
        protected SQLUser sqlUser;
        protected UserAccount user;
        protected KhachHang kh;
        protected NhanVien nv;

        public abstract bool Login(string name, string pass);
        public abstract bool Logup(string name, string pass, string email);
        public abstract List<T> GetList();

        public bool Logout()
        {
            SQLUser user = SQLUser.Instance;
            user.SetKH(null);
            user.SetNV(null);
            return user.KH == null && user.NV == null;
        }
    }
}