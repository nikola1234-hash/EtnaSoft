using System.Windows.Media;
using DevExpress.Data;
using DevExpress.Mvvm.POCO;

namespace EtnaSoft.WPF.Models
{
    public class PaymentState
    {
        public PaymentState Create()
        {
            return ViewModelSource.Create(() => new PaymentState());
        }

        protected PaymentState()
        {

        }

        public virtual int Id { get; set; }
        public virtual string Caption { get; set; }
        public virtual Brush Brush { get; set; }
    }
}
