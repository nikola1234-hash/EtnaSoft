using System;
using System.Collections.Generic;
using System.Text;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface IGuestSearchService
    {
        IEnumerable<Guest> GetGuests(string keyword="");
    }
}
