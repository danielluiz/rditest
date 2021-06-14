using Cashless.Domain.Data;
using Cashless.Domain.Data.Class;

namespace Cashless.Business.Data
{
    public class Status : IStatus
    {
        public ApplicationStatus GetStatus()
        {
            return Storage.Storage.GetStatus();
        }
    }
}
