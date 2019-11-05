using Microsoft.EntityFrameworkCore;


namespace CatalogMicroservice.Models
{
    public class PolicyContext : DbContext
    {
        public PolicyContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<InsurancePolicy> InsurancePolicies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InsurancePolicy>().HasData(
                new InsurancePolicy
            {
                PolicyId = 1,
                Name = "Miniature Policy",
                Description = "This is the most basic policy, for families of size 1-3",
                BasePrice = 106040,
            }, new InsurancePolicy
            {
                PolicyId = 2,
                Name = "Small Sized Family Policy",
                Description = "This is a policy for a family of size 4-6",
                BasePrice = 87640
            }, new InsurancePolicy
            {
                PolicyId = 3,
                Name = "Medium Sized Family Policy",
                Description = "This policy is for families consiting of 7-9 members",
                BasePrice = 76810
            });
        }
    }

}
