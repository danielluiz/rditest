namespace Cashless.Domain.Cash.Class
{
    public class CardPaymentData : PaymentData
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CardName { get; set; }
        public string Cvv { get; set; }
        public string CustomerDocument { get; set; }
    }
}
