using System;
using System.Collections.Generic;
using System.Text;

namespace EtnaSoft.Bll.Services
{
    public interface IBookingService
    {
        void CheckIn(int id);
        void Cancel(int id);
    }
}
