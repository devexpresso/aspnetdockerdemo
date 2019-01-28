using System;
using System.Collections.Generic;
using System.Text;

namespace AspnetCoreDocker.Model
{
    public class OrderMap
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageLink { get; set; }
        public string DeliveryAddress { get; set; }
        
    }
}
