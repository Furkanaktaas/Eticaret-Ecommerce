using EticaretSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EticaretSitesi.Controllers
{
    public class LoginController : Controller
    {
        EticaretContext db = new EticaretContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
            //Tuple.Create<Giris, Kullanici>(new Giris(), new Kullanici())
        }

        [HttpPost]
        public ActionResult Index(Giris gir)
        {
            if (ModelState.IsValid == false) // validation hatası varsa
            {
                return View();
            }
            else
            {
                Kullanici kullanici = db.Kullanici.Where(x => x.email.ToLower() == gir.email &&
                x.sifre.ToLower() == gir.sifre &&
                x.Yetki.yetkiAd == "MÜŞTERİ" && x.durum==true).SingleOrDefault();
                if (kullanici == null)
                {
                    TempData["Mesaj"] = "Kullanıcı Adı veya Şifre Hatalı";
                }
                else
                {
                    Session["Musteri"] = kullanici;
                    return RedirectToAction("Index", "Anasayfa");
                }
            }
            return View();
        }
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(Kullanici k, HttpPostedFileBase gelenResim)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            Kullanici kullanici = db.Kullanici.Where(x => x.email.ToLower() == k.email && x.yetkiID == 2).SingleOrDefault();
            if (!(kullanici == null))
            {
                TempData["Hata"] = "Mail zaten kayıtlı. Lütfen şifremi unuttum diyerek şifrenizi öğrenin";
                return View();
            }
            kullanici = db.Kullanici.Where(x => x.tc == k.tc && x.yetkiID == 2).SingleOrDefault();
            if (!(kullanici == null))
            {
                TempData["Hata"] = "Tc zaten kayıtlı. Lütfen şifremi unuttum diyerek şifrenizi öğrenin";
                return View();
            }
            Resim resim = new Resim();
            if (gelenResim != null)
            {
                string deger = resim.Ekle(gelenResim);
                if (deger == "uzanti")
                {
                    TempData["Hata"] = "Resim uzantısı jpg ve png den başka olamaz";
                    return View();
                }
                if (deger == "boyut")
                {
                    TempData["Hata"] = "Resmin boyutu maksimum 3MB olabilir";
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
            k.yetkiID = 2;
            k.durum = true;
            db.Kullanici.Add(k);
            db.SaveChanges();
            TempData["Mesaj"] = "Kayıt başarıyla oluşturulmuştur";
            return RedirectToAction("Index");
        }
        public ActionResult Cikis()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Sifre()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Sifre(string eposta)
        {
            Kullanici kullanici = db.Kullanici.Where(x => x.email.ToLower() == eposta.ToLower()).SingleOrDefault();
            if (kullanici != null)
            {
                Sifre s = new Sifre();
                s.kullaniciID = kullanici.kullaniciID;
                s.kod = Guid.NewGuid();
                db.Sifre.Add(s);
                db.SaveChanges();
                MailGonderme Eposta = new MailGonderme();
                string konu = "Şifre Sıfırlama";
                string mesaj = "Şifrenizi sıfırlamak için <a href='http://localhost:65283/Login/SifreSifirla?kod=" + s.kod + "'> tıklayınız";
                Eposta.Gonder(konu, mesaj, kullanici.email.ToLower());
                ViewBag.Uyari = "Epostanıza şifreniz gönderilmiştir.";
            }
            else
            {
                ViewBag.Hata = "Böyle bir eposta kayıtlı değildir";
            }
            return View();
        }
        [HttpGet]
        public ActionResult SifreSifirla(string kod)
        {
            Sifre s = db.Sifre.Where(x => x.kod.ToString() == kod).SingleOrDefault();
            if (s == null)
            {
                return RedirectToAction("Sifre");
            }
            Kullanici k = db.Kullanici.Where(x => x.kullaniciID == s.kullaniciID).SingleOrDefault();
            return View(k);
        }
        [HttpPost]
        public ActionResult SifreSifirla(string sifre, int kullaniciID)
        {
            Kullanici k = db.Kullanici.SingleOrDefault(x => x.kullaniciID == kullaniciID);
            k.sifre = sifre;
            db.SaveChanges();
            TempData["Basari"] = "Şifreniz Başarı ile Değiştirildi";
            return RedirectToAction("Index");
        }
    }
}