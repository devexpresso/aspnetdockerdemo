using System;
using System.Data.SqlClient;
using AspnetCoreDocker.DataLayer.Interface;

namespace AspnetCoreDocker.DataLayer
{
    public class DbConnection : IDbConnection
    {
        public SqlConnectionStringBuilder SqlConnectionBuilder()
        {
            return new SqlConnectionStringBuilder()
            {
                DataSource = "<your_server.database.windows.net>",
                UserID = "<your_username>",
                Password = "<your_password>",
                InitialCatalog = "<your_database>"
            };
        }
    }
}
