using System;
namespace e_commerce.Models
{
    public class PaymentResultDto
    {

        public Data data { get; set; }
        public string errorInfo { get; set; }
        public string messages { get; set; }
        public bool succeeded { get; set; }



        public class Data
        {
            public string paymentId { get; set; }
            public string orderId { get; set; }
            public decimal amount { get; set; }
            public DateTime firstInquiryTime { get; set; }
            public bool inquiryAlreadyCalled { get; set; }
            public DateTime operationDate { get; set; }
            public string status { get; set; }
            public string code { get; set; }
            public string message { get; set; }
            public string operation { get; set; }
            public string transactionOrderId { get; set; }
        }

    }


}

