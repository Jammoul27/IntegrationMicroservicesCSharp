
namespace PricingMicroservice.Models
{
    public class InsurancePolicy
    {
        public long PolicyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; }

    }
}
