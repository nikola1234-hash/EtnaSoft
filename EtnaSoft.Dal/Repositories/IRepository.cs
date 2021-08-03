using System.Collections.Generic;

namespace EtnaSoft.Dal.Repositories
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        bool Update(int id, TEntity entity);
        TEntity Create(TEntity entity);
        bool Delete(int id);
    }
}