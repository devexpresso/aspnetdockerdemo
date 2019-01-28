using System;
using System.Collections.Generic;
using System.Text;
using AspnetCoreDocker.BusinessLayer.Interface;
using AspnetCoreDocker.DataLayer.Interface;
using AspnetCoreDocker.Model;

namespace AspnetCoreDocker.BusinessLayer
{
    public class CustomerCatalogue : ICatalogue<Customer>, ILogon
    {
        private readonly IDbTransaction<Customer> _customer;
        private readonly ILogonTransaction _customerLogon;

        public CustomerCatalogue(IDbTransaction<Customer> customer, ILogonTransaction customerLogon)
        {
            _customer = customer;
            _customerLogon = customerLogon;
        }

        public IEnumerable<Customer> GetList()
        {
            return _customer.Get();
        }

        public Customer GetDetails(int id)
        {
            return _customer.Get(id);
        }

        public int AddNewRecord(Customer model)
        {
            return _customer.Insert(model);
        }

        public bool UpdateRecord(int id, Customer model)
        {
            return _customer.Update(id, model);
        }

        public bool DeleteRecord(int id)
        {
            return _customer.Delete(id);
        }

        public bool ValidateLogin(string logonEmail, string password)
        {
            return _customerLogon.ValidateLogin(logonEmail, password);
        }

        public bool SaveCustomerLogonInformation(CustomerLogin logonInfo)
        {
            return _customerLogon.SaveLogonDetails(logonInfo);
        }
    }
}
