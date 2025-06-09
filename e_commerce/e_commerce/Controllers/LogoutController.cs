using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_commerce.Controllers
{
    public class LogoutController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;

        public LogoutController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            await _signInManager.SignOutAsync();
            ViewBag.Mesaj = "Hesaptan Çıkış Yapıldı";
            return RedirectToAction("Index", "Home");
        }
    }
}

