using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestoranSiparisTakipSistemi.Models;
using System.Text.RegularExpressions;

public class HesapController : Controller
{
    public AppDBContext _context;

    public HesapController(AppDBContext context)
    {
        _context = context;
    }

    public IActionResult Giris()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Giris(VMGiris modelGiris)
    {
        string HataMesaji = "";
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string? strBaglanti = configuration.GetConnectionString("WebApiDatabase");
        var opt = new DbContextOptionsBuilder<AppDBContext>();
        opt.UseNpgsql(strBaglanti);
        _context = new AppDBContext(opt.Options);

        if (!string.IsNullOrWhiteSpace(modelGiris.Eposta) && !string.IsNullOrWhiteSpace(modelGiris.Sifre))
        {
            Kullanicilar? kullanici = _context.Kullanicilar.FirstOrDefault(x => x.Eposta == modelGiris.Eposta);

            if (kullanici == null || kullanici.Sifre != modelGiris.Sifre)
            {
                HataMesaji = "Eposta veya Şifre Hatalı.";
            }
            else
            {
                List<Claim> claims = new List<Claim>
            {

                new Claim(ClaimTypes.NameIdentifier, kullanici.Eposta),
                new Claim(ClaimTypes.Name, kullanici.Ad),
                new Claim("KullaniciId", kullanici.KullaniciId.ToString()),
                new Claim(ClaimTypes.Role, kullanici.Rol)
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                // return RedirectToAction("Giris", "Hesap");
                if (!string.IsNullOrEmpty(kullanici.Rol))
                {
                    if (kullanici.Rol == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (kullanici.Rol == "Calisan")
                    {
                        return RedirectToAction("Index", "Calisan");
                    }
                    else
                        return RedirectToAction("Menu", "Home");

                }
            }
        }
        else
        {
            HataMesaji = "Eposta ve Şifrenizi giriniz.";
        }

        ViewData["ValidateMessage"] = HataMesaji;
        return View();
    }

    public IActionResult KayitOl()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> KayitOl(VMKayitOl modelKayitOl)
    {
        string HataMesaji = "";
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
        string? strBaglanti = configuration.GetConnectionString("WebApiDatabase");
        var opt = new DbContextOptionsBuilder<AppDBContext>();
        opt.UseNpgsql(strBaglanti);
        _context = new AppDBContext(opt.Options);
        Kullanicilar dbkullanicilar = new Kullanicilar();

        if (modelKayitOl.Eposta != null && modelKayitOl.Ad != null)
        {
            dbkullanicilar.Ad = modelKayitOl.Ad;
            dbkullanicilar.Soyad = modelKayitOl.Soyad;
            dbkullanicilar.Eposta = modelKayitOl.Eposta;
            dbkullanicilar.Sifre = modelKayitOl.Sifre;
            dbkullanicilar.Telefon = modelKayitOl.Telefon;
            dbkullanicilar.Adres = modelKayitOl.Adres;
            dbkullanicilar.Rol = modelKayitOl.Rol;

            if (ModelState.IsValid)
            {
                var mevcutKullanici = await _context.Kullanicilar.FirstOrDefaultAsync(u => u.Eposta == modelKayitOl.Eposta);
                if (mevcutKullanici != null)
                {
                    HataMesaji = "Bu Eposta zaten kayıtlı.";
                    ViewData["ValidateMessage"] = HataMesaji;
                    return View(modelKayitOl);
                }

                if (!SifreKontrol(modelKayitOl.Sifre))
                {
                    HataMesaji = "Parola en az 8 karakter uzunluğunda olmalı ve bir büyük harf, bir küçük harf ile bir sayı içermelidir.";
                    ViewData["ValidateMessage"] = HataMesaji;
                    return View(modelKayitOl);
                }

                _context.Kullanicilar.Add(dbkullanicilar);
                _context.SaveChanges();
                return RedirectToAction("Giris", "Hesap");
            }
        }
        HataMesaji = "Lütfen gerekli alanları doldurun.";
        ViewData["ValidateMessage"] = HataMesaji;
        return View(modelKayitOl);

    }

    private bool SifreKontrol(string sifre)
    {
        var sifrePattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
        return Regex.IsMatch(sifre, sifrePattern);
    }

}