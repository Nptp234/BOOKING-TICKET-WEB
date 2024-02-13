using CNPMNC_REPORT1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Factory.FactoryBL
{
    public class CreateBLWithFilm : BinhLuanFactory
    {
        protected string MaPhim { get; set; }

        public CreateBLWithFilm(string maPhim)
        {
            this.MaPhim = maPhim;
        }

        public override List<BinhLuan> CreateBL()
        {
            List<BinhLuan> dsBL = new List<BinhLuan>();

            SingletonBL singletonBL = SingletonBL.Instance;
            List<BinhLuan> lsBL = singletonBL.CreateBL();

            foreach (BinhLuan binhLuan in lsBL)
            {
                if (binhLuan.MaPhim.Equals(MaPhim))
                {
                    dsBL.Add(binhLuan);
                }
            }

            return dsBL;
        }
    }
}