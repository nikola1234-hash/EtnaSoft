using System;
using System.Collections.Generic;

namespace EtnaSoft.Dal.Infrastucture
{
    public interface IDbTransactions : IDisposable
    {
        void StartTransaction();
        void SaveDataTransaction<T>(string storedProcedure, T parameters);
        List<T> LoadDataTransaction<T, U>(string storedProcedure, U parameters);
        void CommitTransaction();
        void RollBackTransaction();
        void Dispose();
    }
}