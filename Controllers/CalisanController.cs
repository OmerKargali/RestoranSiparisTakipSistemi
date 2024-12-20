using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace RestoranSiparisTakipSistemi.Controllers;


[Authorize(Roles = "Calisan")]
public class CalisanController : Controller
{
    public AppDBContext _context;
    private readonly ILogger<CalisanController> _logger;

    public CalisanController(AppDBContext context, ILogger<CalisanController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        IndexViewModel model = new IndexViewModel();

        // Tüm siparişleri sorguluyoruz
        var siparisler = (from A in _context.OnaylananSiparis
                          join B in _context.Siparisler on A.SiparisId equals B.SiparisId
                          join C in _context.Urunler on B.UrunId equals C.UrunId // Ürünler tablosuna join
                          select new cstOnaylananSiparisler
                          {
                              OnaylananSiparisId = A.OnaylananSiparisId,
                              SiparisId = B.SiparisId,
                              KullaniciId = B.KullaniciId,
                              UrunId = B.UrunId,
                              TeslimatAdresi = A.TeslimatAdresi,
                              SiparisNotu = A.SiparisNotu,
                              Durum = A.Durum,
                              TahminiTeslimatSuresi = A.TahminiTeslimatSuresi,
                              UrunAdi = C.UrunAdi, // Ürünler tablosundaki UrunAdi
                              UrunGorseliUrl = C.UrunGorseliUrl
                          }).ToList();

        // Eğer siparişler bulunmazsa kullanıcıya hata mesajı gösteriyoruz
        if (siparisler == null || !siparisler.Any())
        {
            ModelState.AddModelError("", "Aktif Sipariş bulunmamaktadır.");
        }

        model.list_cstOnaylananSiparisler = siparisler; // Siparişleri modele atıyoruz
        model.OnaylananSiparislers = new List<OnaylananSiparis>(); // Diğer model alanı boş

        // View'e modeli gönderiyoruz
        return View(model);
    }

    public IActionResult ProfilDuzenleme()
    {
        return View();
    }
    public IActionResult SiparisDetay()
    {
        return View();
    }

    [HttpPost]
    public IActionResult DurumGuncelle(int OnaylananSiparisId, string Durum)
    {
        var verilenSiparis = _context.OnaylananSiparis.Find(OnaylananSiparisId);
        if (verilenSiparis != null)
        {
            verilenSiparis.Durum = Durum;
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }
}