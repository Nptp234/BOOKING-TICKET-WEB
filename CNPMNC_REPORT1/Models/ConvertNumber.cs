using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CNPMNC_REPORT1.Models;
using System.Net;
using System.Net.Mail;
namespace CNPMNC_REPORT1.Models
{ 
    public class ConvertNumber
    {
        public static string ConvertNumberToAbbreviation(string value = "0")
        {
            int index = int.Parse(value);
            if (index > 1000000)
            {
                return (index / 1000000).ToString() + "M+";
            }
            else if (index > 1000)
            {
                return (index / 1000).ToString() + "K+";
            }
            else
            {
                return index.ToString();
            }
        }
    }
}