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

        private const string UpdateTypes =
            "UPDATE dbo.StayTypes SET Title = @title, Price = @price, IsActive = @isActive, IsSpecialType = @isSpecialType Where Id = @id";

        private const string CreateType =
            @"INSERT INTO dbo.StayTypes (Title, Price, IsActive, IsSpecialType) Values (@Title, @Price, 1, @isSpecialType);
               SELECT * FROM dbo.StayTypes where Id = @@IDENTITY";

        private const string SetInactive = @"UPDATE dbo.StayTypes SET IsActive = @isActive WHERE Id = @Id";

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
            var success = false;
            var output = _context.SaveData(UpdateTypes,
                new { title = entity.Title, price = entity.Price, isActive = entity.IsActive, isSpecialType = entity.IsSpecialType, id });
            success = output > 0;
            return success;
        }

        public StayType Create(StayType entity)
        {
            var e = new
            {
                Title = entity.Title,
                Price = entity.Price,
                IsSpecialType = entity.IsSpecialType
            };

            var output = _context.LoadData<StayType, dynamic>(CreateType, e).FirstOrDefault();
            return output;
        }

        public bool Delete(int id)
        {
            bool success = false;
            var stayType = GetById(id);
            bool isActive = !stayType.IsActive;
            var output = _context.SaveData(SetInactive, new { isActive, id });
            success = output > 0;
            return success;
        }
    }
}
