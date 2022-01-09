using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface IUpdateGuestService
    {
        bool UpdateGuestData(int id, Guest guest);
    }
}