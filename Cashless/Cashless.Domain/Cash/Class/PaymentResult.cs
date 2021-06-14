namespace Cashless.Domain.Cash.Class
{
    // The result could be better if there was a common result object and the middleware knew hou to handle it
    public class PaymentResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public PaymentResultData Data { get; set; }
    }

    public class PaymentResultData
    {
        public int AquiredTokenQuantity { get; set; }
        public string CustomerName { get; set; }

        public string PaymentId { get; set; }
    }
}
