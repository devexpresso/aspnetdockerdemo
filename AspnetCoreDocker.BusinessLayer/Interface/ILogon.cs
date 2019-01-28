using System;
using System.Collections.Generic;
using System.Text;
using AspnetCoreDocker.Model;

namespace AspnetCoreDocker.BusinessLayer.Interface
{
    public interface ILogon
    {
        bool ValidateLogin(string logonEmail, string password);
        bool SaveCustomerLogonInformation(CustomerLogin logonInfo);
    }
}
