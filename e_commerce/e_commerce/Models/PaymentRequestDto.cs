using System;
namespace e_commerce.Models
{
	public class PaymentRequestDto
	{
        public decimal amount { get; set; }
        public int currencyCode { get; set; }
        public string orderId { get; set; }
        public int installment { get; set; }
        public string cardHolderName { get; set; }
        public string cardNumber { get; set; }
        public string expireYear { get; set; }
        public string expireMonth { get; set; }
        public string cvc { get; set; }
        public bool isSecureTransaction { get; set; }
        public string callbackUrl { get; set; }
    }
}

