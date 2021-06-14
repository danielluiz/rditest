using System.Linq;
using Cashless.Business.Cash;
using Cashless.Business.Data;
using Cashless.Domain.Cash.Class;
using Cashless.Domain.Cash.Enum;
using NUnit.Framework;

namespace Cashless.Test
{
    public class Buy
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CardShouldAddToCustomer()
        {
            var req = new PaymentRequest()
            {
                PaymentMethod = PaymentEnum.PaymentMethod.CreditCard,
                CustomerId = "CUS1",
                TokenQuantity = 10
            };
            var status = new Status().GetStatus();
            var customer = status.Customers.First(x => x.Id == req.CustomerId);
            var tokenStart = customer.TokenQuantity;
            var res = new CardPay().Pay(req);
            if (res.Success)
            {
                status = new Status().GetStatus();
                customer = status.Customers.First(x => x.Id == req.CustomerId);
                Assert.AreEqual(tokenStart + 10, customer.TokenQuantity);
            }
            else
                Assert.Fail(res.Error);
        }

        [Test]
        public void CardShouldFail()
        {
            var req = new PaymentRequest()
            {
                PaymentMethod = PaymentEnum.PaymentMethod.DebitCard,
                CustomerId = "CUS1"
            };
            var res = new CardPay().Pay(req);
            if (res.Success)
                Assert.Fail("Debit successful");
            else
                Assert.Pass(res.Error);
        }


        [Test]
        public void TokenShouldAddToCustomer()
        {
            var req = new PaymentRequest()
            {
                PaymentMethod = PaymentEnum.PaymentMethod.GooglePay,
                CustomerId = "CUS1",
                TokenQuantity = 10
            };
            var status = new Status().GetStatus();
            var customer = status.Customers.First(x => x.Id == req.CustomerId);
            var tokenStart = customer.TokenQuantity;
            var res = new TokenPay().Pay(req);
            if (res.Success)
            {
                status = new Status().GetStatus();
                customer = status.Customers.First(x => x.Id == req.CustomerId);
                Assert.AreEqual(tokenStart + 10, customer.TokenQuantity);
            }
            else
                Assert.Fail(res.Error);
        }

        [Test]
        public void TokenShouldFail()
        {
            var req = new PaymentRequest()
            {
                PaymentMethod = PaymentEnum.PaymentMethod.Pix,
                CustomerId = "CUS1"
            };
            var res = new TokenPay().Pay(req);
            if (res.Success)
                Assert.Fail("Pix successful");
            else
                Assert.Pass(res.Error);
        }
    }
}