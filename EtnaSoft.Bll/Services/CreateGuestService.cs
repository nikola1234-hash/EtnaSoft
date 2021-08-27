using System;
using System.Collections.Generic;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public class CreateGuestService : ICreateGuestService
    {
        private readonly IUnitOfWork _unit;

        public CreateGuestService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public Guest CreateGuest(Guest guest)
        {
            var result = _unit.Guests.Create(guest);
            return result;
        }
    }
}
