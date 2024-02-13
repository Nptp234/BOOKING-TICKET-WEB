using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.User
{
    interface UserAccount
    {
        string UserName { get; set; }
        string UserType { get; }
    }
}