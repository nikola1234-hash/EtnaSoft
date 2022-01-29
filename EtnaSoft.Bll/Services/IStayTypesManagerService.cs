using System.Collections.Generic;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface IStayTypesManagerService
    {
        /// <summary>
        /// If Its Actve its gonna be false, if inactive puts it back to active;
        /// </summary>
        /// <param name="id">Id of StayType</param>
        /// <returns>true if success</returns>
        bool DeactiveTypeOrActivate(int id);

        void CreateStayType(object entity);
        bool UpdateStayType(int id, object entity);
        List<StayType> GetAllStayTypes();
    }
}