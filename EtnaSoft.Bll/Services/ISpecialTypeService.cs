using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Dto;

namespace EtnaSoft.Bll.Services
{
    public interface ISpecialTypeService
    {
        void Register(StayTypesDto dto);
        decimal GetPricePerChild(int stayTypeId);
        Promotion GetPromotionById(int id);
        bool UpdatePromotion(Promotion promotion);
        Promotion GetPromotionByStayTypeId(int stayTypeId);
    }
}