using EticaretSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EticaretSitesi.Controllers.Admin
{
    public class UrunController : Controller
    {
        EticaretContext db = new EticaretContext();
        public static int urunChange;
        // GET: Urun
        public ActionResult Index()
        {
            List<Urun> urun = db.Urun.ToList();
            return View(urun);
        }
        public ActionResult Ekle()
        {
            UrunEkleModelView model = new UrunEkleModelView();
            model.kategoriler = db.Kategori.ToList();
            model.ozellikDegerler = db.OzellikDeger.ToList();
            model.ozellikTipler = db.OzellikTip.ToList();
            var kategori = db.Kategori.ToList();
            ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
            Session["UrunSayisi"] = db.Urun.Count().ToString();
            return View(model);
        }
        [HttpPost]
        public ActionResult Ekle(Urun urun, int ozellikTip, int ozellikDeger, int k, HttpPostedFileBase gelenResim1, HttpPostedFileBase gelenResim2, HttpPostedFileBase gelenResim3)
        {
            string resim1, resim2, resim3;
            var kategori = db.Kategori.ToList();
            if (ModelState.IsValid == false) // validation hatası varsa
            {
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View();
            }
            if (k == 0)
            {
                ViewBag.Hata = "Lütfen Kategori Seçin";
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View();
            }
            else if (ozellikTip == 0)
            {
                ViewBag.Hata = "Lütfen Özellik Tipini Seçin";
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View();
            }
            else if (ozellikDeger == 0)
            {
                ViewBag.Hata = "Lütfen Alt Özellik Tipini Seçin";
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View();
            }
            Urun urundb = db.Urun.Where(x => x.urunAd.ToLower() == urun.urunAd.ToLower()).SingleOrDefault();
            if (urundb != null)
            {
                ViewBag.Hata = "Bu ürün mevcuttur";
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View();
            }
            Resim resim = new Resim();
            if (gelenResim1 != null)
            {
                string deger = resim.Ekle(gelenResim1, "/Content/Resimler/Urunler/");
                if (deger == "uzanti")
                {
                    ViewBag.Hata = "Resim uzantısı jpg ve png den başka olamaz";
                    ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                    return View();
                }
                if (deger == "boyut")
                {
                    ViewBag.Hata = "Resmin boyutu maksimum 3MB olabilir";
                    ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                    return View();
                }
                resim1 = deger;
            }
            else
            {
                resim1 = "default.png";
            }
            if (gelenResim2 != null)
            {
                string deger = resim.Ekle(gelenResim2, "/Content/Resimler/Urunler/");
                if (deger == "uzanti")
                {
                    ViewBag.Hata = "Resim uzantısı jpg ve png den başka olamaz";
                    ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                    return View();
                }
                if (deger == "boyut")
                {
                    ViewBag.Hata = "Resmin boyutu maksimum 3MB olabilir";
                    ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                    return View();
                }
                resim2 = deger;
            }
            else
            {
                resim2 = "default.png";
            }
            if (gelenResim3 != null)
            {
                string deger = resim.Ekle(gelenResim3, "/Content/Resimler/Urunler/");
                if (deger == "uzanti")
                {
                    ViewBag.Hata = "Resim uzantısı jpg ve png den başka olamaz";
                    ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                    return View();
                }
                if (deger == "boyut")
                {
                    ViewBag.Hata = "Resmin boyutu maksimum 3MB olabilir";
                    ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                    return View();
                }
                resim3 = deger;
            }
            else
            {
                resim3 = "default.png";
            }
            UrunResim resimdb = new UrunResim();
            Urun urunekledb = new Urun();
            urunekledb.urunAd = urun.urunAd.ToUpper();
            urunekledb.urunAciklama = urun.urunAciklama.ToUpper();
            urunekledb.urunFiyat = urun.urunFiyat;
            urunekledb.kategoriID = k;
            urunekledb.durum = true;
            db.Urun.Add(urunekledb);
            db.SaveChanges();
            resimdb.resimAd = resim1;
            resimdb.urunID = urunekledb.urunID;
            db.UrunResim.Add(resimdb);
            db.SaveChanges();
            resimdb.resimAd = resim2;
            resimdb.urunID = urunekledb.urunID;
            db.UrunResim.Add(resimdb);
            db.SaveChanges();
            resimdb.resimAd = resim3;
            resimdb.urunID = urunekledb.urunID;
            db.UrunResim.Add(resimdb);
            db.SaveChanges();
            UrunOzellik ozellikdb = new UrunOzellik();
            ozellikdb.urunID = urunekledb.urunID;
            ozellikdb.ozellikTipID = ozellikTip;
            ozellikdb.ozellikDegerID = ozellikDeger;
            db.UrunOzellik.Add(ozellikdb);
            db.SaveChanges();
            TempData["mesaj"] = "Ürün ekleme başarı ile tamamlanmıştır";
            ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
            return RedirectToAction("Ekle");
        }
        public ActionResult Duzenle(int id)
        {
            urunChange = id;
            Urun urun = db.Urun.Where(x => x.urunID == id).SingleOrDefault();
            if (urun != null)
            {
                UrunEkleModelView model = new UrunEkleModelView();
                model.urun = db.Urun.Where(x => x.urunID == id).SingleOrDefault();
                model.urunResimler = db.UrunResim.Where(x => x.urunID == id).ToList();
                model.urunOzellik = db.UrunOzellik.Where(x=> x.urunID == id).SingleOrDefault();
                var kategori = db.Kategori.ToList();
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Duzenle(Urun urun, int ozellikTip, int ozellikDeger, int k, HttpPostedFileBase gelenResim1, HttpPostedFileBase gelenResim2, HttpPostedFileBase gelenResim3)
        {
            UrunEkleModelView model = new UrunEkleModelView();
            List<UrunResim> urunResimleri = db.UrunResim.Where(x => x.urunID == urunChange).ToList();
            model.urun = db.Urun.Where(x => x.urunID == urunChange).SingleOrDefault();
            model.urunResimler = urunResimleri;
            model.urunOzellik = db.UrunOzellik.Where(x => x.urunID == urunChange).SingleOrDefault();
            var kategori = db.Kategori.ToList();
            string resim1, resim2, resim3;
            if (ModelState.IsValid == false) // validation hatası varsa
            {
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View(model);
            }
            if (k == 0)
            {
                ViewBag.Hata = "Lütfen Kategori Seçin";
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View(model);
            }
            else if (ozellikTip == 0)
            {
                ViewBag.Hata = "Lütfen Özellik Tipini Seçin";
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View(model);
            }
            else if (ozellikDeger == 0)
            {
                ViewBag.Hata = "Lütfen Alt Özellik Tipini Seçin";
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View(model);
            }
            Urun urundb = db.Urun.Where(x => x.urunAd.ToLower() == urun.urunAd.ToLower()).SingleOrDefault();
            if (urundb != null && urundb.urunID != urunChange)
            {
                ViewBag.Hata = "Bu ürün mevcuttur";
                ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                return View(model);
            }
            if (k != 0)
            {
               Kategori kategoriler = db.Kategori.Where(x => x.kategoriID == k).SingleOrDefault();
            }
            Resim resim = new Resim();
            if (gelenResim1 != null)
            {
                string deger = resim.Ekle(gelenResim1, "/Content/Resimler/Urunler/");
                if (deger == "uzanti")
                {
                    ViewBag.Hata = "Resim uzantısı jpg ve png den başka olamaz";
                    ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                    return View(model);
                }
                if (deger == "boyut")
                {
                    ViewBag.Hata = "Resmin boyutu maksimum 3MB olabilir";
                    ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                    return View(model);
                }
                resim1 = deger;
            }
            else
            {
                resim1 = urunResimleri[0].resimAd;
            }
            if (gelenResim2 != null)
            {
                string deger = resim.Ekle(gelenResim2, "/Content/Resimler/Urunler/");
                if (deger == "uzanti")
                {
                    ViewBag.Hata = "Resim uzantısı jpg ve png den başka olamaz";
                    ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                    return View(model);
                }
                if (deger == "boyut")
                {
                    ViewBag.Hata = "Resmin boyutu maksimum 3MB olabilir";
                    ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                    return View(model);
                }
                resim2 = deger;
            }
            else
            {
                resim2 = urunResimleri[1].resimAd;
            }
            if (gelenResim3 != null)
            {
                string deger = resim.Ekle(gelenResim3, "/Content/Resimler/Urunler/");
                if (deger == "uzanti")
                {
                    ViewBag.Hata = "Resim uzantısı jpg ve png den başka olamaz";
                    ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                    return View(model);
                }
                if (deger == "boyut")
                {
                    ViewBag.Hata = "Resmin boyutu maksimum 3MB olabilir";
                    ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");
                    return View(model);
                }
                resim3 = deger;
            }
            else
            {
                resim3 = urunResimleri[2].resimAd;
            }
            urundb = db.Urun.Where(x => x.urunID == urun.urunID).SingleOrDefault();
            urundb.urunAd = urun.urunAd.ToUpper();
            urundb.urunAciklama = urun.urunAciklama.ToUpper();
            urundb.urunFiyat = urun.urunFiyat;
            urundb.kategoriID = k;
            urundb.durum = true;
            db.SaveChanges();
            urunResimleri[0].resimAd = resim1;
            urunResimleri[1].resimAd = resim2;
            urunResimleri[2].resimAd = resim3;
            db.SaveChanges();
            UrunOzellik ozellikdb = db.UrunOzellik.Where(x => x.urunID == urunChange).SingleOrDefault();
            db.UrunOzellik.Remove(ozellikdb);
            db.SaveChanges();
            ozellikdb = new UrunOzellik();
            ozellikdb.urunID = urunChange;
            ozellikdb.ozellikTipID = ozellikTip;
            ozellikdb.ozellikDegerID = ozellikDeger;
            db.UrunOzellik.Add(ozellikdb);
            db.SaveChanges();
            TempData["mesaj"] = "Ürün başarı ile güncellenmiştir";
            ViewBag.Kategori = new SelectList(kategori, "kategoriID", "kategoriAd");

            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult Ozellik(int id)
        {
            var ozellik = db.OzellikTip.Where(x => x.kategoriID == id).ToList();
            List<OzellikTip> liste = new List<OzellikTip>();
            foreach (var t in ozellik)
            {
                OzellikTip nesne = new OzellikTip();
                nesne.ad = t.ad;
                nesne.ozellikTipID = t.ozellikTipID;
                liste.Add(nesne);
            }

            return Json(liste, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AltOzellik(int id)
        {
            var altOzellik = db.OzellikDeger.Where(x => x.ozellikTipID == id).ToList();
            List<OzellikDeger> liste = new List<OzellikDeger>();
            foreach (var t in altOzellik)
            {
                OzellikDeger nesne = new OzellikDeger();
                nesne.ad = t.ad;
                nesne.ozellikDegerID = t.ozellikDegerID;
                liste.Add(nesne);
            }
            return Json(liste, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Talep()
        {
            List<Satis> satis = db.Satis.Where(x => x.siparisDurumID == 1).ToList();
            return View(satis);
        }
        public ActionResult TalepDetay(int id)
        {
            KullaniciDetay kullaniciDetay = new KullaniciDetay();
            Satis satis = db.Satis.Where(x => x.satisID == id).SingleOrDefault();
            if (satis != null)
            {
                List<SatisDetayi> detay = db.SatisDetayi.Where(x => x.satisID == id).ToList();
                kullaniciDetay.satisTek = satis;
                kullaniciDetay.satisDetay = detay;
                kullaniciDetay.kullanici = satis.Kullanici;
                return View(kullaniciDetay);
            }
            else
            {
                TempData["Hata"] = "Böyle bir talep bulunmamaktadır";
                return RedirectToAction("Talep");
            }
        }
        public ActionResult Kargo(int id)
        {
            Satis satis = db.Satis.Where(x => x.satisID == id).SingleOrDefault();
            if (satis != null)
            {
                satis.siparisDurumID = 2;
                db.SaveChanges();
                TempData["Talep"] = "Ürün başarı ile kargoya aşamasına aktarılmıştır";
                Session["TalepSayisi"] = db.Satis.Where(x => x.siparisDurumID == 1).Count().ToString();
                return RedirectToAction("Talep");
            }
            else
            {
                TempData["Hata"] = "Böyle bir ürün yoktur";
                return RedirectToAction("Talep");
            }
        }
        public ActionResult AktifPasif(int id)
        {
            Urun silinecek = db.Urun.Where(x => x.urunID == id).SingleOrDefault();
            if (silinecek != null && silinecek.durum == true)
            {
                silinecek.durum = false;
                db.SaveChanges();
                TempData["mesaj"] = "Ürün başarı ile pasif edilmiştir";
            }
            else if (silinecek != null && silinecek.durum == false)
            {
                silinecek.durum = true;
                db.SaveChanges();
                TempData["mesaj"] = "Ürün başarı ile aktif edilmiştir";
            }
            return RedirectToAction("Index");
        }
    }
}