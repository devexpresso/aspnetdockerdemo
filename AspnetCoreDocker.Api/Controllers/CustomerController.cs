using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreDocker.BusinessLayer.Interface;
using AspnetCoreDocker.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetCoreDocker.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICatalogue<Customer> _customer;
        private readonly ILogon _customerLogin;

        public CustomerController(ICatalogue<Customer> customer, ILogon customerLogin)
        {
            _customer = customer;
            _customerLogin = customerLogin;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _customer.GetList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _customer.GetDetails(id);
        }

        // POST api/<controller>
        [HttpPost]
        public bool Post([FromBody]Customer value, string password)
        {
            var customerId = _customer.AddNewRecord(value);
            var customerInfo = _customer.GetDetails(customerId);

            var result = _customerLogin.SaveCustomerLogonInformation(new CustomerLogin
            {
                CustomerId = customerId, LogonEmail = customerInfo.Email, LogonPassword = password
            });
            return result;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]Customer value)
        {
            return _customer.UpdateRecord(id, value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _customer.DeleteRecord(id);
        }
    }
}
