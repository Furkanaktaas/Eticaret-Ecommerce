using EticaretSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EticaretSitesi.Controllers
{
    public class OyuncakController : Controller
    {
        // GET: Oyuncak
        EticaretContext db = new EticaretContext();
        UrunListele urun = new UrunListele();
        public ActionResult Index()
        {
            urun.kategoriler = db.Kategori.ToList();
            urun.urunler = db.Urun.Where(x => x.durum == true).OrderByDescending(x => x.urunID).ToList();
            urun.urunResimler = db.UrunResim.ToList();
            return View(urun);
        }
        [HttpGet]
        public ActionResult Detay(int id)
        {
            Urun urunDetay = db.Urun.Where(x => x.urunID == id).SingleOrDefault();
            if (urunDetay != null)
            {
                UrunOzellik urunOzellik = db.UrunOzellik.Where(x => x.urunID == id).SingleOrDefault();
                urun.urunOzellik = urunOzellik;
                urun.urun = urunDetay;
                urun.urunResimler = db.UrunResim.Where(x => x.urunID == urunDetay.urunID).ToList();
                return View(urun);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Index(int kategoriListele)
        {
            if(kategoriListele==1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                urun.kategoriler = db.Kategori.ToList();
                urun.urunler = db.Urun.Where(x => x.durum == true && x.kategoriID == kategoriListele).OrderByDescending(x => x.urunID).ToList();
                urun.urunResimler = db.UrunResim.ToList();
            }
            return View(urun);
        }
        [HttpPost]
        public ActionResult Ekle(int sayi, Urun urun)
        {
            Kullanici kullanici =(Kullanici) Session["Musteri"];
            Urun urundb = db.Urun.Where(x => x.urunID == urun.urunID).SingleOrDefault(); ;
            Sepet sepet = new Sepet();
            sepet.urunID = urun.urunID;
            sepet.kullaniciID = kullanici.kullaniciID;
            int urunfiyatdb = Convert.ToInt32(urundb.urunFiyat);
            sepet.fiyat = urunfiyatdb;
            sepet.adet = sayi;
            db.Sepet.Add(sepet);
            db.SaveChanges();
            TempData["Mesaj"] = "Ürün sepete Eklenmiştir";
            return RedirectToAction("/Detay/"+urun.urunID);
        }
    }
}