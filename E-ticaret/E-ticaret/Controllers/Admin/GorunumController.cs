using EticaretSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EticaretSitesi.Controllers.Admin
{
    public class GorunumController : Controller
    {
        // GET: Gorunum
        EticaretContext db = new EticaretContext();
        public ActionResult Index()
        {
            List<Slider> slider = db.Slider.ToList();
            return View(slider);
        }
        public ActionResult Duzenle(int id)
        {
            Slider slider = db.Slider.Where(x => x.resimID == id).SingleOrDefault();
            if (slider != null)
            {
                return View(slider);
            }
            TempData["Hata"] = "Böyle bir slider resmi yoktur";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Duzenle(HttpPostedFileBase gelenResim, Slider form)
        {
            Slider sliderdb = db.Slider.Where(x => x.resimID == form.resimID).SingleOrDefault();
            if (ModelState.IsValid == false) // validation hatası varsa
            {
                return View(sliderdb);
            }
            Resim resim = new Resim();
            if (sliderdb != null)
            {
                if (gelenResim != null)
                {
                    string deger = resim.Ekle(gelenResim, "/Content/Resimler/Slider/");
                    if (deger == "uzanti")
                    {
                        ViewBag.Hata = "Resim uzantısı jpg ve png den başka olamaz";
                        return View(sliderdb);
                    }
                    if (deger == "boyut")
                    {
                        ViewBag.Hata = "Resmin boyutu maksimum 3MB olabilir";
                        return View(sliderdb);
                    }
                    sliderdb.resimAd = deger;
                }
                else
                {
                    sliderdb.resimAd = "default.jpg";
                }
                sliderdb.aciklama = form.aciklama.ToUpper();
                db.SaveChanges();
                TempData["Basari"] = "Slider resmi ve açıklaması başarı ile güncellenmiştir";
            }
            return RedirectToAction("Index");
        }
    }
}