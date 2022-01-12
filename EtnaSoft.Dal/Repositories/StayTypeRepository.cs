using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Repositories
{
    public class StayTypeRepository : IRepository<StayType>
    {
        private const string GetTypes = "SELECT * from dbo.StayTypes";
        private const string GetTypesById = "SELECT * from dbo.StayTypes WHERE Id = @Id";

        private readonly IGenericDbContext _context;
        //TODO: FINISH THIS REPOSITORY
        public StayTypeRepository(IGenericDbContext context)
        {
            _context = context;
        }

        public IEnumerable<StayType> GetAll()
        {
            var output = _context.LoadData<StayType, dynamic>(GetTypes, new { });
            return output;
        }

        public StayType GetById(int id)
        {
            var output = _context.LoadData<StayType, dynamic>(GetTypesById, new {Id = id}).FirstOrDefault();
            return output;
            
        }

        public bool Update(int id, StayType entity)
        {
            throw new NotImplementedException();
        }

        public StayType Create(StayType entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
