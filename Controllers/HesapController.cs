using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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

    public IActionResult KayitOl()
    {
        return View();
    }

}