using EticaretSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EticaretSitesi.Controllers
{
    public class AdminController : Controller
    {
        EticaretContext db = new EticaretContext();
        // GET: Admin
        public ActionResult Index()
        {
            Session["KullaniciSayisi"] = db.Kullanici.Count().ToString();
            Session["TalepSayisi"] = db.Satis.Where(x => x.siparisDurumID == 1).Count().ToString();
            Session["UrunSayisi"] = db.Urun.Count().ToString();
            Session["SatisSayisi"] = db.Satis.Count().ToString();
            Session["AktifKullanici"] = db.Kullanici.Where(x => x.durum == true).Count().ToString();
            return View();
        }
        public ActionResult Cikis()
        {
            Session.Abandon();
            return RedirectToAction("Index","AdminLogin");
        }

    }
}