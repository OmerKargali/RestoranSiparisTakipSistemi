using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RestoranSiparisTakipSistemi.Models;
using Microsoft.EntityFrameworkCore;

namespace RestoranSiparisTakipSistemi.Controllers;

public class HomeController : Controller
{
    public AppDBContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(AppDBContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

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

    public IActionResult Menu()
    {
        return View();
    }
    public IActionResult Sepet()
    {
        return View();
    }
    public IActionResult SiparisTamamla()
    {
        return View();
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
