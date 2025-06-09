using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace e_commerce.Models
{
    public class PaymentViewModel
    {
        [Required(ErrorMessage = "Kart Sahibinin Adı ve Soyadı gereklidir.")]
        [CreditCard(ErrorMessage = "Kart Sahibinin Adını ve Soyadını girin.")]
        [Display(Name = "Kart Sahibinin Adı ve Soyadı")]
        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "Kart Numarası gereklidir.")]
        [CreditCard(ErrorMessage = "Geçerli bir kart numarası girin.")]
        [Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Son Kullanma Yılı gereklidir.")]
        [Display(Name = "Son Kullanma Yılı (YYYY)")]
        public string ExpireYear { get; set; }

        [Required(ErrorMessage = "Son Kullanma Ayı gereklidir.")]
        [Display(Name = "Son Kullanma Ayı (MM)")]
        public string ExpireMonth { get; set; }

        [Required(ErrorMessage = "CVV gereklidir.")]
        public string CVV { get; set; }

        public decimal amount { get; set; }

    }

}


