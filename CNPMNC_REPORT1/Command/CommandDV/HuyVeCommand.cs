using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.SQLData;
using CNPMNC_REPORT1.SQLFolder;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CNPMNC_REPORT1.Command.CommandDV
{
    public class HuyVeCommand : CommandInterface
    {
        SQLUser user = SQLUser.Instance;

        SQLVePhim vePhim;

        ReceiverDV Receiver;

        string MaVe {  get; set; }

        public HuyVeCommand(string mave)
        { 
            this.MaVe = mave;
            vePhim = new SQLVePhim();
        }

        public HuyVeCommand(ReceiverDV receiver, string mave)
        {
            this.MaVe = mave;
            vePhim = new SQLVePhim();
            Receiver = receiver;
        }

        public void ExecuteCommand()
        {
            Receiver.HuyVe(MaVe);
        }

        public string ThongBao(string text)
        {
            return text;
        }
    }
}
