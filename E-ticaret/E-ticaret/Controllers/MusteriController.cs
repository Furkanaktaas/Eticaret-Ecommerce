using EticaretSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EticaretSitesi.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        EticaretContext db = new EticaretContext();
        public static int kid;
        public ActionResult Index()
        {
            Kullanici session =(Kullanici)Session["Musteri"];
            KullaniciDetay detay = new KullaniciDetay();
            detay.satis = db.Satis.Where(x=> x.kullaniciID == session.kullaniciID && (x.siparisDurumID==1 || x.siparisDurumID==2)).ToList();
            foreach (var item in detay.satis)
            {
                List<SatisDetayi> liste = db.SatisDetayi.Where(x => x.satisID == item.satisID).ToList();
                var allProducts = detay.satisDetay.Concat(liste).ToList();
                detay.satisDetay = allProducts;
            }
            return View(detay);
        }
        public ActionResult Iptal(int id)
        {
            Satis satis = db.Satis.Where(x=> x.satisID == id).SingleOrDefault();
            if(satis!=null)
            {
                satis.siparisDurumID = 4;
                db.SaveChanges();
                TempData["mesaj"] = "Sipariş talebiniz başarı ile iptal edilmiştir";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Gecmis()
        {
            Kullanici session = (Kullanici)Session["Musteri"];
            KullaniciDetay detay = new KullaniciDetay();
            detay.satis = db.Satis.Where(x => x.kullaniciID == session.kullaniciID && (x.siparisDurumID == 3 || x.siparisDurumID == 4)).ToList();
            foreach (var item in detay.satis)
            {
                List<SatisDetayi> liste = db.SatisDetayi.Where(x => x.satisID == item.satisID).ToList();
                var allProducts = detay.satisDetay.Concat(liste).ToList();
                detay.satisDetay = allProducts;
            }
            return View(detay);
        }
        [HttpGet]
        public ActionResult Bilgiler()
        {
            Kullanici session = (Kullanici)Session["Musteri"];
            return View(session);
        }
        [HttpPost]
        public ActionResult Bilgiler(Kullanici k,HttpPostedFileBase gelenResim)
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
            TempData["mesaj"] = "Bilgileriniz Başarı ile Güncellenmiştir";
            Session["Musteri"] = kullanici;
            return RedirectToAction("Bilgiler");
        }
        [HttpGet]
        public ActionResult Sepet()
        {
            Kullanici kullanici = (Kullanici)Session["Musteri"];
            List<Sepet> sepet = db.Sepet.Where(x=> x.kullaniciID == kullanici.kullaniciID).ToList();
            return View(sepet);
        }
        public ActionResult Cikar(int id)
        {
            Sepet sepet = db.Sepet.Where(x => x.sepetID == id).SingleOrDefault();
            if(sepet!=null)
            { 
            db.Sepet.Remove(sepet);
            db.SaveChanges();
            TempData["mesaj"] = "Ürün başarı ile sepetten silinmiştir";
            }
            return RedirectToAction("Sepet");
        }
        public ActionResult Tamamla()
        {
            int toplamTutar=0;
            Kullanici kullanici = (Kullanici) Session["Musteri"];
            List<Sepet> sepeti = db.Sepet.Where(x=> x.kullaniciID == kullanici.kullaniciID).ToList();
            Satis satis = new Satis();
            foreach (var item in sepeti)
            {
                int fiyat = Convert.ToInt32(item.fiyat);
                int adet = item.adet.Value;
                int hesapla = adet * fiyat;
                toplamTutar = toplamTutar + hesapla;
            }
            satis.kullaniciID = kullanici.kullaniciID;
            satis.satisTarihi = DateTime.Now;
            satis.toplamTutar = toplamTutar;
            satis.siparisDurumID = 1;
            db.Satis.Add(satis);
            db.SaveChanges();
            SatisDetayi satisDetaylari = new SatisDetayi();
            foreach (var item in sepeti)
            {
                satisDetaylari.urunID = item.urunID;
                satisDetaylari.satisID = satis.satisID;
                satisDetaylari.fiyat = item.fiyat;
                satisDetaylari.adet = item.adet;
                db.SatisDetayi.Add(satisDetaylari);
                db.SaveChanges();
            }
            foreach(var item in sepeti)
            {
                db.Sepet.Remove(item);
                db.SaveChanges();
            }
            TempData["Mesaj"] = "Siparişiniz başarı ile verilmiştir";
            return RedirectToAction("Sepet");
        }
    }
}