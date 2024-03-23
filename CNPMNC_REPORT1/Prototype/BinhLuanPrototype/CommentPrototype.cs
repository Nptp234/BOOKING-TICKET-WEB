using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Prototype.BinhLuanPrototype
{
    public abstract class ICommentPrototype
    {
        public string TenTK { get; set; }
        public string NgayTao { get; set; }
        public string GhiChu { get; set; }
        public string MaPhim { get; set; }
        public string MaBL { get; set; }

        public abstract ICommentPrototype Clone();
        public abstract string Render();
    }
}