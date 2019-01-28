using System;
using System.Collections.Generic;
using AspnetCoreDocker.BusinessLayer.Interface;
using AspnetCoreDocker.DataLayer.Interface;
using AspnetCoreDocker.Model;

namespace AspnetCoreDocker.BusinessLayer
{
    public class ProductCatalogue : ICatalogue<Product>
    {
        private readonly IDbTransaction<Product> _product;

        public ProductCatalogue(IDbTransaction<Product> product)
        {
            _product = product;
        }

        public IEnumerable<Product> GetList()
        {
            return _product.Get();
        }

        public Product GetDetails(int id)
        {
            return _product.Get(id);
        }

        public int AddNewRecord(Product model)
        {
            return _product.Insert(model);
        }

        public bool UpdateRecord(int id, Product model)
        {
            return _product.Update(id, model);
        }

        public bool DeleteRecord(int id)
        {
            return _product.Delete(id);
        }
    }
}
