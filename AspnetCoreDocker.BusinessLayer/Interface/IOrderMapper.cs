using System;
using System.Collections.Generic;
using System.Text;
using AspnetCoreDocker.Model;

namespace AspnetCoreDocker.BusinessLayer.Interface
{
    public interface IOrderMapper
    {
        List<OrderMap> GetOrders();

    }
}
