using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using e_commerce.Models;

namespace e_commerce.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel gelen)
        {
            var result = await _signInManager.PasswordSignInAsync(gelen.UserName, gelen.Password, false, true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(gelen.UserName);
                if(user.EmailConfirmed == true)
                {
                    return RedirectToAction("UserDetails", "Home");
                }
            }

            return View();
        }

        public IActionResult AccesDenied()
        {
            return View();
        }
    }
}

