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
    public class ProductController : Controller
    {
        private readonly ICatalogue<Product> _product;

        public ProductController(ICatalogue<Product> product)
        {
            _product = product;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _product.GetList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _product.GetDetails(id);
        }

        // POST api/<controller>
        [HttpPost]
        public int Post([FromBody]Product value)
        {
            return _product.AddNewRecord(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]Product value)
        {
            return _product.UpdateRecord(id, value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _product.DeleteRecord(id);
        }
    }
}
