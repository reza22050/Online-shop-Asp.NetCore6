using Application.Payments;
using Dto.Payment;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebSite.EndPoint.Utilities;
using ZarinPal.Class;

namespace WebSite.EndPoint.Controllers
{
    public class PayController : Controller
    {
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;

        private readonly IConfiguration _configuration;
        private readonly IPaymentService _paymentService;
        private readonly string merchendId;
        public PayController(IConfiguration configuration, IPaymentService paymentService)
        {
            
            _configuration = configuration;
            this._paymentService = paymentService;
            merchendId = _configuration["ZarinpalMerchendId"];


            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();

        }

        public async Task<IActionResult> Index(Guid PaymentId)
        {
            var payment = _paymentService.GetPayment(PaymentId);

            if (payment == null)
            {
                return NotFound();
            }

            string userId = ClaimUtility.GetUserId(User);

            if (userId != payment.UserId)
            {
                return BadRequest();
            }

            string callbackUrl = Url.Action(nameof(Verify), "Pay", new { payment.Id }, protocol: Request.Scheme);

            var resultZarinpalRequest = await _payment.Request(new DtoRequest()
            {
                Amount = (int)payment.Amount,
                CallbackUrl = callbackUrl,
                Description = payment.Description,
                Email = payment.Email,
                MerchantId = merchendId,
                Mobile = payment.PhoneNumber
            }, Payment.Mode.sandbox);

            return Redirect($"https://zarinpal.com/pg/StartPay/{resultZarinpalRequest.Authority}");
        }


        public IActionResult Verify(Guid Id,string Authority)
        {
            string Status = HttpContext.Request.Query["Status"];
            if (Status != "" && Status.ToString().ToLower() == "ok" && Authority != "")
            {
                var payment = _paymentService.GetPayment(Id);
                if (payment == null)
                {
                    return NotFound();
                }

                //var verification = _payment.Verification(new DtoVerification()
                //{
                //    Amount = (int)payment.Amount,
                //    Authority = Authority,
                //    MerchantId = merchendId
                //}, Payment.Mode.sandbox).Result;

                var client = new RestClient("https://www.zarinpal.com/pg/rest/WebGate/PaymentVerification.json");

                var request = new RestRequest("", Method.Post);
                request.AddHeader("Contextn-Type", "application/json");
                request.AddParameter("application/json", $"{{\"MechantID\" :\"{merchendId}\",\"Authority\":\"{Authority}\",\"Amount\":\"{(int)payment.Amount}\"}}", ParameterType.RequestBody);

                var response = client.ExecuteAsync(request).Result;

                VerificationPayResultDto verification = JsonConvert.DeserializeObject<VerificationPayResultDto>(response.Content);

                if (verification.Status == 100)
                {
                   bool verifyResult = _paymentService.VerifyPayment(Id, Authority, verification.RefID);
                    if (verifyResult)
                    {
                        return Redirect("/customers/orders");
                    }
                    else
                    {
                        TempData["message"] = "Payment is succeded but not verified";
                        return RedirectToAction("checkout", "basket");
                    }
                }
                else {
                    TempData["message"] = "Not successful. Try out again!";
                    return RedirectToAction("checkout", "basket");
                }
            }
            TempData["message"] = "Not successful. Try out again!";
            return RedirectToAction("checkout", "basket");
        }

    }

    public class VerificationPayResultDto
    {
        public int Status { get; set; }
        public long RefID { get; set; }
    }
}
