using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class SubRoomViewModel :EtnaBaseViewModel
    {
        public Room Room { get; set; }
        public string RoomNumber { get; set; }

        public string IsActive
        {
            get { return @"C:\InDevelopment\EtnaSoft\EtnaSoft.WPF\Icons\hotelbed.svg"; }
        }

        public SubRoomViewModel(Room room)
        {
            Room = room;
            RoomNumber = room.RoomNumber;
        }
    }
}
