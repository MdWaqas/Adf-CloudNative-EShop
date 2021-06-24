using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API
{
    public class PaymentSettings
    {
        public bool PaymentSucceeded { get; set; }
        public decimal? MaxOrderTotal { get; set; }
    }
}
