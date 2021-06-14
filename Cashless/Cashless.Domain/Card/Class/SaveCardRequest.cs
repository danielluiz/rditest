namespace Cashless.Domain.Card.Class
{
    public class SaveCardRequest
    {
        public int CustomerId { get; set; }
        public long CardNumber { get; set; }
        public int CVV { get; set; }
    }
}