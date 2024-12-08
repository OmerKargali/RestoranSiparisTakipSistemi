using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        return View();
    }
        public IActionResult ProfilDuzenleme()
    {
        return View();
    }
            public IActionResult SiparisDetay()
    {
        return View();
    }
}