using System;
using System.ComponentModel.DataAnnotations;

namespace e_commerce.Dto
{
	public class AppUserRegisterDto
	{
        [Display(Name = "Adınız")]
        [Required(ErrorMessage = "Bu alanı boş bırakamazsınız.")]
        public string FirstName { get; set; }

        [Display(Name = "Soyadınız")]
        [Required(ErrorMessage = "Bu alanı boş bırakamazsınız.")]
        public string LastName { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Bu alanı boş bırakamazsınız.")]
        public string UserName { get; set; }

        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "Bu alanı boş bırakamazsınız.")]
        public string City { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Bu alanı boş bırakamazsınız.")]
        public string Email { get; set; }

        

        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu alanı boş bırakamazsınız.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bu alanı boş bırakamazsınız.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
    }
}

