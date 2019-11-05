using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogMicroservice.Models;
using CatalogMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CatalogMicroservice.Controllers
{
    [Route("api/policy")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IDataRepository<InsurancePolicy> _dataRepository;

        public PolicyController(IDataRepository<InsurancePolicy> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/policy
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<InsurancePolicy> policies = _dataRepository.GetAll();
            return Ok(policies);
        }

        // GET: api/policy/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            InsurancePolicy policy = _dataRepository.Get(id);

            if (policy == null)
            {
                return NotFound("The Policy couldn't be found.");
            }

            return Ok(policy);
        }

        // POST: api/policy
        [HttpPost]
        public IActionResult Post([FromBody] InsurancePolicy policy)
        {
            if (policy == null)
            {
                return BadRequest("Policy is null.");
            }

            _dataRepository.Add(policy);
            return CreatedAtRoute(
                  "Get",
                  new { Id = policy.PolicyId },
                  policy);
        }

        // PUT: api/policy/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] InsurancePolicy policy)
        {
            if (policy == null)
            {
                return BadRequest("Policy is null.");
            }

            InsurancePolicy policyToUpdate = _dataRepository.Get(id);
            if (policyToUpdate == null)
            {
                return NotFound("The Policy couldn't be found.");
            }

            _dataRepository.Update(policyToUpdate, policy);
            return NoContent();
        }

        // DELETE: api/policy/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            InsurancePolicy policy = _dataRepository.Get(id);
            if (policy == null)
            {
                return NotFound("The Policy couldn't be found.");
            }

            _dataRepository.Delete(policy);
            return NoContent();
        }
    }
}
