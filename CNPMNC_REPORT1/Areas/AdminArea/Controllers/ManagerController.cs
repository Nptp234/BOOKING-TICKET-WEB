using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNPMNC_REPORT1.Areas.AdminArea.Controllers
{
    public class ManagerController : Controller
    {
        NhanVienRepository nvRepo;

        // GET: AdminArea/Manager
        public ActionResult Index()
        {
            ViewBag.ThongBao = TempData["ThongBao"];
            return View();
        }

        public ActionResult AddStaffForm()
        {
            nvRepo = new NhanVienRepository();
            ViewBag.DSLNV = nvRepo.LayDSLNV();
            return View();
        }

        public ActionResult AddStaff(string HoTenNV, string Email, string MatKhauNV, string Status, string Type)
        {
            nvRepo = new NhanVienRepository();

            if (HoTenNV == null || Email == null || MatKhauNV == null || Status == null || Type == null)
            {
                TempData["ThongBao"] = "Input was null!";
                return RedirectToAction("Index", "Manager");
            }

            NhanVien nv = new NhanVien();
            nv.HoTenNV = HoTenNV;
            nv.Email = Email;
            nv.MatKhauNV = MatKhauNV;
            nv.TrangThaiTKNV = Status;

            nvRepo.ThemNV(nv, Type);

            return RedirectToAction("Index", "Manager");
        }
    }
}