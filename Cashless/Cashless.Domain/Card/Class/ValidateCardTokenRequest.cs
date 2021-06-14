namespace Cashless.Domain.Card.Class
{
    public class ValidateCardTokenRequest
    {
        public int CustomerId { get; set; }
        public int CardId { get; set; }
        public long Token { get; set; }
        public int CVV { get; set; }
    }
}