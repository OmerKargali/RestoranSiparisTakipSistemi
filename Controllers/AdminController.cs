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
        return View();
    }

    public IActionResult GenelTablolar()
    {
        return View();
    }
    public IActionResult VeriTablolari()
    {
        return View();
    }

    public IActionResult UrunEkleme()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UrunEkleme(VMUrunler modelUrunler)
    {
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
}