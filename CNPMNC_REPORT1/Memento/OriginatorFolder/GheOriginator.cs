using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Memento.OriginatorFolder
{
    public class GheOriginator : IOriginator
    {
        private static GheOriginator instance;
        private List<string> DsGhe { get; set; }

        private GheOriginator()
        {
            DsGhe = new List<string>();
        }

        public static GheOriginator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GheOriginator();
                }
                return instance;
            }
        }

        public void SetListChair(List<string> ds)
        {
            DsGhe = ds;
        }

        public IMemento Save()
        {
            return new GheMemento(DsGhe);
        }

        public List<string> GetList()
        {
            return DsGhe;
        }

        public void ThemGhe(List<string> ten)
        {
            DsGhe.AddRange(ten);
        }

        public void XoaGhe(string ten)
        {
            DsGhe.Remove(ten);
        }
    }
}