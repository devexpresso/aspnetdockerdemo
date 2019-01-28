using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using AspnetCoreDocker.DataLayer.Interface;
using AspnetCoreDocker.Model;

namespace AspnetCoreDocker.DataLayer.Transaction
{
    public class OrderTransaction: IDbTransaction<Order>
    {
        private readonly IDbConnection _dbConnection;
        public OrderTransaction(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Order> Get()
        {
            try
            {
                var orders = new List<Order>();
                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();

                    var strBuilder = new StringBuilder();
                    strBuilder.Append("SELECT OrderId, CustomerId, ProductId, ItemCount FROM Order");

                    using (var command = new SqlCommand(strBuilder.ToString(), sqlConn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                orders.Add(new Order()
                                {
                                    OrderId = reader.GetInt16(0),
                                    CustomerId = reader.GetInt16(1),
                                    ProductId = reader.GetInt16(2),
                                    ItemCount = reader.GetInt16(3)
                                });
                            }
                        }
                    }
                }

                return orders;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Order Get(int id)
        {
            try
            {
                var order = new Order();

                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();

                    var strBuilder = new StringBuilder();
                    strBuilder.Append($"SELECT OrderId, CustomerId, ProductId, ItemCount FROM Order WHERE OrderId = '{id}'");

                    using (var command = new SqlCommand(strBuilder.ToString(), sqlConn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                order.OrderId = reader.GetInt16(0);
                                order.CustomerId = reader.GetInt16(1);
                                order.ProductId = reader.GetInt16(2);
                                order.ItemCount = reader.GetInt16(3);
                            }
                        }
                    }
                }

                return order;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Insert(Order model)
        {
            try
            {
                var id = 0;
                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();
                    var strBuilder = new StringBuilder();
                    strBuilder.Append(
                        $"INSERT INTO Order(CustomerId, ProductId, ItemCount) VALUES({model.CustomerId},{model.ProductId}, {model.ItemCount}); SELECT SCOPE_IDENTITY();");
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

        public bool Update(int id, Order model)
        {
            try
            {
                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();

                    var strBuilder = new StringBuilder();
                    strBuilder.Append(
                        $"UPDATE Order SET CustomerId = {model.CustomerId}, ProductId = {model.ProductId}, ItemCount = {model.ItemCount} WHERE OrderId = '{id}'");
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
                        $"DELETE FROM Order WHERE OrderId = '{id}'");
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
