using EticaretSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EticaretSitesi.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        EticaretContext db = new EticaretContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Giris giris)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            Kullanici kullanici = db.Kullanici.Where(x => x.email.ToLower() == giris.email && x.sifre == giris.sifre && x.Yetki.yetkiAd == "ADMİN" && x.durum == true).SingleOrDefault();
            if (kullanici == null)
            {
                ViewBag.Hata = "Kullanıcı adı ve şifre hatalı";
            }
            else
            {
                Session["Admin"] = kullanici;
                return RedirectToAction("Index", "Admin");
            }
            return View();
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
                string mesaj = "Şifrenizi sıfırlamak için <a href='http://localhost:65283/AdminLogin/SifreSifirla?kod=" + s.kod + "'> tıklayınız";
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