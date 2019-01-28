using System;
using System.Collections.Generic;
using System.Text;
using AspnetCoreDocker.Model;

namespace AspnetCoreDocker.DataLayer.Interface
{
    public interface ILogonTransaction
    {
        bool SaveLogonDetails(CustomerLogin logonDetails);
        bool ValidateLogin(string logonEmail, string password);
    }
}
