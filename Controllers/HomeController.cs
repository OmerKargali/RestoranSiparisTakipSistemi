using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RestoranSiparisTakipSistemi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace RestoranSiparisTakipSistemi.Controllers;

[Authorize(Roles = "Kullanici")]

public class HomeController : Controller
{
    public AppDBContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(AppDBContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        ViewData["Layout"] = "_Layout";
        return View();
    }

    public IActionResult Hakkinda()
    {
        return View();
    }

    public IActionResult Iletisim()
    {
        return View();
    }
      public IActionResult Siparislerim()
    {
        return View();
    }

    public IActionResult Menu()
    {
        var UrunlerListe = _context.Urunler.ToList();
        VMUrunler ModelUrunler = new VMUrunler();
        ModelUrunler.list_Urunler = UrunlerListe;

        return View(ModelUrunler);
    }

    public IActionResult SiparisDetay(int id)
    {
        var entity = _context.Urunler.Where(x => x.UrunId == id).FirstOrDefault();
        VMUrunler model = new VMUrunler();
        model.entity_Urunler = entity;
        return View(model);
    }
    public async Task<IActionResult> SiparisKaydet(int UrunId)
    {
        int kullaniciId = int.Parse(User.FindFirstValue("KullaniciId"));

        var YeniUrun = new Siparisler()
        {
            UrunId = UrunId,
            KullaniciId = kullaniciId,
            Durum = "Sepette"
        };

        await _context.Siparisler.AddAsync(YeniUrun);
        await _context.SaveChangesAsync();

        return RedirectToAction("Sepet", "Home");
    }

    public IActionResult Sepet()
    {
        IndexViewModel model = new IndexViewModel();
        System.Security.Claims.ClaimsPrincipal claim = this.User;
        string? strKullaniciId = claim.FindFirstValue("KullaniciId");
        int intKullaniciId = (strKullaniciId == null ? 0 : Convert.ToInt32(strKullaniciId));

        Kullanicilar? kullanici = _context.Kullanicilar.Where(x => x.KullaniciId == intKullaniciId).FirstOrDefault();
        if (kullanici != null)
        {
            int kullaniciId = kullanici.KullaniciId;

            // "Sepette" durumundaki siparişleri al
            var siparisler = (from A in _context.Urunler
                              join B in _context.Siparisler on A.UrunId equals B.UrunId
                              where B.KullaniciId == intKullaniciId && B.Durum == "Sepette" // Durum "Sepette" olan siparişleri seç
                              select new cstSiparis
                              {
                                  SiparisId = B.SiparisId,
                                  UrunId = A.UrunId,
                                  KullaniciId = B.KullaniciId,
                                  BirimFiyat = A.Fiyat,
                                  UrunAdi = A.UrunAdi,
                                  UrunGorseliUrl = A.UrunGorseliUrl
                              }).ToList();

            if (siparisler == null || !siparisler.Any())
            {
                // Eğer Sepet boşsa veya ürün bulunamıyorsa
                ModelState.AddModelError("", "Sepetinizde ürün bulunmamaktadır.");
            }

            model.list_cstSiparis = siparisler;
            model.Siparislers = new List<Siparisler>();
        }

        return View(model);
    }




    // public async Task<IActionResult> SiparisTamamla(VMOnaylananSiparis modelOnaylananSiparis)
    // {
    //     int kullaniciId = int.Parse(User.FindFirstValue("kullaniciId"));

    //     // Aktif siparişi al
    //     var aktifSiparis = await _context.Siparisler
    //         .FirstOrDefaultAsync(s => s.KullaniciId == kullaniciId);

    //     if (aktifSiparis == null)
    //     {
    //         ModelState.AddModelError("", "Aktif bir sipariş bulunamadı.");
    //         return View(modelOnaylananSiparis);
    //     }

    //     var yeniOnaylananSiparis = new OnaylananSiparis
    //     {
    //         SiparisId = aktifSiparis.SiparisId,
    //         KullaniciId = kullaniciId,
    //         TeslimatAdresi = modelOnaylananSiparis.TeslimatAdresi,
    //         KrediKartiNumarasi = modelOnaylananSiparis.KrediKartiNumarasi,
    //         SonKullanmaTarihi = modelOnaylananSiparis.SonKullanmaTarihi,
    //         CvvKodu = modelOnaylananSiparis.CvvKodu,
    //         OdemeYontemi = modelOnaylananSiparis.OdemeYontemi,
    //         SiparisNotu = modelOnaylananSiparis.SiparisNotu
    //     };

    //     if (ModelState.IsValid)
    //     {
    //         // Onaylanan siparişi kaydet
    //         await _context.OnaylananSiparis.AddAsync(yeniOnaylananSiparis);
    //         await _context.SaveChangesAsync();

    //         // Siparişin durumunu güncelle
    //         aktifSiparis.Durum = "Sepete eklendi";
    //         _context.Siparisler.Update(aktifSiparis);
    //         await _context.SaveChangesAsync();

    //         return RedirectToAction("SiparisOnayla", "Home");
    //     }

    //     ModelState.AddModelError("", "Geçersiz giriş.");
    //     return View(modelOnaylananSiparis);
    // }
    // public IActionResult SiparisTamamla()
    // {
    //     IndexViewModel model = new IndexViewModel();
    //     System.Security.Claims.ClaimsPrincipal claim = this.User;
    //     string? strKullaniciId = claim.FindFirstValue("KullaniciId");
    //     int intKullaniciId = (strKullaniciId == null ? 0 : Convert.ToInt32(strKullaniciId));
    //     Kullanicilar? kullanici = _context.Kullanicilar.Where(x => x.KullaniciId == intKullaniciId).FirstOrDefault();
    //     if (kullanici != null)
    //     {
    //         int kullaniciId = kullanici.KullaniciId;
    //         var siparisler = (from A in _context.Urunler
    //                           join B in _context.Siparisler on A.UrunId equals B.UrunId
    //                           where B.KullaniciId == intKullaniciId
    //                           select new cstSiparis
    //                           {
    //                               SiparisId = B.SiparisId,
    //                               UrunId = A.UrunId,
    //                               KullaniciId = B.KullaniciId,
    //                               BirimFiyat = A.Fiyat,
    //                               UrunAdi = A.UrunAdi,
    //                               UrunGorseliUrl = A.UrunGorseliUrl
    //                           }).ToList();

    //         model.list_cstSiparis = siparisler;
    //         model.Siparislers = new List<Siparisler>();
    //     }
    //     return View(model);
    // }

    public IActionResult SiparisTamamla()
    {

        VMOnaylananSiparis model = new VMOnaylananSiparis();

        int kullaniciId = int.Parse(User.FindFirstValue("kullaniciId"));

        // Aktif siparişi al
        var aktifSiparis = _context.Siparisler.FirstOrDefaultAsync(s => s.KullaniciId == kullaniciId);

        if (aktifSiparis == null)
        {
            model.BilgiNotu = "Aktif siparişiniz bulunamadı";
        }
        else
        {
            model.SiparisId = aktifSiparis.Id;
        }

        return View(model);
    }

    // [HttpPost]
    // public async Task<IActionResult> SiparisOnayla(VMOnaylananSiparis modelOnaylananSiparis)
    // {
    //     int kullaniciId = int.Parse(User.FindFirstValue("kullaniciId"));

    //     // Aktif siparişi al
    //     var aktifSiparis = await _context.Siparisler
    //         .FirstOrDefaultAsync(s => s.KullaniciId == kullaniciId);

    //     if (aktifSiparis == null)
    //     {
    //         ModelState.AddModelError("", "Aktif bir sipariş bulunamadı.");
    //         return View(modelOnaylananSiparis);
    //     }

    //     var yeniOnaylananSiparis = new OnaylananSiparis
    //     {
    //         SiparisId = aktifSiparis.SiparisId,
    //         KullaniciId = kullaniciId,
    //         TeslimatAdresi = modelOnaylananSiparis.TeslimatAdresi,
    //         KrediKartiNumarasi = modelOnaylananSiparis.KrediKartiNumarasi,
    //         SonKullanmaTarihi = modelOnaylananSiparis.SonKullanmaTarihi,
    //         CvvKodu = modelOnaylananSiparis.CvvKodu,
    //         OdemeYontemi = modelOnaylananSiparis.OdemeYontemi,
    //         SiparisNotu = modelOnaylananSiparis.SiparisNotu
    //     };

    //     if (ModelState.IsValid)
    //     {
    //         foreach(var sepetUrunu in aktifSiparis){

    //         }
    //         // Onaylanan siparişi kaydet
    //         await _context.OnaylananSiparis.AddAsync(yeniOnaylananSiparis);
    //         await _context.SaveChangesAsync();

    //         // Siparişin durumunu güncelle
    //         aktifSiparis.Durum = "Siparis Alindi";
    //         _context.Siparisler.Update(aktifSiparis);
    //         await _context.SaveChangesAsync();

    //         return RedirectToAction("Index", "Home");
    //     }

    //     ModelState.AddModelError("", "Geçersiz giriş.");
    //     return View(modelOnaylananSiparis);
    // }
    public async Task<IActionResult> SiparisOnayla(VMOnaylananSiparis modelOnaylananSiparis)
    {
        int kullaniciId = int.Parse(User.FindFirstValue("kullaniciId"));

        // Sepetteki aktif siparişleri al (Durumu 'Sepette' olan)
        var aktifSiparisler = await _context.Siparisler
            .Where(s => s.KullaniciId == kullaniciId && s.Durum == "Sepette")
            .FirstOrDefaultAsync();  // Sadece bir sipariş döndür

        if (aktifSiparisler == null)
        {
            ModelState.AddModelError("", "Sepetinizde onaylanacak sipariş bulunmamaktadır.");
            return View(modelOnaylananSiparis);
        }

        if (ModelState.IsValid)
        {
            // Onaylanan siparişi oluştur
            var yeniOnaylananSiparis = new OnaylananSiparis
            {
                SiparisId = aktifSiparisler.SiparisId,
                KullaniciId = kullaniciId,
                TeslimatAdresi = modelOnaylananSiparis.TeslimatAdresi,
                KrediKartiNumarasi = modelOnaylananSiparis.KrediKartiNumarasi,
                SonKullanmaTarihi = modelOnaylananSiparis.SonKullanmaTarihi,
                CvvKodu = modelOnaylananSiparis.CvvKodu,
                OdemeYontemi = modelOnaylananSiparis.OdemeYontemi,
                SiparisNotu = modelOnaylananSiparis.SiparisNotu,
                Durum = "Siparis Alindi"
            };

            // Onaylanan siparişi OnaylananSiparis tablosuna ekle
            await _context.OnaylananSiparis.AddAsync(yeniOnaylananSiparis);

            // Sepetteki siparişin durumunu 'Tamamlandi' olarak güncelle
            aktifSiparisler.Durum = "Beklemede";

            // Değişiklikleri kaydet
            await _context.SaveChangesAsync();

            return RedirectToAction("Menu", "Home");
        }

        ModelState.AddModelError("", "İşlem sırasında bir hata oluştu");
        return View(modelOnaylananSiparis);
    }

    public IActionResult Profil()
    {
        var claim = HttpContext.User;
        string? strKullaniciId = claim?.FindFirstValue("KullaniciId");
        int intKullaniciId = (strKullaniciId == null ? 0 : Convert.ToInt32(strKullaniciId));
        Kullanicilar? entity = _context.Kullanicilar.Where(x => x.KullaniciId == intKullaniciId).FirstOrDefault();
        VMKullanicilar model = new VMKullanicilar();
        model.KullaniciId = entity?.KullaniciId ?? 0;
        model.entity_Kullanicilar = entity;

        return View(model);
    }

    public IActionResult BilgiGuncelle(Kullanicilar k)
    {

        Kullanicilar? kullanici = _context.Kullanicilar.Find(k.KullaniciId);
        if (!string.IsNullOrWhiteSpace(k.Ad))
        {
            kullanici.Ad = k.Ad;
        }

        if (!string.IsNullOrWhiteSpace(k.Soyad))
        {
            kullanici.Soyad = k.Soyad;
        }

        if (!string.IsNullOrWhiteSpace(k.Eposta))
        {
            kullanici.Eposta = k.Eposta;
        }

        if (!string.IsNullOrWhiteSpace(k.Telefon))
        {
            kullanici.Telefon = k.Telefon;
        }

        if (!string.IsNullOrWhiteSpace(k.Adres))
        {
            kullanici.Adres = k.Adres;
        }

        if (!string.IsNullOrWhiteSpace(k.Ulke))
        {
            kullanici.Ulke = k.Ulke;
        }

        if (!string.IsNullOrWhiteSpace(k.ProfilFotoUrl))
        {
            kullanici.ProfilFotoUrl = k.ProfilFotoUrl;
        }

        _context.SaveChanges();

        return RedirectToAction("Profil");
    }

    [AllowAnonymous]
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Giris", "Hesap");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
