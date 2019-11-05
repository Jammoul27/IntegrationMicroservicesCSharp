using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricingMicroservice.Models
{
    public class Order
    {
        public InsurancePolicy policy { get; set; }
        public double finalPrice { get; set; }
        public string email { get; set; }
    }
}
