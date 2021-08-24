using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public class GuestSearchService : IGuestSearchService
    {
        private readonly IUnitOfWork _unit;

        public GuestSearchService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public IEnumerable<Guest> GetGuests(string keyword)
        {
            IEnumerable<Guest> result;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                result = _unit.Guests.GetAll();
                return result;
            }
            result =_unit.Guests.GetAll().Where(s => s.FirstName == keyword || s.LastName == keyword).OrderBy(s => s.FirstName);
            return result;
        }
    }
}
