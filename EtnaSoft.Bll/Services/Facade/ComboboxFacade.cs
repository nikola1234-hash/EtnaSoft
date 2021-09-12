using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services.Facade
{
    public class ComboboxFacade : IComboboxFacade
    {
        private readonly ISchedulerService _scheduler;
        private readonly IResourceService _resource;
        
        

        public ComboboxFacade(ISchedulerService scheduler, IResourceService resource)
        {
            _scheduler = scheduler;
            _resource = resource;
        }

        public ObservableCollection<Room> FillRoomCombobox(int id, out Room selectedRoom, out int selectedIndex)
        {
            var rooms = _resource.CreateResource();
            List<Room>roomList = rooms.ToList();
            selectedRoom = rooms.FirstOrDefault(s => s.Id == id);
            selectedIndex = roomList.IndexOf(selectedRoom);
            return rooms;
        }

        public ObservableCollection<StayType> FillStayTypeCombobox()
        {
            var stayTypesEnum = _scheduler.LoadStayTypes();
            var stayTypes = new ObservableCollection<StayType>(stayTypesEnum);
            return stayTypes;
        }
    }
}
