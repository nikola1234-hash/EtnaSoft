using System;
using DevExpress.Mvvm;
using DevExpress.Xpf.Scheduling;

namespace EtnaSoft.WPF.ViewModels
{
    public class AppointmentViewModel : AppointmentWindowViewModel
    {
        private AppointmentItem _appointmentItem;
        public AppointmentViewModel(AppointmentItem appointmentItem, SchedulerControl scheduler = null) : base(appointmentItem, scheduler)
        {
            _appointmentItem = appointmentItem;
            RemoveAppointmentCommand = new DelegateCommand(ExecuteRemove);
        }

        private void ExecuteRemove()
        {
            
            throw new NotImplementedException();
        }
    }
}


