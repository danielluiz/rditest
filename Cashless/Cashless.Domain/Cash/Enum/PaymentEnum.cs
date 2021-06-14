namespace Cashless.Domain.Cash.Enum
{

    public static class PaymentEnum
    {
        public enum PaymentMethod
        {
            CreditCard = 1,
            DebitCard = 2,
            GooglePay = 3,
            ApplePay = 4,
            Pix = 5
        }
        public static PaymentMethod GetFromString(string stringMethod)
        {
            return (PaymentMethod) System.Enum.Parse(typeof(PaymentMethod), stringMethod, true);
        }
    }
}
