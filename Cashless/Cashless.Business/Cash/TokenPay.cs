using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cashless.Domain.Cash.Class;
using Cashless.Domain.Cash.Const;
using Cashless.Domain.Cash.Enum;
using Cashless.Domain.Cash.Interface;
using Cashless.Domain.Data.Class;

namespace Cashless.Business.Cash
{
    public class TokenPay : IPayment
    {
        private static int _paymentId = 1;
        public bool IsForMethod(PaymentRequest paymentRequest)
        {
            var valids = new List<PaymentEnum.PaymentMethod>()
            {
                PaymentEnum.PaymentMethod.ApplePay,
                PaymentEnum.PaymentMethod.GooglePay,
                PaymentEnum.PaymentMethod.Pix
            };

            return valids.Any(x => x == paymentRequest.PaymentMethod);
        }

        public PaymentResult Pay(PaymentRequest paymentRequest)
        {
            if (paymentRequest.PaymentMethod == PaymentEnum.PaymentMethod.Pix)
                return new PaymentResult
                {
                    Error = "Pix not implemented yet",
                    Success = false
                };

            var customer = Storage.Storage.Customers.FirstOrDefault(x => x.Id == paymentRequest.CustomerId);
            if (customer == null)
                return new PaymentResult
                {
                    Error = "Customer does not exist",
                    Success = false
                };


            var price = paymentRequest.TokenQuantity * Price.Amount;
            var tokenPaymentId = "TOKENPAY" + _paymentId;
            Storage.Storage.Purchases.Add(new Purchase()
            {
                CustomerId = paymentRequest.CustomerId,
                MoneySpent = price,
                PaymentId = tokenPaymentId,
                TokenQuantity = paymentRequest.TokenQuantity
            });

            customer.TokenQuantity += paymentRequest.TokenQuantity;
            _paymentId++;

            return new PaymentResult
            {
                Success = true,
                Error = null,
                Data = new PaymentResultData()
                {
                    AquiredTokenQuantity = paymentRequest.TokenQuantity,
                    CustomerName = customer.Name,
                    PaymentId = tokenPaymentId
                }
            };

        }
    }
}
