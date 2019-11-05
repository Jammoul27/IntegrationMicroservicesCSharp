using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricingMicroservice.Models
{
    public class ClientInfo
    {
        public int PolicyId { get; set; }
        public int age { get; set;}
        public int supported { get; set; }
        public int salary { get; set; }
        public string email { get; set; }
    }
}
