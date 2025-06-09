using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using e_commerce.Dto;
using e_commerce.Models;

namespace e_commerce.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] AppUserRegisterDto appUserRegisterDto)
        {
            var errors = new Dictionary<string, string>();

            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Count > 0)
                    {
                        errors.Add(state.Key, state.Value.Errors[0].ErrorMessage);
                    }
                }
                return Json(new { showModal = false, errors });
            }

            Random random = new Random();
            int code = random.Next(10000, 1000000);
            AppUser appuser = new AppUser()
            {
                FirstName = appUserRegisterDto.FirstName,
                LastName = appUserRegisterDto.LastName,
                City = appUserRegisterDto.City,
                Email = appUserRegisterDto.Email,
                UserName = appUserRegisterDto.UserName,
                ConfirmCode = code,
            };

            var result = await _userManager.CreateAsync(appuser, appUserRegisterDto.Password);

            if (result.Succeeded)
            {
                try
                {
                    var mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress("Eticaret Uygulaması", "akgunderyaa@gmail.com"));
                    mimeMessage.To.Add(new MailboxAddress("User", appuser.Email));
                    mimeMessage.Subject = "ETicaret Uygulaması";

                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = $"<h1>Kaydınız Başarılı Şekilde Gerçekleşti.</h1><p>Onay Kodunuz: {code}</p>"
                    };
                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("akgunderyaa@gmail.com", "my password"); // Güvenlik için parola yerine uygulama şifresi kullanın
                        client.Send(mimeMessage);
                        client.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    errors.Add("EmailError", $"Email gönderiminde bir hata oluştu: {ex.Message}");
                    return Json(new { showModal = false, errors });
                }

                // Onay kodunu TempData'ya kaydediyoruz
                TempData["Mail"] = appuser.Email;
                
                return Json(new { showModal = true });
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    errors.Add("ServerError", item.Description);
                }
                return Json(new { showModal = false, errors });

            }
        }

        [HttpGet]
        public IActionResult ConfirmMail()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmMail([FromBody] ConfirmUser gelen)
        {
            var user = await _userManager.FindByEmailAsync(gelen.Mail);
            if (user != null && user.ConfirmCode == gelen.ConfirmCode)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return Json(new { showModal = true });
            }
            return Json(new { showModal = false });
        }
    }
}
