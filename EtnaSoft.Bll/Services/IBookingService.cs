using System;
using System.Collections.Generic;
using System.Text;

namespace EtnaSoft.Bll.Services
{
    public interface IBookingService
    {
        bool CheckIn(int id);
        bool Cancel(int id);
    }
}
