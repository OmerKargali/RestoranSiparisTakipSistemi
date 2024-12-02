using Microsoft.AspNetCore.Mvc;

namespace RestoranSiparisTakipSistemi.Controllers;

public class AdminController : Controller
{
    public AppDBContext _context;
    private readonly ILogger<HomeController> _logger;

    public AdminController(AppDBContext context, ILogger<HomeController> logger)
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
    public IActionResult UrunIslemleri()
    {
        return View();
    }
}