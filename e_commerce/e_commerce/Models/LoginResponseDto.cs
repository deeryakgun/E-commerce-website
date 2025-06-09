using System;
namespace e_commerce.Models
{
	public class LoginResponseDto
	{
        public DataResponse Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ResultCode { get; set; }
    }

    public class DataResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}

