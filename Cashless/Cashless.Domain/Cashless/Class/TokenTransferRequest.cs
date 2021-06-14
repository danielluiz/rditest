namespace Cashless.Domain.Cashless.Class
{
    public class TokenTransferRequest
    {
        public string CustomerId { get; set; }
        public string VendorId { get; set; }
        public int TokenQuantity { get; set; }
    }
}
