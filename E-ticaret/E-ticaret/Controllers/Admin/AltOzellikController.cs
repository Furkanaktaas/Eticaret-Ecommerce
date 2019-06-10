using EticaretSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EticaretSitesi.Controllers.Admin
{
    public class AltOzellikController : Controller
    {
        EticaretContext db = new EticaretContext();
        public static int ozelid;
        // GET: AltOzellik
        public ActionResult Index()
        {
            List<OzellikDeger> ozellikDeger = db.OzellikDeger.ToList();
            return View(ozellikDeger);
        }
        public ActionResult Sil(int id)
        {
            OzellikDeger ozellikDeger = db.OzellikDeger.Where(x => x.ozellikDegerID == id).SingleOrDefault();
            if (ozellikDeger != null)
            {
                List<UrunOzellik> urunOzellik = db.UrunOzellik.ToList();
                foreach (var urunOzellikleri in urunOzellik)
                {
                    if (urunOzellikleri.ozellikDegerID == ozellikDeger.ozellikDegerID)
                    {
                        UrunOzellik yeniUrun = new UrunOzellik();
                        yeniUrun.urunID = urunOzellikleri.urunID;
                        yeniUrun.ozellikTipID = urunOzellikleri.ozellikTipID;
                        yeniUrun.ozellikDegerID = 1;
                        db.UrunOzellik.Remove(urunOzellikleri);
                        db.SaveChanges();
                        db.UrunOzellik.Add(yeniUrun);
                        db.SaveChanges();
                    }
                }
                db.OzellikDeger.Remove(ozellikDeger);
                db.SaveChanges();
            }
            TempData["mesaj"] = "Alt Özellik Başarı ile Silinmiştir";
            return RedirectToAction("Index");
        }
        public ActionResult Ekle()
        {
            var ozellik = db.OzellikTip.ToList();
            ViewBag.OzellikTip = new SelectList(ozellik, "ozellikTipId", "ad");
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(OzellikDeger deger)
        {
            OzellikDeger ozellikDeger = db.OzellikDeger.Where(x => x.ad == deger.ad.ToUpper() && x.ozellikTipID == deger.ozellikTipID).SingleOrDefault();
            if (ModelState.IsValid == false)
            {
                var ozellik = db.OzellikTip.ToList();
                ViewBag.OzellikTip = new SelectList(ozellik, "ozellikTipId", "ad");
                return View();
            }
            if(deger.ozellikTipID==1)
            {
                ViewBag.Hata = "Özelliksizlere Alt Özellik Eklenemez";
                var ozellik = db.OzellikTip.ToList();
                ViewBag.OzellikTip = new SelectList(ozellik, "ozellikTipId", "ad");
                return View();
            }
            if (ozellikDeger != null && ozellikDeger.ozellikTipID == deger.ozellikTipID)
            {
                ViewBag.Hata = "Bu Özellikte Alt Özellik Adı Zaten Mevcut";
                var ozellik = db.OzellikTip.ToList();
                ViewBag.OzellikTip = new SelectList(ozellik, "ozellikTipId", "ad");
                return View();
            }
            deger.ad = deger.ad.ToUpper();
            db.OzellikDeger.Add(deger);
            db.SaveChanges();
            TempData["Mesaj"] ="Alt Özellik Başarı ile Eklenmiştir";
            return RedirectToAction("Index");
        }
        public ActionResult Duzenle(int id)
        {
            OzellikDeger ozellikDeger = db.OzellikDeger.Where(x=> x.ozellikDegerID == id).SingleOrDefault();
            if (ozellikDeger != null && ozellikDeger.ozellikDegerID != 1)
            {
                ozelid = id;
                var ozellik = db.OzellikTip.ToList();
                ViewBag.OzellikTip = new SelectList(ozellik, "ozellikTipId", "ad");
                return View(ozellikDeger);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Duzenle(OzellikDeger deger)
        {
            OzellikDeger ozellikDeger = db.OzellikDeger.Where(x => x.ozellikDegerID == ozelid).SingleOrDefault();
            var ozellik = db.OzellikTip.ToList();
            if (ModelState.IsValid == false)
            {
                ViewBag.OzellikTip = new SelectList(ozellik, "ozellikTipId", "ad");
                return View();
            }
            if (deger.ozellikTipID == 1)
            {
                ViewBag.Hata = "Özelliksizlere Alt Özellik Eklenemez";
                ViewBag.OzellikTip = new SelectList(ozellik, "ozellikTipId", "ad");
                return View();
            }
            if (ozellikDeger != null)
            {
                OzellikDeger ozellikDeger2 = db.OzellikDeger.Where(x => x.ad == deger.ad && x.ozellikTipID == deger.ozellikTipID).SingleOrDefault();
                if (ozellikDeger2 != null && deger.ozellikTipID == ozellikDeger2.ozellikTipID)
                {
                    ViewBag.OzellikTip = new SelectList(ozellik, "ozellikTipId", "ad");
                    ViewBag.Hata = "Aynı Alt Özellik Aynı Kategoride Mevcut";
                    return View();
                }
                else
                {
                    ozellikDeger.ad = deger.ad.ToUpper();
                    ozellikDeger.ozellikTipID = deger.ozellikTipID;
                    db.SaveChanges();
                    TempData["mesaj"] = "Alt Özellik Başarı ile Düzenlenmiştir";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.OzellikTip = new SelectList(ozellik, "ozellikTipId", "ad");
            return View();
        }
    }
}