using System.Linq;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Dto;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public sealed class SpecialTypeService : ISpecialTypeService
    {
        private readonly IUnitOfWork _unit;

        public SpecialTypeService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public void Register(StayTypesDto dto)
        {
            StayType stayType = new StayType()
            {
                Title = dto.Title,
                Price = dto.Price,
                IsSpecialType = dto.IsSpecialType
            };

            var newStayType = _unit.StayTypes.Create(stayType);
            if (newStayType.IsSpecialType)
            {
                Promotion prom = new Promotion()
                {
                    StayTypeId = newStayType.Id,
                    ChildPrice = dto.ChildPrice,
                    Price = dto.Price,
                    StayDays = dto.StayDays
                };
                _unit.Promotions.Create(prom);
            }
        }

        public decimal GetPricePerChild(int stayTypeId)
        {
            var output = _unit.Promotions.GetAll().FirstOrDefault(s => s.StayTypeId == stayTypeId);
            return output.ChildPrice;
        }

        public Promotion GetPromotionById(int id)
        {
            return _unit.Promotions.GetById(id);
        }

        public bool UpdatePromotion(Promotion promotion)
        {
            return _unit.Promotions.Update(promotion.Id, promotion);
        }

        public Promotion GetPromotionByStayTypeId(int stayTypeId)
        {
            var promotion = _unit.Promotions.GetAll().FirstOrDefault(s => s.StayTypeId == stayTypeId);
            return promotion;
        }
    }

   
}
