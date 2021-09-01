using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace EtnaSoft.Dal.Services
{
    public class DatabaseService
    {
        public const string Sql = @"select count(*) from master.dbo.sysdatabases where name=@database";

        public bool DoesDatabaseExist(string dbName)
        {
            bool result = false;
            using (IDbConnection conn = new SqlConnection(EtnaSettings.ConnectionString))
            {
                
                var i = conn.ExecuteScalar(Sql, new { database = dbName}, commandType: CommandType.Text);
                if ((int) i != 0)
                {
                    result = true;
                }
            }

            return result;
        }

    }
}
