using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public sealed class UpdateGuestService : IUpdateGuestService
    {
        private readonly IUnitOfWork _unit;

        public UpdateGuestService(IUnitOfWork unit)
        {
            _unit = unit;
        }


        public bool UpdateGuestData(Guest guest)
        {
            return _unit.Guests.Update(guest.Id, guest);
        }
    }
}
