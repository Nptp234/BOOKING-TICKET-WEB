using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.SQLData;
using CNPMNC_REPORT1.SQLFolder;
using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text;
using System.Threading.Tasks;

namespace CNPMNC_REPORT1.Command.CommandDV
{
    public class DatVeCommand : CommandInterface
    {
        SQLData123 db = new SQLData123();
        SQLUser sQLUser = SQLUser.Instance;
        SQLVePhim sQLve = new SQLVePhim();

        List<VeP> vePs;

        ReceiverDV Receiver;

        string SLVe { get; set; }
        string Total_price { get; set; }
        string Malc { get; set; }
        string ListGhe {  get; set; }

        public DatVeCommand(string sLVe, string total_price, string malc, string getlistghe)
        {
            SLVe = sLVe;
            Total_price = total_price;
            Malc = malc;
            this.ListGhe = getlistghe;

            vePs = new List<VeP>();
        }

        public DatVeCommand(ReceiverDV receiver, string sLVe, string total_price, string malc, string getlistghe)
        {
            SLVe = sLVe;
            Total_price = total_price;
            Malc = malc;
            this.ListGhe = getlistghe;

            vePs = new List<VeP>();
            Receiver = receiver;
        }

        public void ExecuteCommand()
        {
            Receiver.DatVe(SLVe, Total_price, Malc, ListGhe);
        }
    }
}
