using Microsoft.AspNetCore.Mvc;


public class Header : ViewComponent
{
    private readonly AppDBContext _context;

    public Header(AppDBContext context)
    {
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}