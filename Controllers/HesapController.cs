using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestoranSiparisTakipSistemi.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using RestoranSiparisTakipSistemi.Models;

public class HesapController : Controller
{
    public AppDBContext _context;

    public HesapController(AppDBContext context)
    {
        _context = context;
    }

    public IActionResult Giris()
    {
        ClaimsPrincipal claimUser = HttpContext.User;

        if (claimUser.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Menu", "Home");
        }
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
            // Kullanıcıyı veritabanından kontrol et
            Kullanicilar? kullanici = _context.Kullanicilar.FirstOrDefault(x => x.Eposta == modelGiris.Eposta);

            if (kullanici == null || kullanici.Sifre != modelGiris.Sifre)
            {
                HataMesaji = "Eposta veya Şifre Hatalı.";
            }
            else
            {
                // Kullanıcı için Claim oluştur
                List<Claim> claims = new List<Claim>
            {

                new Claim(ClaimTypes.NameIdentifier, kullanici.Eposta), // Kullanıcı e-postasını NameIdentifier olarak ekle
                new Claim(ClaimTypes.Name, kullanici.Ad),             // Kullanıcı adını Name olarak ekle
                new Claim("KullaniciId", kullanici.KullaniciId.ToString()) // Kullanıcı ID'sini özel bir Claim olarak ekle
            };

                // Kimlik oluşturma ve oturum başlatma
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Başarılı giriş sonrası yönlendirme
                return RedirectToAction("Menu", "Home");
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
                return RedirectToAction("Menu", "Home");
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