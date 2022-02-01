using System;
using System.Collections.Generic;
using System.Text;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Dal.Exceptions
{
    public class RoomHasRecordsException : Exception
    {
        public Room Room { get; set; }

        public RoomHasRecordsException(Room room)
        {
            Room = room;
        }

        public RoomHasRecordsException(string message, Room room) : base(message)
        {
            Room = room;
        }
    }
}
