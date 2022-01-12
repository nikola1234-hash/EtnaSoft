using System.Collections.Generic;

namespace EtnaSoft.Dal.Infrastucture
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns>Boolean</returns>
        bool Update(int id, TEntity entity);
        TEntity Create(TEntity entity);
        bool Delete(int id);
    }
}