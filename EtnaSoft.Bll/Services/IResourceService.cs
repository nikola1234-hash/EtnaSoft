using System.Collections.Generic;
using System.Collections.ObjectModel;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface IResourceService
    {
        ObservableCollection<Room> CreateResource();
    }
}