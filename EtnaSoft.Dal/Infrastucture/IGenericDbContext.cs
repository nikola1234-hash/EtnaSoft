using System.Collections.Generic;

namespace EtnaSoft.Dal.Infrastucture
{
    public interface IGenericDbContext
    {
        IEnumerable<TEntity> LoadData<TEntity, TParameters>(string sql, TParameters parameters);
        int SaveData<TParameters>(string sql, TParameters parameters);
    }
}