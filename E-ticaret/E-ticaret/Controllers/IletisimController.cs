using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EticaretSitesi.Controllers
{
    public class IletisimController : Controller
    {
        // GET: Iletisim
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Iletisim iletisim)
        {
            if (ModelState.IsValid==false) // validation hatası varsa
            {
                return View();
            }
            else
            {
                MailGonderme mail = new MailGonderme();
                string mesaj = "";
                mesaj = mesaj + "<b> Gönderici Adı: </b>" + iletisim.ad + "<br/>";
                mesaj = mesaj + "<b> Gönderici Maili: </b>" + iletisim.eposta + " <br/><br/>";
                mesaj = mesaj + "<b> Konu : </b>" + iletisim.konu + " <br/>";
                mesaj = mesaj + "<b> Gönderici mesajı : </b>" + iletisim.icerik;
                string deger = mail.Gonder(iletisim.konu, mesaj);
                if(deger=="basarili")
                {
                    ViewBag.Mesaj = "Mesajınız gönderilmiştir en kısa zamanda geri dönüş sağlanacaktır";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.Mesaj = "Sistemde bir hata oluştuğu için mesajınız gönderilemedi";
                    return View();
                }
            }
        }
    }
}