using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.VePhim;
using CNPMNC_REPORT1.SQLData;
using CNPMNC_REPORT1.SQLFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNPMNC_REPORT1.Command.CommandDV
{
    public class ThanhToanVeCommand : CommandInterface
    {
        SQLUser user = SQLUser.Instance;
        SQLData123 data = new SQLData123();
        SQLVePhim vePhim;

        ReceiverDV Receiver;

        string MaVe { get; set; }
        string TongGia { get; set; }
        string ThanhTien { get; set; }

        public ThanhToanVeCommand(string maVe, string tongGia, string thanhTien)
        {
            this.MaVe = maVe;
            this.TongGia = tongGia;
            this.ThanhTien = thanhTien;
            vePhim = new SQLVePhim();
        }

        public ThanhToanVeCommand(ReceiverDV receiver, string maVe, string tongGia, string thanhTien)
        {
            this.MaVe = maVe;
            this.TongGia = tongGia;
            this.ThanhTien = thanhTien;
            vePhim = new SQLVePhim();
            Receiver = receiver;
        }

        public void ExecuteCommand()
        {
            Receiver.ThanhToanVeP(MaVe, TongGia, ThanhTien);
        }

    }
}