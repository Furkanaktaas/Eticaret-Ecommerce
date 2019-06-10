using EticaretSitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EticaretSitesi.Controllers
{
    public class KategoriController : Controller
    {
        EticaretContext db = new EticaretContext();
        public static int kid;
        // GET: Kategori
        public ActionResult Index()
        {
            List<Kategori> kategori = db.Kategori.ToList();
            return View(kategori);
        }
        [HttpGet]
        public ActionResult Sil(int id)
        {
            Kategori kategori = db.Kategori.Where(x => x.kategoriID == id).SingleOrDefault();
            if (kategori != null && kategori.kategoriID != 1)
            {
                List<OzellikTip> ozellikTip = db.OzellikTip.ToList();
                foreach (var ozellik in ozellikTip)
                {
                    if (ozellik.kategoriID == kategori.kategoriID)
                    {
                        ozellik.kategoriID = 1;
                        db.SaveChanges();
                    }
                }
                List<Urun> urun = db.Urun.ToList();
                foreach (var urunler in urun)
                {
                    if (urunler.kategoriID == kategori.kategoriID)
                    {
                        urunler.kategoriID = 1;
                        db.SaveChanges();
                    }
                }
                db.Kategori.Remove(kategori);
                db.SaveChanges();
                TempData["mesaj"] = "Kategori Başarı ile Silinmiştir ";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Kategori k)
        {
            Kategori kategori = db.Kategori.Where(x => x.kategoriAd == k.kategoriAd.ToUpper()).SingleOrDefault();
            if (ModelState.IsValid == false)
            {
                return View();
            }
            if (kategori != null)
            {
                ViewBag.Hata = "Kategori adı zaten mevcut";
                return View();
            }
            k.kategoriAd = k.kategoriAd.ToUpper();
            db.Kategori.Add(k);
            db.SaveChanges();
            @TempData["mesaj"] = "Kategori başarıyla kaydedildi";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Duzenle(int id)
        {
            Kategori kategori = db.Kategori.Where(x => x.kategoriID == id).SingleOrDefault();
            if (kategori != null && kategori.kategoriID != 1)
            {
                kid = id;
                return View(kategori);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Duzenle(Kategori k)
        {
            Kategori kategori = db.Kategori.Where(x => x.kategoriID == kid).SingleOrDefault();
            if (ModelState.IsValid == false)
            {
                return View();
            }
            if (kategori != null)
            {
                Kategori kategori2 = db.Kategori.Where(x => x.kategoriAd == k.kategoriAd).SingleOrDefault();
                if (kategori2 != null)
                {
                    ViewBag.Hata = "Aynı kategori adı mevcut";
                    return View();
                }
                else
                {
                    kategori.kategoriAd = k.kategoriAd.ToUpper();
                    db.SaveChanges();
                    TempData["mesaj"] = "Kategori başarı ile düzenlenmiştir";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}