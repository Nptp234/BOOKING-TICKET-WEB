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
using CNPMNC_REPORT1.Factory.FactoryYT;
using CNPMNC_REPORT1.Repository.UserRepository;
using CNPMNC_REPORT1.SQLFolder;
using CNPMNC_REPORT1.Repository.VePRepository;

namespace CNPMNC_REPORT1.Controllers
{
    public class HomeController : Controller
    {
        PhimFactory factoryPhim;
        BinhLuanFactory factoryBL;
        GHTFactory factoryGHT;
        SQLData123 db = new SQLData123();
        private readonly static SubjectObserver subject = new SubjectObserver();
        KhachHangRepository khachHangRepository;
        NhanVienRepository nhanVienRepository;

        public ActionResult Index(string Logout)
        {
            khachHangRepository = new KhachHangRepository();

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

            if (khachHangRepository.GetKH()!=null)
            {
                YeuThichFactory ytFactory = new CreateListLikedUser();
                ViewBag.YeuThich = ytFactory.CreateYT();
            }


            return View();
        }

        public ActionResult Logout()
        {
            khachHangRepository = new KhachHangRepository();
            Session["isLogined"] = "false";
            Session["Username"] = null;

            if (khachHangRepository.Logout())
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("LoginPage", "Home");
            }

        }

        public ActionResult LoginPage(string Username, string Password, string status)
        {
            khachHangRepository = new KhachHangRepository();

            Session["isLoginedQL"] = "false";

            if (Username != null && Password != null)
            {
                bool check = khachHangRepository.Login(Username, Password);

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
            nhanVienRepository = new NhanVienRepository();
            Session["isLoginedQL"] = "false";

            if (nhanVienRepository.Login(Username, Password))
            {
                return RedirectToAction("IndexNull", "Admin", new { area = "AdminArea" });
            }
            else
            {
                ViewBag.ThongBao = "Error Login!";
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
            khachHangRepository = new KhachHangRepository();

            if (Username != null && Password != null && Email != null)
            {
                if (khachHangRepository.Logup(Username, Password, Email))
                {
                    Session["isLogined"] = "true";
                    Session["Username"] = Username;

                    return RedirectToAction("Index", "Home");
                }
            }
            
            return View();
        }

        public ActionResult AccountPage()
        {
            khachHangRepository = new KhachHangRepository();
            AVePRepository vePRepository = new AVePRepository();

            KhachHang kh = khachHangRepository.GetKH();

            ViewBag.GetDate = vePRepository.LayTTVePTuMaKH(kh.MaKH);

            ViewBag.GetVP = vePRepository.LayVePhimChiTietKH(kh.TenTKKH);

            ViewBag.ThongBao = TempData["ThongBao"];

            return View(kh);
        }
        [HttpPost]
        public ActionResult AccountPage(string TenTK, string Email, string Pass)
        {
            khachHangRepository = new KhachHangRepository();

            khachHangRepository.CapNhatKH(TenTK, Pass, Email);

            return RedirectToAction("AccountPage", "Home");
        }

        //FILM ZONE

        public ActionResult FilmDetail(string MaPhim)
        {
            if (MaPhim != null)
            {
                Console.OutputEncoding = Encoding.GetEncoding("UTF-8");

                var blObserver = new BinhLuanObserver();
                subject.Attach(blObserver);

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
            BinhLuan bl = new BinhLuan();

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