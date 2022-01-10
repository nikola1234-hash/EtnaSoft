
using System.Collections.Generic;
using System.Linq;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public class GuestHistoryService : IGuestHistoryService
    {
        private readonly IGenericDbContext _dbContext;

        public GuestHistoryService(IGenericDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<GuestBookingHistory> GetGuestBookingHistory(int id)
        {
            var result = _dbContext.LoadData<GuestBookingHistory, dynamic>("sp_GuestHistory", new { guestId = id });
            return result;

        }
    }
}
