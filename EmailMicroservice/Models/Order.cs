using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMicroservice.Models
{
    class Order
    {
        public InsurancePolicy policy { get; set; }
        public double finalPrice { get; set; }
        public string email { get; set; }
    }
}
