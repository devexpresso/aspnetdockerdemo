using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using AspnetCoreDocker.DataLayer.Interface;
using AspnetCoreDocker.Model;
using IDbConnection = AspnetCoreDocker.DataLayer.Interface.IDbConnection;

namespace AspnetCoreDocker.DataLayer.Transaction
{
    public class CustomerTransaction : IDbTransaction<Customer>
    {
        private readonly IDbConnection _dbConnection;
        
        public CustomerTransaction(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Customer> Get()
        {
            try
            {
                var customers = new List<Customer>();
                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();

                    var strBuilder = new StringBuilder();
                    strBuilder.Append("SELECT CustomerId, FirstName, LastName, Email, Address, PhoneNumber FROM Customer");

                    using (var command = new SqlCommand(strBuilder.ToString(), sqlConn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customers.Add(new Customer()
                                {
                                    CustomerId = reader.GetInt16(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Email = reader.GetString(3),
                                    Address = reader.GetString(4),
                                    PhoneNumber = reader.GetString(5)
                                });
                            }
                        }
                    }
                }

                return customers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Customer Get(int id)
        {
            try
            {
                var customer = new Customer();

                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();

                    var strBuilder = new StringBuilder();
                    strBuilder.Append($"SELECT CustomerId, FirstName, LastName, Email, Address, PhoneNumber FROM Customer WHERE CustomerId = '{id}'");

                    using (var command = new SqlCommand(strBuilder.ToString(), sqlConn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customer.CustomerId = reader.GetInt16(0);
                                customer.FirstName = reader.GetString(1);
                                customer.LastName = reader.GetString(2);
                                customer.Email = reader.GetString(3);
                                customer.Address = reader.GetString(4);
                                customer.PhoneNumber = reader.GetString(5);
                            }
                        }
                    }
                }

                return customer;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Insert(Customer model)
        {
            try
            {
                var id = 0;
                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();
                    var strBuilder = new StringBuilder();
                    strBuilder.Append(
                        $"INSERT INTO Customer(FirstName, LastName, Email, Address, PhoneNumber) VALUES('{model.FirstName}','{model.LastName}', '{model.Email}', '{model.Address}', '{model.PhoneNumber}'); SELECT SCOPE_IDENTITY();");
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

        public bool Update(int id, Customer model)
        {
            try
            {
                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();

                    var strBuilder = new StringBuilder();
                    strBuilder.Append(
                        $"UPDATE Customer SET FirstName = '{model.FirstName}', LastName = '{model.LastName}', Email = '{model.Email}', Address = '{model.Address}', PhoneNumber = '{model.PhoneNumber}' WHERE CustomerId = '{id}'");
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
                        $"DELETE FROM Customer WHERE CustomerId = '{id}'");
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
