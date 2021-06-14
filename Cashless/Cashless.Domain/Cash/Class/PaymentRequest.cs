using System;
using Cashless.Domain.Cash.Enum;
using Newtonsoft.Json.Linq;

namespace Cashless.Domain.Cash.Class
{
    public class PaymentRequest
    {
        public string CustomerId { get; set; }
        public PaymentEnum.PaymentMethod PaymentMethod { get; set; }
        public int TokenQuantity { get; set; }
        public PaymentData PaymentData { get; set; }

        public static PaymentRequest FromJson(JObject json, string customerId)
        {
            var method = json["paymentMethod"].Value<string>();
            var paymentMethod = PaymentEnum.GetFromString(method);
            var tokenQuantity = json["tokenQuantity"].Value<int>();

            var result = new PaymentRequest
            {
                PaymentMethod = paymentMethod,
                TokenQuantity = tokenQuantity,
                CustomerId = customerId
            };

            var paymentData = json["paymentData"].Value<JObject>();
            switch (paymentMethod)
            {
                case PaymentEnum.PaymentMethod.CreditCard:
                case PaymentEnum.PaymentMethod.DebitCard:
                    result.PaymentData = paymentData.ToObject<CardPaymentData>();
                    break;
                case PaymentEnum.PaymentMethod.GooglePay:
                case PaymentEnum.PaymentMethod.ApplePay:
                case PaymentEnum.PaymentMethod.Pix:
                    result.PaymentData = paymentData.ToObject<TokenPaymentData>();
                    break;
                default:
                    throw new Exception($"Object with invalid payment method:  {json}");
            }

            return result;
        }
    }
}
