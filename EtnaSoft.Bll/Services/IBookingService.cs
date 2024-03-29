﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EtnaSoft.Bll.Services
{
    public interface IBookingService
    {
        bool UndoCheckIn(int id);
        bool CheckInStatus(int id);
        bool CheckIn(int id);
        bool Cancel(int id);
    }
}
