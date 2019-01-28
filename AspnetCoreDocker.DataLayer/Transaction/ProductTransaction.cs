using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using AspnetCoreDocker.DataLayer.Interface;
using AspnetCoreDocker.Model;

namespace AspnetCoreDocker.DataLayer.Transaction
{
    public class ProductTransaction : IDbTransaction<Product>
    {
        private readonly IDbConnection _dbConnection;
        public ProductTransaction(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public IEnumerable<Product> Get()
        {
            try
            {
                var products = new List<Product>();
                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();

                    var strBuilder = new StringBuilder();
                    strBuilder.Append("SELECT ProductId, ProductName, ProductDescription, ProductImage FROM Product");

                    using (var command = new SqlCommand(strBuilder.ToString(), sqlConn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new Product()
                                {
                                    ProductId = reader.GetInt16(0),
                                    ProductName = reader.GetString(1),
                                    ProductDescription = reader.GetString(2),
                                    ProductImageLink = reader.GetString(3)
                                });
                            }
                        }
                    }
                }

                return products;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Product Get(int id)
        {
            try
            {
                var product = new Product();

                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();

                    var strBuilder = new StringBuilder();
                    strBuilder.Append($"SELECT ProductId, ProductName, ProductDescription, ProductImage FROM Product WHERE ProductId = '{id}'");

                    using (var command = new SqlCommand(strBuilder.ToString(), sqlConn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                product.ProductId = reader.GetInt16(0);
                                product.ProductName = reader.GetString(1);
                                product.ProductDescription = reader.GetString(2);
                                product.ProductImageLink = reader.GetString(3);
                            }
                        }
                    }
                }

                return product;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Insert(Product model)
        {
            try
            {
                var id = 0;
                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();
                    var strBuilder = new StringBuilder();
                    strBuilder.Append(
                        $"INSERT INTO Product(ProductName, ProductDescription, ProductImage) VALUES('{model.ProductName}','{model.ProductDescription}', '{model.ProductImageLink}'); SELECT SCOPE_IDENTITY();");
                    using (var command = new SqlCommand(strBuilder.ToString(), sqlConn))
                    {
                        id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool Update(int id, Product model)
        {
            try
            {
                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();

                    var strBuilder = new StringBuilder();
                    strBuilder.Append(
                        $"UPDATE Product SET ProductName = '{model.ProductName}', ProductDescription = '{model.ProductDescription}', ProductImage = '{model.ProductImageLink}' WHERE ProductId = '{id}'");
                    using (var command = new SqlCommand(strBuilder.ToString(), sqlConn))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();

                    var strBuilder = new StringBuilder();
                    strBuilder.Append(
                        $"DELETE FROM Product WHERE ProductId = '{id}'");
                    using (var command = new SqlCommand(strBuilder.ToString(), sqlConn))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
