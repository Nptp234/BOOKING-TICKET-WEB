using CNPMNC_REPORT1.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.State
{
    public abstract class UserState
    {
        protected UserAccount _user;
        public UserState(UserAccount user)
        {
            _user = user;
        }
        public abstract void ClickLogin();
        public abstract void ClickLogout();
    }
}