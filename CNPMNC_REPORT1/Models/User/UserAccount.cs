using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models.User
{
    public interface UserAccount
    {
        string UsernameUS { get; set; }
        string PasswordUS { get; set; }
        string EmailUS { get; set; }
        string UserType { get; }
    }
}