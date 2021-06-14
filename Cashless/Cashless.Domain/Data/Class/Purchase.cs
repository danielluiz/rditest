namespace Cashless.Domain.Data.Class
{
    public class Purchase
    {
        public string CustomerId { get; set; }
        public int TokenQuantity { get; set; }
        public decimal MoneySpent { get; set; }
        public string PaymentId { get; set; }
    }
}
