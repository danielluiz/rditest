using System;
using System.Collections.Generic;
using System.Text;
using Cashless.Domain.Data.Class;

namespace Cashless.Domain.Data
{
    public interface IStatus
    {
        ApplicationStatus GetStatus();
    }
}
