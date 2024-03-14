using CNPMNC_REPORT1.Models.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.State
{
    public class LoginState : UserState
    {
        private string _script { get; set; }
        public LoginState(UserAccount user) : base(user) { }

        public override void ClickLogin()
        {
            string userDataJson = JsonConvert.SerializeObject(_user); // Chuyển đối tài khoản người dùng thành chuỗi JSON
            string script = $"localStorage.setItem('user', '{userDataJson}');";
            _script = script;
        }

        public override void ClickLogout()
        {
            throw new NotImplementedException();
        }

        public string ReturnScript()
        {
            return _script;
        }
    }
}