using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Scheduling;

namespace EtnaSoft.WPF.ViewModels
{
    public class AppointmentWindowBase : AppointmentWindowViewModel, IDisposable
    {
        public virtual void Dispose()
        {
        }

        public AppointmentWindowBase(AppointmentItem appointmentItem, SchedulerControl scheduler) : base(appointmentItem, scheduler)
        {
        }
    }
}
