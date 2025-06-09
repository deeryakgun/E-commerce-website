using System;
namespace e_commerce.Models
{
	public class PaymentResponseDto
	{
        public Data data { get; set; }
        public bool success { get; set; }
        public object message { get; set; }
        public string resultCode { get; set; }
    }

    public class Data
    {
        public string responseMessage { get; set; }
        public string orderId { get; set; }
        public string returnUrl { get; set; }
        public string errorCode { get; set; }
        public string status { get; set; }
        public string paymentId { get; set; }
        public string paymentData { get; set; }
        public int returnType { get; set; }
    }
}

