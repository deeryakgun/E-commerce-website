using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using e_commerce.Models;
using RestSharp;
using System.Security.Cryptography;
using System.Text;
using MailKit.Search;
using Org.BouncyCastle.Asn1.X509;
using e_commerce.Dto;
using e_commerce.Data;
using e_commerce.Oturum;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace e_commerce.Controllers
{
    public class PaymentController : Controller
    {
        private readonly RestClient _client;
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _client = new RestClient("https://virtualapi.hstmobil.com.tr");
            _context = context;
        }

        public string ComputeSha512Hash(string rawData)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            if(items != null) {
                CartViewModel cartvm = new()
                {
                    CartItems = items,
                    GrandTotal = items.Sum(x => x.Quantity * x.Price)
                };
                ViewBag.CartViewModel = cartvm;
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PaymentViewModel model)
        {
            var hashedPassword = ComputeSha512Hash("177492");
            LoginRequestDto requestData = new LoginRequestDto()
            {
                UserName = "5452045674",
                Password = hashedPassword,
                Lat = "",
                Long =""
            };

            var request = new RestRequest("/api/Auth/login", Method.Post)
                .AddJsonBody(requestData) 
                .AddHeader("Content-Type", "application/json"); 

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // API cevabı başarılıysa işlenir
                var responseData = JsonConvert.DeserializeObject<LoginResponseDto>(response.Content);

                var token = responseData.Data.Token;

                

                PaymentRequestDto newReq = new PaymentRequestDto()
                {
                    amount = model.amount,
                    currencyCode = 949,
                    orderId = Guid.NewGuid().ToString(),
                    installment = 1,
                    cardHolderName = model.CardHolderName,
                    cardNumber = model.CardNumber,
                    expireYear = model.ExpireYear,
                    expireMonth = model.ExpireMonth,
                    cvc = model.CVV,
                    isSecureTransaction = true,
                    callbackUrl = "https://localhost:7236/Payment/PaymentResult"
                };
   
                var paymentRequest = new RestRequest("/api/Payments/Payment", Method.Post)
                    .AddJsonBody(JsonConvert.SerializeObject( newReq)) // Ödeme bilgilerini ekle
                    .AddHeader("Authorization", $"Bearer {token}") // Token'ı header'a ekle
                    .AddHeader("Content-Type", "application/json"); // Header ekle

                var paymentResponse = await _client.ExecuteAsync(paymentRequest);

                if (paymentResponse.IsSuccessful)
                {
                    var paymentResponseData = JsonConvert.DeserializeObject<PaymentResponseDto>(paymentResponse.Content);


                    if(paymentResponseData.data != null)
                    {
                        if (paymentResponseData.data.returnType == 2)
                        {
                            var paymentHtml = paymentResponseData.data.paymentData;
                           
                            return Content(paymentHtml, "text/html");
                        }
                        else if (paymentResponseData.data.returnType == 1)
                        {
                            var paymentLink = paymentResponseData.data.returnUrl;
                            return Redirect(paymentLink);
                        }
                        else
                        {
                            return Json(new { success = false, message = "Geçersiz ödeme yanıtı." });
                        }
                    }
                   
                    else
                    {
                        return Json(new { success = false, message = "Geçersiz ödeme yanıtı." });
                    }

                }
                else
                {
                    // Ödeme başarısızsa hata mesajı göster
                    TempData["Error"] = "Ödeme başarısız oldu: " + paymentResponse.ErrorMessage;
                    return View();
                }


            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ErrorMessage);
            }
        }


        [HttpGet]
        public IActionResult PaymentResult()
        {
           // List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart");
           // if (items != null)
             //   ViewBag.CartItems = items;

            return View();
        }




        [HttpPost]
        public async Task<IActionResult> PaymentResult([FromForm] AnswerModel answerModel)
        {


            var hashedPassword = ComputeSha512Hash("177492");
            LoginRequestDto requestData = new LoginRequestDto()
            {
                UserName = "5452045674",
                Password = hashedPassword,
                Lat = "",
                Long = ""
            };

            // POST isteği oluşturulur
            var request = new RestRequest("/api/Auth/login", Method.Post)
                .AddJsonBody(requestData) // JSON veriyi ekle
                .AddHeader("Content-Type", "application/json"); // Header ekle

            // İstek gönderilir
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // API cevabı başarılıysa işlenir
                var responseData = JsonConvert.DeserializeObject<LoginResponseDto>(response.Content);

                var token = responseData.Data.Token;

                var url = "https://virtualapi.hstmobil.com.tr/api/Payments/PaymentResult";




                var paymentResult = new RestRequest("/api/Payments/PaymentResult", Method.Post);
                paymentResult.AddQueryParameter("paymentId", answerModel.paymentId);
                paymentResult.AddQueryParameter("orderId", answerModel.orderId);
                paymentResult.AddHeader("Authorization", $"Bearer {token}");

                var result = await _client.ExecuteAsync(paymentResult);

                string message = "";

                if (result.IsSuccessful)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore // Null değerleri yok sayar
                    };

                    PaymentResultDto resultResponse = JsonConvert.DeserializeObject<PaymentResultDto>(result.Content, settings);
                    if (resultResponse.data.status == "SUCCESS")
                    {
                        message = "Ödemeniz başarıyla gerçekleşmiştir.";
                    }
                    else if (resultResponse.data.status == "PENDING")
                    {
                        message = "Ödemeniz henüz tamamlanmadı. Lütfen onay sürecini bekleyin.";
                    }
                    else
                    {
                        message = "Ödeme başarısız. Lütfen bankanızla iletişime geçiniz.";
                    }


                    ViewBag.Message = message;
                }
                else
                {
                    ViewBag.Message = "İşlem Başarısız.";
                }



            }
            return View();
        }

            


       
    }
}
