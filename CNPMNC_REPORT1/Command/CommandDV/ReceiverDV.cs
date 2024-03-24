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
    public class ReceiverDV
    {
        SQLData123 db = new SQLData123();
        SQLUser sQLUser = SQLUser.Instance;
        SQLVePhim sQLve = new SQLVePhim();

        List<VeP> vePs;

        public ReceiverDV()
        {

        }

        public void DatVe(string sLVe, string total_price, string malc, string getlistghe)
        {
            string getUsername = sQLUser.KH.TenTKKH;

            // total_price sẽ có dữ liệu là " ... VND", nên cần phải tách chuỗi ra dựa theo khoảng trắng và lấy kí tự đầu tiên
            db.getData($"INSERT INTO VEPHIM VALUES ('{DateTime.Now.ToString()}', N'CHƯA THANH TOÁN', N'CHƯA HẾT HẠN', {sLVe}, {Convert.ToDouble(total_price.Split(' ')[0].Replace(".", ""))}, {malc}, {sQLUser.KH.MaKH});");
            vePs = sQLve.LayVePhimLonNhat(sQLUser.KH.MaKH);
            string idVePhim = vePs[0].MaVe.ToString();

            List<string> getListGhe = getlistghe.Split(' ').ToList();
            foreach (var item in getListGhe)
            {
                db.getData($"INSERT INTO VE_GHE VALUES ({idVePhim}, '{item}', N'CHƯA THANH TOÁN')");
            }
        }

        public void HuyVe(string mave)
        {
            bool isDelete = sQLve.KiemTraXoaVe(mave);
            if (!isDelete)
            {
                ThongBao("Lỗi xóa!");
            }
        }

        public void ThanhToanVeP(string maVe, string tongGia, string thanhTien)
        {
            string tentk = sQLUser.KH.TenTKKH;

            if (tongGia != null && (thanhTien != null || thanhTien != "0"))
            {
                if (sQLve.ThanhToanVP(tongGia, tentk))
                {
                    //Lấy danh sách vé phim chưa thanh toán
                    List<VeP> lsVeP = LayDSVPChuaTT(sQLUser.KH.MaKH);

                    //Lấy số lượng ghế của từng vé đã đặt
                    List<VeGhe> lsVG = new List<VeGhe>();
                    foreach (var ve in lsVeP)
                    {
                        lsVG.AddRange(LayDSVGChuaTT(ve.MaVe));
                    }

                    //Thêm vào chi tiết hóa đơn
                    bool themCTHD = false;

                    foreach (var ve in lsVeP)
                    {
                        //Lấy số lượng vé ghế
                        //Lấy thành tiền = giá vé * sl vé ghế
                        double tongTienVe = double.Parse(ve.GiaVe);
                        int soLuongVeGhe = 1;

                        //Đếm số vé ghế
                        foreach (var vg in lsVG)
                        {
                            if (vg.MaVe == ve.MaVe)
                            {
                                soLuongVeGhe++;
                            }
                        }

                        //Nhân giá vé với số lượng ghế
                        tongTienVe *= soLuongVeGhe;
                        themCTHD = sQLve.ThanhToanVG(ve.MaVe, soLuongVeGhe.ToString(), tongTienVe.ToString());

                        //Cập nhật trạng thái thanh toán sau khi thêm hóa đơn
                        if (themCTHD)
                        {
                            sQLve.CapNhatThanhToanVeP(ve.MaVe);
                        }
                    }
                }
            }
        }

        public string ThongBao(string text)
        {
            return text;
        }

        private List<VeP> LayDSVPChuaTT(string makh)
        {
            return sQLve.LayDanhSachVePChuaTTTuMaKH(makh);
        }

        private List<VeGhe> LayDSVGChuaTT(string mave)
        {
            return sQLve.LayDanhSachGheVePhimTuMaVe(mave);
        }
    }
}