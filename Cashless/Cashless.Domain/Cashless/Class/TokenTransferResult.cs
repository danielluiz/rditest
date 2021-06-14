namespace Cashless.Domain.Cashless.Class
{
    public class TokenTransferResult
    {
        public string Error { get; set; }
        public bool Success { get; set; }
        public TokenTransferData Data { get; set; }
    }

    public class TokenTransferData
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public int TransferredTokenQuantity { get; set; }
    }
}
