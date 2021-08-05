using System.Collections.Generic;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Dal.Infrastucture
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        bool Update(int id, TEntity entity);
        TEntity Create(TEntity entity);
        bool Delete(int id);
    }
}