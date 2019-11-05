using CatalogMicroservice.Models;
using CatalogMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogMicroservice.DataManager
{
    public class PolicyManager : IDataRepository<InsurancePolicy>
    {
        readonly PolicyContext _policyContext;

        public PolicyManager(PolicyContext context)
        {
            _policyContext = context;
        }

        public IEnumerable<InsurancePolicy> GetAll()
        {
            return _policyContext.InsurancePolicies.ToList();
        }

        public InsurancePolicy Get(long id)
        {
            return _policyContext.InsurancePolicies
                  .FirstOrDefault(e => e.PolicyId == id);
        }

        public void Add(InsurancePolicy entity)
        {
            _policyContext.InsurancePolicies.Add(entity);
            _policyContext.SaveChanges();
        }

        public void Update(InsurancePolicy policy, InsurancePolicy entity)
        {
            policy.Name = entity.Name;
            policy.Description = entity.Description;
            policy.BasePrice = entity.BasePrice;

            _policyContext.SaveChanges();
        }

        public void Delete(InsurancePolicy policy)
        {
            _policyContext.InsurancePolicies.Remove(policy);
            _policyContext.SaveChanges();
        }
    }
}
