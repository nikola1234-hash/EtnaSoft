using EtnaSoft.Bll.Dto;

namespace EtnaSoft.Bll.Services
{
    public interface ISpecialTypeService
    {
        void Register(StayTypesDto dto);
        decimal GetPricePerChild(int stayTypeId);
    }
}