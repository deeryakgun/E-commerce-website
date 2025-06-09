using e_commerce.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Models;
using Microsoft.AspNetCore.Identity;
using e_commerce.Dto;

namespace e_commerce.Controllers;

public class HomeController : Controller
{
    
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<AppUser> _userManager;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Detay(int id)
    {
        var result = _context.Products.Find(id);
        return View(result);
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult Products()
    {
        var result = _context.Products.ToList();
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> UserDetails()
    {
        var values = await _userManager.FindByNameAsync(User.Identity.Name);
        AppUserEditDto appUserEditDto = new AppUserEditDto();
        appUserEditDto.FirstName = values.FirstName;
        appUserEditDto.LastName = values.LastName;
        appUserEditDto.PhoneNumber = values.PhoneNumber;
        appUserEditDto.City = values.City;
        appUserEditDto.Email = values.Email;

        return View(appUserEditDto);
    }

    [HttpPost]
    public async Task<IActionResult> UserDetails(AppUserEditDto appUserEditDto)
    {
        if (appUserEditDto.Password == appUserEditDto.ConfirmPassword)
        {
            var user = await _userManager.FindByEmailAsync(appUserEditDto.Email);
            user.FirstName = appUserEditDto.FirstName;
            user.LastName = appUserEditDto.LastName;
            user.City = appUserEditDto.City;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, appUserEditDto.Password);
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("UserDetails", "Home");
            }

        }
        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

