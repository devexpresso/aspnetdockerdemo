using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspnetCoreDocker.BusinessLayer.Interface;
using AspnetCoreDocker.DataLayer.Interface;
using AspnetCoreDocker.Model;

namespace AspnetCoreDocker.BusinessLayer
{
    public class OrderCatalogue : ICatalogue<Order>, IOrderMapper
    {
        private readonly IDbTransaction<Order> _order;
        private readonly ICatalogue<Product> _product;
        private readonly ICatalogue<Customer> _customer;

        public OrderCatalogue(IDbTransaction<Order> order, ICatalogue<Product> product, ICatalogue<Customer> customer)
        {
            _order = order;
            _product = product;
            _customer = customer;
        }


        public IEnumerable<Order> GetList()
        {
            return _order.Get();
        }

        public Order GetDetails(int id)
        {
            return _order.Get(id);
        }

        public int AddNewRecord(Order model)
        {
            return _order.Insert(model);
        }

        public bool UpdateRecord(int id, Order model)
        {
            return _order.Update(id, model);
        }

        public bool DeleteRecord(int id)
        {
            return _order.Delete(id);
        }

        public List<OrderMap> GetOrders()
        {
            var records = new List<OrderMap>();

            var orders = GetList();
            orders.ToList().ForEach(order =>
            {
                var product = _product.GetDetails(order.ProductId);
                var customer = _customer.GetDetails(order.CustomerId);

                records.Add(new OrderMap()
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = $"{customer.FirstName} {customer.LastName}",
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductImageLink = product.ProductImageLink,
                    DeliveryAddress = customer.Address,
                    OrderId = order.OrderId
                });
            });

            return records;
        }
    }
}
