﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models
{
    public class ConvertDateTime
    {
        private DateTime dateTime;
        public ConvertDateTime() { this.dateTime = new DateTime(); }
        public DateTime ConvertToDateTime(string dateString)
        {
            //dateTime = DateTime.ParseExact(dateString, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

            bool success = DateTime.TryParseExact(dateString, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);

            if (success)
            {
                // dateTime chứa giá trị ngày đã được chuyển đổi
                return dateTime;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
    }
}