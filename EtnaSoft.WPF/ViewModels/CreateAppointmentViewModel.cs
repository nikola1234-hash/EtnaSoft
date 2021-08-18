using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Scheduling;

namespace EtnaSoft.WPF.ViewModels
{
    public class CreateAppointmentViewModel : AppointmentWindowViewModel
    {
        public CreateAppointmentViewModel(AppointmentItem appointmentItem, SchedulerControl scheduler) : base(appointmentItem, scheduler)
        {
        }
    }
}
