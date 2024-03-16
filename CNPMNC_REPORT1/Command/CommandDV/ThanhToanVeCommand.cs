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

        public void ExecuteCommand()
        {
            string tentk = user.KH.TenTKKH;

            if (TongGia != null && (ThanhTien != null || ThanhTien != "0"))
            {
                if (vePhim.ThanhToanVP(TongGia, tentk))
                {
                    //Lấy danh sách vé phim chưa thanh toán
                    List<VeP> lsVeP = LayDSVPChuaTT(user.KH.MaKH);

                    //Lấy số lượng ghế của từng vé đã đặt
                    List<VeGhe> lsVG = new List<VeGhe>();
                    foreach(var ve in lsVeP)
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
                        themCTHD = vePhim.ThanhToanVG(ve.MaVe, soLuongVeGhe.ToString(), tongTienVe.ToString());

                        //Cập nhật trạng thái thanh toán sau khi thêm hóa đơn
                        if (themCTHD)
                        {
                            vePhim.CapNhatThanhToanVeP(ve.MaVe);
                        }
                    }
                }
            }
        }

        private List<VeP> LayDSVPChuaTT(string makh)
        {
            return vePhim.LayDanhSachVePChuaTTTuMaKH(makh);
        }

        private List<VeGhe> LayDSVGChuaTT(string mave)
        {
            return vePhim.LayDanhSachGheVePhimTuMaVe(mave);
        }

        public string ThongBao(string text)
        {
            return text;
        }
    }
}