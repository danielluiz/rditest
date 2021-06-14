using Cashless.Domain.Cashless.Class;

namespace Cashless.Domain.Cashless.Interface
{
    public interface ITokenTransfer
    {
        TokenTransferResult Transfer(TokenTransferRequest request);
    }
}
