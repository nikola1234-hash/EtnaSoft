using System;
using System.Collections.Generic;

namespace EtnaSoft.Dal.Infrastucture
{
    public interface IDbTransactions : IDisposable
    {
        void StartTransaction();
        int SaveDataTransaction<T>(string storedProcedure, T parameters);
        List<T> LoadDataTransaction<T, TU>(string storedProcedure, TU parameters);
        void CommitTransaction();
        void RollBackTransaction();
        void Dispose();
    }
}