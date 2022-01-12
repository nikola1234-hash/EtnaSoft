using System.Collections.Generic;
using System.Collections.ObjectModel;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services.Facade
{
    public interface IComboboxFacade
    {
        ObservableCollection<Room> FillRoomCombobox(int id, out Room selectedRoom, out int selectedIndex);
        ObservableCollection<StayType> FillStayTypeCombobox();
    }
}