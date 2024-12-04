using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RestoranSiparisTakipSistemi.Models;


public class Header : ViewComponent
{
    private readonly AppDBContext _context;

    public Header(AppDBContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        string strGirisGoster = "1";
        ClaimsPrincipal claimUser = HttpContext.User;
        if (claimUser.Identity?.IsAuthenticated == true)
        {
            strGirisGoster = "0";
        }
        ViewBag.GirisGoster = strGirisGoster;
        return View();
    }


}