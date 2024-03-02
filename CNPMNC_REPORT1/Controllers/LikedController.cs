using CNPMNC_REPORT1.Factory;
using CNPMNC_REPORT1.Factory.FactoryPhim;
using CNPMNC_REPORT1.Factory.FactoryYT;
using CNPMNC_REPORT1.Models;
using CNPMNC_REPORT1.Models.User;
using CNPMNC_REPORT1.Observer;
using CNPMNC_REPORT1.Observer.Object;
using CNPMNC_REPORT1.SQLData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNPMNC_REPORT1.Controllers
{
    public class LikedController : Controller
    {
        // GET: Liked
        public ActionResult Index()
        {
            YeuThichFactory ytFactory = new CreateListLikedUser();
            ViewBag.YeuThich = ytFactory.CreateYT();

            return View();
        }

        public ActionResult RemoveFilmLiked(string MaPhim)
        {
            YeuThich yt = new YeuThich();
            var subject = new SubjectObserver();
            var ytObserver = new YeuThichObserver();

            SQLUser user = SQLUser.Instance;

            yt.MaPhim = MaPhim;
            yt.MaKH = user.KH.MaKH;

            subject.Attach(ytObserver);

            subject.Notify(yt, ActionType.Remove);

            return RedirectToAction("Index", "Liked");
        }
    }
}