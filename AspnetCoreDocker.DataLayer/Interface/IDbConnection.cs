﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AspnetCoreDocker.DataLayer.Interface
{
    public interface IDbConnection
    {
        SqlConnectionStringBuilder SqlConnectionBuilder();
    }
}
