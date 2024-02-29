using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Models
{
    public class GioiHanTuoi
    {
        public string MaGHT { get; set; }
        public string TenGHT { get; set; }
        public string MoTaGHT { get; set; }

        public GioiHanTuoi() { }

        public GioiHanTuoi(string tenGHT, string moTaGHT)
        {
            TenGHT = tenGHT;
            MoTaGHT = moTaGHT;
        }

        public GioiHanTuoi(string maGHT, string tenGHT, string moTaGHT)
        {
            MaGHT = maGHT;
            TenGHT = tenGHT;
            MoTaGHT = moTaGHT;
        }
    }
}