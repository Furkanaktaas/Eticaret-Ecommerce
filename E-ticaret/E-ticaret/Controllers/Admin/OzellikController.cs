using EticaretSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EticaretSitesi.Controllers.Admin
{
    public class OzellikController : Controller
    {
        EticaretContext db = new EticaretContext();
        public static int ozelid;
        // GET: Ozellik
        public ActionResult Index()
        {
            List<OzellikTip> ozelliktip = db.OzellikTip.ToList();
            return View(ozelliktip);
        }
        public ActionResult Sil(int id)
        {
            OzellikTip ozellikTip = db.OzellikTip.Where(x => x.ozellikTipID == id).SingleOrDefault();
            if (ozellikTip != null && ozellikTip.ozellikTipID != 1)
            {
                List<OzellikDeger> ozellikDeger = db.OzellikDeger.ToList();
                foreach (var ozellik in ozellikDeger)
                {
                    if (ozellik.ozellikTipID == ozellikTip.ozellikTipID)
                    {
                        ozellik.ozellikTipID = 1;
                        db.SaveChanges();
                    }
                }
                List<UrunOzellik> urunOzellik = db.UrunOzellik.ToList();
                foreach (var urunOzellikleri in urunOzellik)
                {
                    if (urunOzellikleri.ozellikTipID == ozellikTip.ozellikTipID)
                    {
                        UrunOzellik yeniUrun = new UrunOzellik();
                        yeniUrun.urunID = urunOzellikleri.urunID;
                        yeniUrun.ozellikDegerID = urunOzellikleri.ozellikDegerID;
                        yeniUrun.ozellikTipID = 1;
                        db.UrunOzellik.Remove(urunOzellikleri);
                        db.SaveChanges();
                        db.UrunOzellik.Add(yeniUrun);
                        db.SaveChanges();                     
                    }
                }
                db.OzellikTip.Remove(ozellikTip);
                db.SaveChanges();
                TempData["Basari"] = "Özellik Başarı ile Silinmiştir";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            var kategori = db.Kategori.ToList();
            ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(OzellikTip ozellik)
        {
            var kategori = db.Kategori.ToList();
            OzellikTip ozellikTip = db.OzellikTip.Where(x => x.ad == ozellik.ad.ToUpper() && x.kategoriID == ozellik.kategoriID).SingleOrDefault();
            if (ModelState.IsValid == false)
            {
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View();
            }
            if (ozellikTip != null && ozellikTip.kategoriID == ozellik.kategoriID)
            {
                ViewBag.Hata = "Bu Kategoride Bu Özellik Adı Zaten Mevcut";
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View();
            }
            if(ozellik.kategoriID==1)
            {
                ViewBag.Hata = "Kategorisizlere Özellik Eklenemez";
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View();
            }
            ozellik.ad = ozellik.ad.ToUpper();
            db.OzellikTip.Add(ozellik);
            db.SaveChanges();
            TempData["Basari" +
                ""] = "Özellik Başarı ile Eklenmiştir";
            return RedirectToAction("Index");
        }
        public ActionResult Duzenle(int id)
        {
            OzellikTip ozellikTip = db.OzellikTip.Where(x=> x.ozellikTipID == id).SingleOrDefault();
            if(ozellikTip !=null && ozellikTip.ozellikTipID != 1)
            {
                ozelid = id;
                var kategori = db.Kategori.ToList();
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View(ozellikTip);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Duzenle(OzellikTip ozellik)
        {
            OzellikTip ozellikTip = db.OzellikTip.Where(x => x.ozellikTipID == ozelid).SingleOrDefault();
            var kategori = db.Kategori.ToList();
            if (ModelState.IsValid == false)
            {
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View();
            }
            if (ozellik.kategoriID==1)
            {
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                ViewBag.Hata = "Kategorisizlere Özellik Eklenemez";
                return View();
            }
            if (ozellikTip != null)
            {
                OzellikTip ozellikTip2 = db.OzellikTip.Where(x => x.ad == ozellik.ad && x.kategoriID == ozellik.kategoriID).SingleOrDefault();
                if (ozellikTip2 != null && ozellik.kategoriID == ozellikTip2.kategoriID)
                {
                    ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                    ViewBag.Hata = "Aynı Alt Özellik Aynı Kategoride Mevcut";
                    return View();
                }
                else
                {
                    ozellikTip.ad = ozellik.ad.ToUpper();
                    ozellikTip.kategoriID = ozellik.kategoriID;
                    db.SaveChanges();
                    TempData["Basari"] = "Özellik Başarı ile Düzenlenmiştir";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
            return View();
        }
    }
}