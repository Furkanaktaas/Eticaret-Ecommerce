using EticaretSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EticaretSitesi.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: Anasayfa
        EticaretContext db = new EticaretContext();
        public ActionResult Index()
        {
            UrunGosterAnasayfa gosterAnasayfa = new UrunGosterAnasayfa();
            gosterAnasayfa.urunResimler = db.UrunResim.ToList();
            gosterAnasayfa.urunler = db.Urun.OrderByDescending(x => x.urunID).Take(6).ToList();
            gosterAnasayfa.slider = db.Slider.ToList();
            return View(gosterAnasayfa);
        }
        public ActionResult Cikis()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}