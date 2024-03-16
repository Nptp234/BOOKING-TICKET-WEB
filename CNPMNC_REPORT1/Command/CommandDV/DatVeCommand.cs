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

        public void ExecuteCommand()
        {
            string getUsername = sQLUser.KH.TenTKKH;

            // total_price sẽ có dữ liệu là " ... VND", nên cần phải tách chuỗi ra dựa theo khoảng trắng và lấy kí tự đầu tiên
            db.getData($"INSERT INTO VEPHIM VALUES ('{DateTime.Now.ToString()}', N'CHƯA THANH TOÁN', N'CHƯA HẾT HẠN', {SLVe}, {Convert.ToDouble(Total_price.Split(' ')[0].Replace(".", ""))}, {Malc}, {sQLUser.KH.MaKH});");
            vePs = sQLve.LayVePhimLonNhat(sQLUser.KH.MaKH);
            string idVePhim = vePs[0].MaVe.ToString();

            List<string> getListGhe = ListGhe.Split(' ').ToList();
            foreach (var item in getListGhe)
            {
                db.getData($"INSERT INTO VE_GHE VALUES ({idVePhim}, '{item}', N'CHƯA THANH TOÁN')");
            }
        }
    }
}
