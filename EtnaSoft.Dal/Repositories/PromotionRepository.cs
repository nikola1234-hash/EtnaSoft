using System;
using System.Collections.Generic;
using System.Linq;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Repositories
{
    public class PromotionRepository : IRepository<Promotion>
    {

        private const string GetAllPromotions = @"SELECT * from dbo.Promocije";

        private const string GetPromotioById = @"SELECT * from dbo.Promocije 
                                                  WHERE Id = @id";

        private const string UpdatePromotion = @"UPDATE dbo.Promocije SET StayTypeId = @stayTypeId,
                                                  StayDays = @stayDays, ChildPrice = @childPrice,
                                                  Price = @price WHERE Id = @id";

        private const string CreatePromotion = @"INSERT INTO dbo.Promocije (StayTypeId, StayDays, ChildPrice,
                                                  Price) VALUES (@stayTypeId, @stayDays, @childPrice, @price)
                                                   SELECT * from dbo.Promocije where Id = @@IDENTITY";

        private const string DeactivatePromotion = @"UPDATE dbo.Promocije SET IsActive = @isActive";
        private readonly IGenericDbContext _context;

        public PromotionRepository(IGenericDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Promotion> GetAll()
        {
            var output = _context.
                LoadData<Promotion, dynamic>(GetAllPromotions,
                                            new { });
            return output;
        }

        public Promotion GetById(int id)
        {
            var output = _context.LoadData<Promotion, dynamic>(GetPromotioById, new { id }).FirstOrDefault();
            return output;
        }

        public bool Update(int id, Promotion entity)
        {
            bool success;
            int output = 0;
            var newProm = new
            {
                Id = id,
                StayTypeId = entity.StayTypeId,
                StayDays = entity.StayDays,
                ChildPrice = entity.ChildPrice,
                Price = entity.Price
            };
            try
            {
                output = _context.SaveData(UpdatePromotion, newProm);
                
            }
            catch
            {
                throw;
            }
            finally
            {
                success = output > 1;
            }

            return success;
        }

        public Promotion Create(Promotion entity)
        {
            Promotion promotion;
            var newProm = new
            {
                StayTypeId = entity.StayTypeId,
                StayDays = entity.StayDays,
                ChildPrice = entity.ChildPrice,
                Price = entity.Price
            };
            try
            {
                promotion = _context.LoadData<Promotion, dynamic>(CreatePromotion, newProm).FirstOrDefault();
            }
            catch
            {
                throw;
            }

            return promotion;
        }
        /// <summary>
        /// This Activates or deactivates Promotion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            bool success;
            bool isActive = false;
            var item = GetById(id);
            if (!item.IsActive)
            {
                isActive = true;
            }
            var output = _context.SaveData(DeactivatePromotion, new { isActive = isActive });
            success = output > 1;
            return success;
        }
    }
}
