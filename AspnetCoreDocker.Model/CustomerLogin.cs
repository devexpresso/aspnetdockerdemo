using System;
using System.Collections.Generic;
using System.Text;

namespace AspnetCoreDocker.Model
{
    public class CustomerLogin
    {
        public int CustomerId { get; set; }
        public string LogonEmail { get; set; }
        public string LogonPassword { get; set; }
    }
}
