using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using AspnetCoreDocker.DataLayer.Interface;
using AspnetCoreDocker.Model;

namespace AspnetCoreDocker.DataLayer.Transaction
{
    public class LogonTransaction : ILogonTransaction
    {
        private readonly IDbConnection _dbConnection;

        public LogonTransaction(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public bool SaveLogonDetails(CustomerLogin logonDetails)
        {
            try
            {
                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();
                    var strBuilder = new StringBuilder();
                    strBuilder.Append(
                        $"INSERT INTO CustomerLogin(CustomerId, LogonEmail, LogonPassword) VALUES('{logonDetails.CustomerId}','{logonDetails.LogonEmail}', '{logonDetails.LogonPassword}')");
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

        public bool ValidateLogin(string logonEmail, string password)
        {
            try
            {
                var result = string.Empty;

                using (var sqlConn = new SqlConnection(_dbConnection.SqlConnectionBuilder().ConnectionString))
                {
                    sqlConn.Open();

                    var strBuilder = new StringBuilder();
                    strBuilder.Append($"SELECT CustomerId FROM CustomerLogin WHERE LogonEmail = '{logonEmail}' AND LogonPassword = '{password}'");

                    using (var command = new SqlCommand(strBuilder.ToString(), sqlConn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = Convert.ToString(reader.GetInt16(0));
                            }
                        }
                    }
                }

                return !string.IsNullOrEmpty(result);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
