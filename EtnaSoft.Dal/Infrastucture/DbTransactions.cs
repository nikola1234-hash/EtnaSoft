using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace EtnaSoft.Dal.Infrastucture
{
    public class DbTransactions : IDbTransactions, IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public string ConnectionString => EtnaSettings.ConnectionString;
        public void StartTransaction()
        {
            _connection = new SqlConnection(ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }


        public void SaveDataTransaction<T>(string storedProcedure, T parameters)
        {
            CommandType cmd = CommandType.StoredProcedure;
            if (!storedProcedure.StartsWith("sp_"))
            {
                cmd = CommandType.Text;
            }
            _connection.Execute(storedProcedure, parameters, commandType: cmd,
                transaction: _transaction);
        }

        public List<T> LoadDataTransaction<T, U>(string storedProcedure, U parameters)
        {
            CommandType cmd = CommandType.StoredProcedure;
            if (!storedProcedure.StartsWith("sp_"))
            {
                cmd = CommandType.Text;
            }
            List<T> rows = _connection.Query<T>(storedProcedure, parameters, commandType: cmd,
                transaction: _transaction).ToList();
            return rows;
        }
        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();

        }

        public void RollBackTransaction()
        {
            _transaction.Rollback();
            _connection.Close();
        }


        public void Dispose()
        {
            CommitTransaction();
        }
    }
}
