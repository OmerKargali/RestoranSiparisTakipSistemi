using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestoranSiparisTakipSistemi.Models;

namespace RestoranSiparisTakipSistemi.Controllers;


[Authorize(Roles = "Admin")]

public class AdminController : Controller
{
    public AppDBContext _context;
    private readonly ILogger<AdminController> _logger;

    public AdminController(AppDBContext context, ILogger<AdminController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewData["Layout"] = "_AdminLayout";
        return View();
    }


public IActionResult AdminProfil()
{
    ViewData["Layout"] = "_AdminLayout";

    var claim = HttpContext.User;
    string? strKullaniciId = claim.FindFirstValue("KullaniciId");
    int intKullaniciId = (strKullaniciId == null ? 0 : Convert.ToInt32(strKullaniciId));

    var entity = _context.Kullanicilar.FirstOrDefault(x => x.KullaniciId == intKullaniciId);

    if (entity == null)
    {
        // Kullanıcı bulunamazsa bir hata mesajı veya yönlendirme yapabilirsiniz
        return RedirectToAction("ErrorPage");
    }

    VMKullanicilar model = new VMKullanicilar
    {
        KullaniciId = entity.KullaniciId,
        entity_Kullanicilar = entity
    };

    return View(model);
}
[HttpPost]
public IActionResult BilgiGuncelle(Kullanicilar k)
{
    // Kullanıcıyı buluyoruz
    Kullanicilar? kullanici = _context.Kullanicilar.Find(k.KullaniciId);

    if (kullanici == null)
    {
        return RedirectToAction("AdminProfil");
    }

    // Güncelleme işlemleri
    if (!string.IsNullOrWhiteSpace(k.Ad)) kullanici.Ad = k.Ad;
    if (!string.IsNullOrWhiteSpace(k.Soyad)) kullanici.Soyad = k.Soyad;
    if (!string.IsNullOrWhiteSpace(k.Telefon)) kullanici.Telefon = k.Telefon;
    if (!string.IsNullOrWhiteSpace(k.Eposta)) kullanici.Eposta = k.Eposta;
    if (!string.IsNullOrWhiteSpace(k.Adres)) kullanici.Adres = k.Adres;
    if (!string.IsNullOrWhiteSpace(k.Ulke)) kullanici.Ulke = k.Ulke;
    if (!string.IsNullOrWhiteSpace(k.ProfilFotoUrl)) kullanici.ProfilFotoUrl = k.ProfilFotoUrl;

    _context.SaveChanges();

    return RedirectToAction("AdminProfil");
}


    public IActionResult UrunEkleme()
    {
        ViewData["Layout"] = "_AdminLayout";

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UrunEkleme(VMUrunler modelUrunler)
    {
        ViewData["Layout"] = "_AdminLayout";

        Urunler? DbUrunler = new Urunler();
        if (modelUrunler.UrunId != null)
        {
            DbUrunler.UrunAdi = modelUrunler.UrunAdi;
            DbUrunler.Fiyat = modelUrunler.Fiyat;
            DbUrunler.Aciklama = modelUrunler.Aciklama;
            DbUrunler.KategoriId = modelUrunler.KategoriId;
            DbUrunler.UrunGorseliUrl = modelUrunler.UrunGorseliUrl;
        }
        if (ModelState.IsValid)
        {
            var BuUrunVarMı = await _context.Urunler.FirstOrDefaultAsync(DbUrunler => DbUrunler.UrunAdi == modelUrunler.UrunAdi);
            if (BuUrunVarMı != null)
            {
                ModelState.AddModelError("UrunAdi", "Aynı Ürün Zaten Mevcut");
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
                return View(modelUrunler);
            }
            _context.Urunler.Add(DbUrunler);
            _context.SaveChanges();
            return RedirectToAction("UrunEkleme", "Admin");
        }
        return View();
    }

    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Giris", "Hesap");
    }
}