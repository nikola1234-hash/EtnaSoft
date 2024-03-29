﻿using System;
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
        private bool _isClosed = false;
        public string ConnectionString => EtnaSettings.ConnectionString;
        /// <summary>
        /// Starts Transaction
        /// ands sets connection state to open
        /// </summary>
        public void StartTransaction()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConnectionString)
            {
                InitialCatalog = EtnaSettings.DbName
            };
            
            _connection = new SqlConnection(builder.ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            _isClosed = false;
        }

        /// <summary>
        /// Saves Transaction
        /// </summary>
        /// <typeparam name="T">Bussiness Object</typeparam>
        /// <param name="storedProcedure">sp_ at the beggining of string sets Command Parameter
        /// to stored procedure, otherwise its CommandParameter.Text</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int SaveDataTransaction<T>(string storedProcedure, T parameters)
        {
            CommandType cmd = CommandType.StoredProcedure;
            if (!storedProcedure.StartsWith("sp_"))
            {
                cmd = CommandType.Text;
            }
            var rows = _connection.Execute(storedProcedure, parameters, commandType: cmd,
                transaction: _transaction);
            return rows;
        }

        public List<T> LoadDataTransaction<T, TU>(string storedProcedure, TU parameters)
        {
            CommandType cmd = CommandType.StoredProcedure;
            if (!storedProcedure.StartsWith("sp_"))
            {
                cmd = CommandType.Text;
            }
            var rows = _connection.Query<T>(storedProcedure, parameters, commandType: cmd,
                transaction: _transaction).ToList();
            return rows;
        }
        /// <summary>
        /// Commits Transaction and closes connection
        /// </summary>
        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();
            _isClosed = true;

        }

        public void RollBackTransaction()
        {
            _transaction.Rollback();
            _connection.Close();

            _isClosed = true;
        }


        public void Dispose()
        {
            if (_isClosed == false)
            {
                try
                {
                    CommitTransaction();
                }
                catch 
                {
                    //TODO: Log this issue
                }
            }

            _transaction = null;
            _connection = null;
        }
    }
}
