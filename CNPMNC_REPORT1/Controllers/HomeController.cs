using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Factory;
using System.Net;
using System.Net.Mail;
using System.Collections;
using Unidecode.NET;
using CNPMNC_REPORT1.Factory.FactoryBL;
using CNPMNC_REPORT1.Factory.FactoryGHT;
using CNPMNC_REPORT1.SQLData;
using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.Factory.FactoryPhim;
using CNPMNC_REPORT1.Observer;

namespace CNPMNC_REPORT1.Controllers
{
    public class HomeController : Controller
    {
        PhimFactory factoryPhim;
        BinhLuanFactory factoryBL;
        GHTFactory factoryGHT;
        SQLData123 db = new SQLData123();

        public ActionResult Index(string Logout)
        {
            SQLConnection sQLConnection = SQLConnection.Instance;

            SingletonPhim singletonPhim = SingletonPhim.Instance;
            singletonPhim.ResetInstance();

            //Lấy danh sách phim đang khởi chiếu
            factoryPhim = new NowShowingFilmFactory();
            ViewBag.FilmNowShowing = factoryPhim.CreatePhim();

            //Lấy danh sách phim sắp khởi chiếu
            factoryPhim = new ComingSoonFilmFactory();
            ViewBag.FilmComingSoon = factoryPhim.CreatePhim();

            //Lấy danh sách phim xem nhiều nhất
            factoryPhim = new MostWatchingFilmFactory();
            ViewBag.FilmMostWatching = factoryPhim.CreatePhim();

            if (Logout == "true")
            {
                Session["isLogined"] = "false";
                Session["Username"] = null;
                return View();
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["isLogined"] = "false";
            Session["Username"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoginPage(string Username, string Password, string status)
        {
            SQLUser sQLUser = SQLUser.Instance;
            UserAccount userAccount;

            Session["isLoginedQL"] = "false";

            if (status == "Check")
            {
                userAccount = new KhachHang();

                bool check = sQLUser.KiemTraThongTinDangNhap(Username, Password, userAccount.UserType);

                if (check)
                {
                    Session["isLogined"] = "true";
                    Session["Username"] = Username;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ThongBao = "Error Login!";
                }
            }

            return View();
        }

        public ActionResult LoginPageNV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPageNV(string Username, string Password, string status)
        {
            SQLUser sQLUser = SQLUser.Instance;
            UserAccount userAccount;

            Session["isLoginedQL"] = "false";

            if (status == "Check")
            {
                userAccount = new NhanVien();

                bool check = sQLUser.KiemTraThongTinDangNhap(Username, Password, userAccount.UserType);

                if (check)
                {
                    Session["isLogined"] = "true";

                    return RedirectToAction("IndexNull", "Admin", new { area = "AdminArea" });
                }
                else
                {
                    ViewBag.ThongBao = "Error Login!";
                }
            }

            return View();
        }

        public ActionResult RegisterPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterPage(string Username, string Password, string Email)
        {
            SQLUser sQLUser = SQLUser.Instance;
            KhachHang kh = new KhachHang();

            if (Username != null && Password != null && Email != null)
            {
                if (sQLUser.KiemTraTenNDKH(Username) && sQLUser.KiemTraEmailNDKH(Email))
                {
                    kh.TenTKKH = Username;
                    kh.MatKhauKH = Password;
                    kh.EmailKH = Email;
                    kh.DiemThuongKH = "0";
                    kh.TrangThaiTKKH = "Actived";
                    kh.MaLoaiKH = "1";

                    if (sQLUser.ThemKH(kh))
                    {
                        Session["isLogined"] = "true";
                        Session["Username"] = kh.TenTKKH;

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            
            return View();
        }

        public ActionResult AccountPage()
        {
            SQLData123 data = new SQLData123();
            SQLUser sQLUser = SQLUser.Instance;
            KhachHang kh = sQLUser.KH;

            ViewBag.GetDate = data.getData($"SELECT vp.NgayDat FROM VEPHIM vp, KHACHHANG kh WHERE kh.MaKH=vp.MaKH AND kh.TenTKKH='{kh.TenTKKH}' GROUP BY vp.NgayDat ORDER BY CONVERT(date, vp.NgayDat) DESC");
            //ViewBag.GetKH = data.getData($"SELECT kh.TenTKKH, kh.EmailKH, kh.MatKhauKH, lkh.TenLKH, lkh.ChietKhau FROM KHACHHANG kh, LOAIKH lkh WHERE kh.MaLoaiKH=lkh.MaLoaiKH AND kh.TenTKKH='{tentk}'");

            ViewBag.GetVP = data.getData($"SELECT vp.MaVe, p.TenPhim, pc.TenPC, STRING_AGG(vg.TenGheVG, ''), vp.NgayDat, lc.NgayLC, xc.GioXC " +
                                        $"FROM VEPHIM vp, KHACHHANG kh, LICHCHIEU lc, PHIM p, PHONGCHIEU pc, VE_GHE vg, XUATCHIEU xc " +
                                        $"WHERE kh.TenTKKH='{kh.TenTKKH}' " +
                                        $"AND vp.MaKH=kh.MaKH " +
                                        $"AND vp.MaLC=lc.MaLC " +
                                        $"AND lc.MaPhim=p.MaPhim " +
                                        $"AND pc.MaPC=lc.MaPC " +
                                        $"AND vp.MaVe=vg.MaVe " +
                                        $"AND lc.MaXC=xc.MaXC " +
                                        $"GROUP BY vp.MaVe, p.TenPhim, pc.TenPC, vp.NgayDat, lc.NgayLC, xc.GioXC " +
                                        $"ORDER BY vp.NgayDat DESC");

            return View(kh);
        }
        [HttpPost]
        public ActionResult AccountPage(string TenTK, string Email, string Pass)
        {
            SQLData123 data = new SQLData123();
            SQLUser sQLUser = SQLUser.Instance;
            KhachHang kh = sQLUser.KH;

            if (Email != null && Pass != null && TenTK != null)
            {
                kh.TenTKKH = TenTK;
                kh.EmailKH = Email;
                kh.MatKhauKH = Pass;

                bool isUpdate = sQLUser.CapNhatKH(kh);

                if (isUpdate)
                {
                    return RedirectToAction("AccountPage", "Home");
                }
                else
                {
                    ViewBag.ThongBao = "Update Fail!";
                }
            }
            else
            {
                ViewBag.ThongBao = "Null Email or Pass!";
            }

            return RedirectToAction("AccountPage", "Home");
        }

        //FILM ZONE

        public ActionResult FilmDetail(string MaPhim)
        {
            if (MaPhim != null)
            {
                Console.OutputEncoding = Encoding.GetEncoding("UTF-8");

                SingletonPhim singletonPhim = SingletonPhim.Instance;
                List<Phim> lsPhim = singletonPhim.CreatePhim();

                int maPhim = lsPhim.FindIndex(x => x.MaPhim == MaPhim);

                if (maPhim != -1)
                {
                    Phim phim = lsPhim[maPhim];

                    //lấy link youtube trailer
                    YoutubeLink youtube = new YoutubeLink();

                    string youtubeLink = phim.Trailer;
                    string embedLink = youtube.GetEmbedLink(youtubeLink);

                    ViewBag.EmbedLink = embedLink;

                    //lấy danh sách bình luận
                    factoryBL = new CreateBLWithFilm(MaPhim);
                    List<BinhLuan> dsBL = factoryBL.CreateBL();

                    ViewBag.BinhLuan = dsBL;

                    //xác định giới hạn tuổi
                    factoryGHT = new CreateAllGHT();
                    List<GioiHanTuoi> dsGHT = factoryGHT.CreateGHT();

                    int number;
                    if (int.TryParse(phim.MaGHT, out number)==false)
                    {
                        return View(phim);
                    }
                    else
                    {
                        int maGHT = int.Parse(phim.MaGHT);
                        foreach (GioiHanTuoi GHT in dsGHT)
                        {
                            if (int.Parse(GHT.MaGHT) == maGHT)
                            {
                                phim.TenGHTP = GHT.TenGHT;
                            }
                        }
                    }

                    return View(phim);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult FilmDetail(string IDPhim, string GhiChu, string IDBL, string status)
        {
            SQLBinhLuan sQLBinhLuan = new SQLBinhLuan();
            BinhLuan bl = new BinhLuan();
            var subject = new SubjectObserver();
            var blObserver = new BinhLuanObserver();

            subject.Attach(blObserver);

            if (status == "Post")
            {
                if (IDPhim != null && GhiChu != null)
                {
                    Console.OutputEncoding = Encoding.GetEncoding("UTF-8");

                    string tentk = Session["Username"].ToString();

                    if (tentk != null)
                    {
                        DateTime now = DateTime.Now;
                        bl.NgayTao = now.ToString();
                        bl.MaPhim = IDPhim;
                        bl.TenTK = tentk;
                        bl.GhiChu = GhiChu;
                        bl.TrangThai = "Active";

                        subject.Notify(bl, ActionType.Add);

                        return RedirectToAction("FilmDetail", "Home", new { MaPhim = IDPhim });
                    }
                    else
                        ViewBag.ThongBao = "Error TenTK = null!";
                }
            } 

            return RedirectToAction("FilmDetail", "Home", new { MaPhim = IDPhim });
        }
        public ActionResult DeleteComment(string maBL, string maPhim)
        {

            BinhLuan bl = new BinhLuan();
            var subject = new SubjectObserver();
            var blObserver = new BinhLuanObserver();

            subject.Attach(blObserver);

            bl.MaBL = maBL;

            subject.Notify(bl, ActionType.Remove);

            return RedirectToAction("FilmDetail", "Home", new { MaPhim = maPhim });
        }




        public ActionResult Error_Page()
        {
            return View();
        }

        public ActionResult PointRule()
        {
            return View();
        }

        public ActionResult SearchResult(string searchValue = "")
        {
            List<Phim> filteredPhimList = new List<Phim>();

            // Lấy danh sách phim có sẵn
            factoryPhim = new CreateAllPhim();
            List<Phim> GetPhimList = factoryPhim.CreatePhim();

            if (searchValue == "")
            {
                filteredPhimList = null;
                return PartialView(filteredPhimList);
            }
            else
            {
                // Lọc danh sách các phim có tên gần giống với searchValue
                filteredPhimList = GetPhimList.Where(p => p.TenPhim.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                return PartialView(filteredPhimList);
            }
        }
    }
}