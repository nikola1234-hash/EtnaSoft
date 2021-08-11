using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Services.Reception
{
    public interface IDetailsManager
    {
        bool CreateUpdateModel(AppointmentViewModel model);
    }
}