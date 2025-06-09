using System;
namespace e_commerce.Models
{
	public class LoginRequestDto
	{
        public string UserName { get; set; }
        public string Password { get; set; } // SHA512 ile hashlenmiş şifre olmalı
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}

