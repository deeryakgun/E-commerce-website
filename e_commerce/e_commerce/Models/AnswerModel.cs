using System;
namespace e_commerce.Models
{
	public class AnswerModel
	{
		public string? orderId { get; set; }
        public string? paymentId { get; set; }
        public string? random { get; set; }
        public string? status { get; set; }
        public string? amount { get; set; }
        public string? hash { get; set; }
        public DateTime? operationDate { get; set; }
        public string? errorCode { get; set; }
        public string? errorMessage { get; set; }
        public string? bankAuthCode { get; set; }

    }
}

