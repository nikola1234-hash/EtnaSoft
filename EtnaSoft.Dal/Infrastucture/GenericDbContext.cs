using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace EtnaSoft.Dal.Infrastucture
{
    public sealed class GenericDbContext : IGenericDbContext, IDisposable
    {
        //TODO: DEBUG CMD PROPERTY 
        // does it start with CommandType.Text
        // and does it change to StoredProcedure if statements starts with sp_
       
        public string ConnectionString => EtnaSettings.ConnectionString;
        public IEnumerable<TEntity> LoadData<TEntity, TParameters>(string sql, TParameters parameters)
        {
            
            
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
            {
                CommandType cmd = CommandType.Text;
                // sql db name starts with sp_ its stored procedure
                if (sql.StartsWith("sp_"))
                {
                    cmd = CommandType.StoredProcedure;
                }

                try
                {
                    IEnumerable<TEntity> output = conn.Query<TEntity>(sql, parameters, commandType: cmd);
                    return output;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
              
            }
        }

        public int SaveData<TParameters>(string sql, TParameters parameters)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(ConnectionString))
            {
                CommandType cmd = CommandType.Text;
                if (sql.StartsWith("sp_"))
                {
                    cmd = CommandType.StoredProcedure;
                }

                var output = conn.Execute(sql, parameters, commandType: cmd);

                return output;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
