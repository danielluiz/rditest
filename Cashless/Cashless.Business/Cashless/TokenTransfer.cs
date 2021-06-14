using System.Linq;
using Cashless.Domain.Cashless.Class;
using Cashless.Domain.Cashless.Interface;
using Cashless.Domain.Data.Class;

namespace Cashless.Business.Cashless
{
    public class TokenTransfer : ITokenTransfer
    {
        public TokenTransferResult Transfer(TokenTransferRequest request)
        {
            var customer = Storage.Storage.Customers.FirstOrDefault(x => x.Id == request.CustomerId);
            if (customer == null)
                return new TokenTransferResult()
                {
                    Error = "Customer does not exist",
                    Success = false
                };

            var vendor = Storage.Storage.Vendors.FirstOrDefault(x => x.Id == request.VendorId);
            if (vendor == null)
                return new TokenTransferResult()
                {
                    Error = "Vendor does not exist",
                    Success = false
                };

            if (customer.TokenQuantity < request.TokenQuantity)
            {
                return new TokenTransferResult()
                {
                    Error = "Customer does not have enough tokens",
                    Success = false
                };
            }

            Storage.Storage.Transfers.Add(new Transfer()
            {
                TokenQuantity = request.TokenQuantity,
                CustomerId = request.CustomerId,
                VendorId = request.VendorId
            });

            customer.TokenQuantity -= request.TokenQuantity;
            vendor.TokenQuantity += request.TokenQuantity;

            return new TokenTransferResult
            {
                Success = true,
                Error = null,
                Data = new TokenTransferData()
                {
                    CustomerId = customer.Id,
                    CustomerName = customer.Name,
                    VendorId = vendor.Id,
                    VendorName = vendor.Name,
                    TransferredTokenQuantity = request.TokenQuantity
                }
            };
        }
    }
}
