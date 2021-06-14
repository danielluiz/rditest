using System;
using System.Collections.Generic;
using System.Linq;
using Cashless.Domain.Cash.Class;
using Cashless.Domain.Cash.Interface;
using Cashless.Domain.Cashless.Class;
using Cashless.Domain.Cashless.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cashless.Web.Controllers
{
    /// <summary>
    /// Route for token purchase and spending
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IEnumerable<IPayment> _cashPaymentHandlers;
        private ITokenTransfer _tokenTransfer;
        public TokenController(IEnumerable<IPayment> payments, ITokenTransfer tokenTransfer)
        {
            _tokenTransfer = tokenTransfer;
            _cashPaymentHandlers = payments;

        }

        /// <summary>
        /// Buy tokens, each for R$ 0,10
        ///The endpoint has to be called like this:
        /// 
        /// ROUTE: /token/buy
        /// METHOD: POST
        /// HEADERS:
        /// Authorization: Bearer [Token]
        /// DATA:
        /// {
        /// "paymentMethod": "",
        /// "paymentData": {
        /// [This object will change based on paymentMethod]
        /// }
        /// "tokenQuantity": 0        
        /// }
        /// 
        /// These are the payment methods available at the moment:
        /// 
        /// CreditCard
        /// DebitCard
        /// GooglePay
        /// ApplePay
        /// Pix
        /// 
        /// For the CreditCard and DebitCard payment methods, the paymentData is:
        /// 
        /// {
        /// "cardNumber": "",
        /// "expirationDate": "",
        /// "cardName": "",
        /// "cvv": "",
        /// "customerDocument": ""
        /// }
        /// 
        /// For the GooglePay, ApplePay and Pix payment methods, the paymentData is:
        /// 
        /// {
        /// "token": ""
        /// }
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("buy")]
        public ActionResult<PaymentResult> Buy([FromBody]JObject obj)
        {
            try
            {
                var customerId = HttpContext.Items["customerId"].ToString();
                var body = PaymentRequest.FromJson(obj, customerId);
                var handler = _cashPaymentHandlers.FirstOrDefault(x => x.IsForMethod(body));
                if (handler == null)
                {
                    return StatusCode(500, new PaymentResult()
                    {
                        Success = false,
                        Error = "No payment handler for payment method"
                    });
                }

                var paymentResult = handler.Pay(body);
                return paymentResult.Success ? Ok(paymentResult) : StatusCode(500, paymentResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PaymentResult()
                {
                    Error = ex.Message,
                    Success = false
                });
            }
        }

        // POST api/<TokenController>
        /// <summary>
        /// Spend the tokens, transfering them to a vendor
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("spend")]
        public ActionResult<TokenTransferResult> Spend([FromBody] TokenTransferRequest req)
        {
            var customerId = HttpContext.Items["customerId"].ToString();
            req.CustomerId = customerId;
            try
            {
                var result = _tokenTransfer.Transfer(req);
                return result.Success ? Ok(result) : StatusCode(500, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new TokenTransferResult()
                {
                    Error = ex.Message,
                    Success = false
                });
            }
        }
    }
}
