using Cashless.Domain.Cash.Class;

namespace Cashless.Domain.Cash.Interface
{
    public interface IPayment
    {
        bool IsForMethod(PaymentRequest paymentRequest);

        PaymentResult Pay(PaymentRequest paymentRequest);
    }
}
