﻿using System.Collections.Generic;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface IRoomsManagerService
    {
        List<Room> GetAllRooms();
    }
}