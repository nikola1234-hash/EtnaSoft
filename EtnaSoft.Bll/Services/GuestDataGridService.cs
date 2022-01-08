using System.Collections.Generic;
using System.Linq;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public class GuestDataGridService : IGuestDataGridService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GuestDataGridService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DataGridGuest> GetAllGuests()
        {
            return _unitOfWork.DataGridGuests.GetAll();
        }

        public bool UpdateGuestData(int id, DataGridGuest guest)
        {
            return _unitOfWork.DataGridGuests.Update(id, guest);
        }
    }
}
