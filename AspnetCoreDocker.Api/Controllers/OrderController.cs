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
    public class OrderController : Controller
    {
        private readonly IOrderMapper _orderMapper;
        private readonly ICatalogue<Order> _order;

        public OrderController(IOrderMapper orderMapper, ICatalogue<Order> order)
        {
            _orderMapper = orderMapper;
            _order = order;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<OrderMap> Get()
        {
            return _orderMapper.GetOrders();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public OrderMap Get(int id)
        {
            return _orderMapper.GetOrders().FirstOrDefault(x => x.OrderId == id);
        }

        // POST api/<controller>
        [HttpPost]
        public int Post([FromBody]Order value)
        {
            return _order.AddNewRecord(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]Order value)
        {
            return _order.UpdateRecord(id, value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _order.DeleteRecord(id);
        }
    }
}
