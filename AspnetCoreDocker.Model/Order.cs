using System;

namespace AspnetCoreDocker.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int ItemCount { get; set; }

    }
}
