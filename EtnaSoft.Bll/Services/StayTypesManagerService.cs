using System;
using System.Collections.Generic;
using System.Linq;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public sealed class StayTypesManagerService : IStayTypesManagerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StayTypesManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// If Its Actve its gonna be false, if inactive puts it back to active;
        /// </summary>
        /// <param name="id">Id of StayType</param>
        /// <returns>true if success</returns>
        public bool DeactiveTypeOrActivate(int id)
        {
            var output = _unitOfWork.StayTypes.Delete(id);
            return output;
        }

        public void CreateStayType(object entity)
        {
            
            if (entity is StayType stayType)
            {
                try
                {
                    _unitOfWork.StayTypes.Create(stayType);
                }
                catch
                {
                    throw;
                }
                
            }
        }

        public bool UpdateStayType(int id, object entity)
        {
            bool output = false;
            if (entity is StayType stayType)
            {
                try
                {
                    output = _unitOfWork.StayTypes.Update(id, stayType);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                

            }
            return output;
        }

        public StayType GetStayTypeById(int id)
        {
            return _unitOfWork.StayTypes.GetById(id);
        }

        public List<StayType> GetAllStayTypes()
        {
            return _unitOfWork.StayTypes.GetAll().ToList();
        }
    }
}
