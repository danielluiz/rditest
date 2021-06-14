using Cashless.Domain.Card.Class;
using Cashless.Domain.Common;

namespace Cashless.Domain.Card.Interface
{
    public interface ICard
    {
        Result<SaveCardResponse> SaveCard(SaveCardRequest req);

        Result<bool> ValidateToken(ValidateCardTokenRequest req);
    }
}