using System.Linq;
using Cashless.Business.Cash;
using Cashless.Business.Cashless;
using Cashless.Business.Data;
using Cashless.Domain.Cash.Class;
using Cashless.Domain.Cash.Enum;
using Cashless.Domain.Cashless.Class;
using NUnit.Framework;

namespace Cashless.Test
{
    public class Spend
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldTransferTokens()
        {
            var req = new TokenTransferRequest()
            {
                CustomerId = "CUS1",
                VendorId = "VENDOR1",
                TokenQuantity = 10
            };
            var status = new Status().GetStatus();
            var customer = status.Customers.First(x => x.Id == req.CustomerId);
            var vendor = status.Vendors.First(x => x.Id == req.VendorId);
            var customerTokenStart = customer.TokenQuantity;
            var vendorTokenStart = vendor.TokenQuantity;
            var res = new TokenTransfer().Transfer(req);
            if (res.Success)
            {
                status = new Status().GetStatus();
                customer = status.Customers.First(x => x.Id == req.CustomerId);
                vendor = status.Vendors.First(x => x.Id == req.VendorId);
                Assert.AreEqual(customerTokenStart - req.TokenQuantity, customer.TokenQuantity);
                Assert.AreEqual(vendorTokenStart + req.TokenQuantity, vendor.TokenQuantity);
            }
            else
                Assert.Fail(res.Error);
        }

        [Test]
        public void ShouldFail()
        {
            var req = new TokenTransferRequest()
            {
                CustomerId = "CUS2",
                VendorId = "VENDOR1",
                TokenQuantity = 10
            };
            var res = new TokenTransfer().Transfer(req);
            if (res.Success)
                Assert.Fail("Customer with 0 tokens bought something");
            else
                Assert.Pass(res.Error);
        }
    }
}