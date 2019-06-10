using EticaretSitesi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EticaretSitesi.Controllers.Admin
{
    public class KullaniciController : Controller
    {
        EticaretContext db = new EticaretContext();
        public static int kid;
        // GET: Kullanici
        public ActionResult Index()
        {
            List<Kullanici> kullanicilar = db.Kullanici.ToList();
            return View(kullanicilar);
        }
        [HttpGet]
        public ActionResult AktifPasif(int id)
        {
            Kullanici silinecek = db.Kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
            Kullanici kullanici = (Kullanici)Session["Admin"];
            if (kullanici.kullaniciID != silinecek.kullaniciID)
            {
                if (silinecek != null && silinecek.durum == true)
                {
                    silinecek.durum = false;
                    db.SaveChanges();
                    TempData["mesaj"] = "Kullanıcı başarı ile pasif yapılmıştır";
                }
                else if (silinecek != null && silinecek.durum == false)
                {
                    TempData["mesaj"] = "Kullanıcı başarı ile aktif yapılmıştır";
                    silinecek.durum = true;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Detay(int id)
        {
            KullaniciDetay detay = new KullaniciDetay();
            Kullanici kullanici = db.Kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
            if (kullanici != null)
            {
                detay.kullanici = kullanici;
                detay.satis = db.Satis.Where(x => x.kullaniciID == id).ToList();
                foreach (var item in detay.satis)
                {
                    List<SatisDetayi> liste = db.SatisDetayi.Where(x => x.satisID == item.satisID).ToList();
                    var allProducts = detay.satisDetay.Concat(liste).ToList();
                    detay.satisDetay = allProducts;
                }
                return View(detay);
            }
            else if (kullanici == null)
            {
                TempData["hata"] = "Böyle bir kullanıcı bulunmamaktadır";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["hata"] = "Lütfen dışarıdan müdahale etmeye çalışmayınız";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            var yetkiler = db.Yetki.ToList();
            ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Kullanici k, HttpPostedFileBase gelenResim)
        {
            var yetkiler = db.Yetki.ToList();
            if (ModelState.IsValid == false) // validation hatası varsa
            {
                ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                return View();
            }
            Kullanici kul = db.Kullanici.Where(x => x.email.ToLower() == k.email.ToLower() && x.yetkiID == k.yetkiID).SingleOrDefault();
            if (kul != null)
            {
                ViewBag.Hata = "Aynı eposta ile kayıtlı " + kul.Yetki.yetkiAd.ToLower() + " vardır";
                ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                return View();
            }
            kul = db.Kullanici.Where(x => x.tc == k.tc && x.yetkiID == k.yetkiID).SingleOrDefault();
            if (kul != null)
            {
                ViewBag.Hata = "Aynı Tc ile kayıtlı " + kul.Yetki.yetkiAd.ToLower() + " vardır";
                ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                return View();
            }
            Resim resim = new Resim();
            if (gelenResim != null)
            {
                string deger = resim.Ekle(gelenResim);
                if (deger == "uzanti")
                {
                    ViewBag.Hata = "Resim uzantısı jpg ve png den başka olamaz";
                    ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                    return View();
                }
                if (deger == "boyut")
                {
                    ViewBag.Hata = "Resmin boyutu maksimum 3MB olabilir";
                    ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                    return View();
                }
                k.resimAd = deger;
            }
            else
            {
                k.resimAd = "default.jpg";
            }
            k.ad = k.ad.ToUpper();
            k.soyad = k.soyad.ToUpper();
            k.email = k.email.ToUpper();
            k.adres = k.adres.ToUpper();
            k.durum = true;
            db.Kullanici.Add(k);
            db.SaveChanges();
            TempData["Basari"] = "Kayıt başarı ile oluşturulmuştur";
            Session["KullaniciSayisi"] = db.Kullanici.Count().ToString();
            ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
            return RedirectToAction("Ekle");
        }
        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            kid = id;
            Kullanici kullanici = db.Kullanici.Where(x => x.kullaniciID == kid).SingleOrDefault();
            Kullanici session = (Kullanici)Session["Admin"];
            if (kullanici != null && session.kullaniciID != kullanici.kullaniciID)
            {
                var yetkiler = db.Yetki.ToList();
                ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                return View(kullanici);
            }
            else if (kullanici != null && session.kullaniciID == kullanici.kullaniciID)
            {
                TempData["hata"] = "Kendi bilgilerinizi sağ üstte bulunan profilim menüsünden güncelleyebilirsiniz";
            }
            else
            {
                TempData["hata"] = "Lütfen dışarıdan müdahale etmeye çalışmayınız";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Duzenle(Kullanici k, HttpPostedFileBase gelenResim)
        {
            Kullanici kullanici = db.Kullanici.Where(x => x.kullaniciID == kid).SingleOrDefault();
            var yetkiler = db.Yetki.ToList();
            if (ModelState.IsValid == false) // validation hatası varsa
            {
                ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                return View(kullanici);
            }
            Kullanici kul = db.Kullanici.Where(x => x.email.ToLower() == k.email.ToLower() && x.yetkiID == k.yetkiID).SingleOrDefault();
            if (kul != null && kul.kullaniciID != kid)
            {
                ViewBag.Hata = "Aynı eposta ile kayıtlı " + kul.Yetki.yetkiAd.ToLower() + " vardır";
                ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                return View(kullanici);
            }
            kul = db.Kullanici.Where(x => x.tc == k.tc && x.yetkiID == k.yetkiID).SingleOrDefault();
            if (kul != null && kul.kullaniciID != kid)
            {
                ViewBag.Hata = "Aynı Tc ile kayıtlı " + kul.Yetki.yetkiAd.ToLower() + " vardır";
                ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                return View(kullanici);
            }
            Resim resim = new Resim();
            if (gelenResim != null)
            {
                string deger = resim.Ekle(gelenResim);
                if (deger == "uzanti")
                {
                    ViewBag.Hata = "Resim uzantısı jpg ve png den başka olamaz";
                    ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                    return View(kullanici);
                }
                if (deger == "boyut")
                {
                    ViewBag.Hata = "Resmin boyutu maksimum 3MB olabilir";
                    ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                    return View(kullanici);
                }
                k.resimAd = deger;
            }
            else
            {
                k.resimAd = kullanici.resimAd;
            }
            kullanici.tc = k.tc;
            kullanici.ad = k.ad.ToUpper();
            kullanici.soyad = k.soyad.ToUpper();
            kullanici.email = k.email.ToUpper();
            kullanici.adres = k.adres.ToUpper();
            kullanici.telefonNo = k.telefonNo;
            kullanici.yetkiID = k.yetkiID;
            kullanici.sifre = k.sifre;
            kullanici.resimAd = k.resimAd;
            db.SaveChanges();
            TempData["mesaj"] = "Güncelleme işlemi başarılı bir şekilde gerçekleştirilmiştir";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Profil()
        {
            Kullanici session = (Kullanici)Session["Admin"];
            kid = session.kullaniciID;
            Kullanici bilgiler = db.Kullanici.Where(x=> x.kullaniciID == kid).SingleOrDefault();
            if (bilgiler != null && session.kullaniciID == bilgiler.kullaniciID)
            {
                var yetkiler = db.Yetki.ToList();
                ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                return View(bilgiler);
            }
            else
            {
                return RedirectToAction("/Admin/Index");
            }
        }
        [HttpPost]
        public ActionResult Profil(Kullanici k, HttpPostedFileBase gelenResim)
        {
            kid = k.kullaniciID;
            Kullanici kullanici = db.Kullanici.Where(x => x.kullaniciID == kid).SingleOrDefault();
            var yetkiler = db.Yetki.ToList();
            if (ModelState.IsValid == false) // validation hatası varsa
            {
                ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                return View(kullanici);
            }
            Kullanici kul = db.Kullanici.Where(x => x.email.ToLower() == k.email.ToLower() && x.yetkiID == k.yetkiID).SingleOrDefault();
            if (kul != null && kul.kullaniciID != kid)
            {
                ViewBag.Hata = "Aynı eposta ile kayıtlı " + kul.Yetki.yetkiAd.ToLower() + " vardır";
                ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                return View(kullanici);
            }
            kul = db.Kullanici.Where(x => x.tc == k.tc && x.yetkiID == k.yetkiID).SingleOrDefault();
            if (kul != null && kul.kullaniciID != kid)
            {
                ViewBag.Hata = "Aynı Tc ile kayıtlı " + kul.Yetki.yetkiAd.ToLower() + " vardır";
                ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                return View(kullanici);
            }
            Resim resim = new Resim();
            if (gelenResim != null)
            {
                string deger = resim.Ekle(gelenResim);
                if (deger == "uzanti")
                {
                    ViewBag.Hata = "Resim uzantısı jpg ve png den başka olamaz";
                    ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                    return View(kullanici);
                }
                if (deger == "boyut")
                {
                    ViewBag.Hata = "Resmin boyutu maksimum 3MB olabilir";
                    ViewBag.Yetkiler = new SelectList(yetkiler, "yetkiID", "yetkiAd");
                    return View(kullanici);
                }
                k.resimAd = deger;
            }
            else
            {
                k.resimAd = kullanici.resimAd;
            }
            kullanici.tc = k.tc;
            kullanici.ad = k.ad.ToUpper();
            kullanici.soyad = k.soyad.ToUpper();
            kullanici.email = k.email.ToUpper();
            kullanici.adres = k.adres.ToUpper();
            kullanici.telefonNo = k.telefonNo;
            kullanici.sifre = k.sifre;
            kullanici.resimAd = k.resimAd;
            db.SaveChanges();
            ViewBag.Mesaj = "Güncelleme işlemi başarılı bir şekilde gerçekleştirilmiştir";
            Session["Admin"] = kullanici;
            return View(kullanici);
        }
    }
}