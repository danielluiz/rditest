using System;

namespace Cashless.Domain.Card.Class
{
    public class SaveCardResponse
    {
        public DateTime RegistrationDate { get; set; }
        public long Token { get; set; }
        public int CardId { get; set; }
    }
}