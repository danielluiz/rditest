using System;
using System.Collections.Generic;
using System.Text;

namespace Cashless.Domain.Data.Class
{
    public class ApplicationStatus
    {
        public List<Vendor> Vendors { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Purchase> Purchases { get; set; }
        public List<Transfer> Transfers { get; set; }
    }
}
