using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public IActionResult Giris(string Eposta, string Sifre)
    {

        var kullanici = _context.Kullanicilar.FirstOrDefault(u => u.Eposta == Eposta && u.Sifre == Sifre);

        if (kullanici != null)
        {
            return RedirectToAction("Menu", "Home");
        }

        return View();

    }

    public IActionResult KayitOl()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> KayitOl(VMKayitOl modelKayitOl)
    {
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
                    ModelState.AddModelError("Eposta", "Bu Eposta zaten kayıtlı.");
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            ModelState.AddModelError("", error.ErrorMessage);
                        }
                    }
                    return View(modelKayitOl);
                }
                _context.Kullanicilar.Add(dbkullanicilar);
                _context.SaveChanges();
                return RedirectToAction("Menu", "Home");
            }
        }
        return View(modelKayitOl);
    }
}