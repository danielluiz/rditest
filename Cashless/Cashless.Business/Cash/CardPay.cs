using System.Linq;
using Cashless.Domain.Cash.Class;
using Cashless.Domain.Cash.Const;
using Cashless.Domain.Cash.Enum;
using Cashless.Domain.Cash.Interface;
using Cashless.Domain.Data.Class;

namespace Cashless.Business.Cash
{
    public class CardPay : IPayment
    {
        private static int _paymentId = 0;
        public bool IsForMethod(PaymentRequest paymentRequest)
        {
            return paymentRequest.PaymentMethod == PaymentEnum.PaymentMethod.CreditCard ||
                   paymentRequest.PaymentMethod == PaymentEnum.PaymentMethod.DebitCard;
        }

        public PaymentResult Pay(PaymentRequest paymentRequest)
        {
            if (paymentRequest.PaymentMethod == PaymentEnum.PaymentMethod.DebitCard)
                return new PaymentResult
                {
                    Error = "Debit Card not implemented yet",
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
            var tokenPaymentId = "CARDPAY" + _paymentId;
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
