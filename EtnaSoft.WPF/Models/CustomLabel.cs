using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using DevExpress.Mvvm.POCO;

namespace EtnaSoft.WPF.Models
{
    public class CustomLabel
    {
        public static ObservableCollection<CustomLabel> Create(IEnumerable<ErtnaSoft.Bo.Entities.CustomLabel> label)
        {
            var labels = new ObservableCollection<CustomLabel>();


            System.Windows.Media.Color color;
            foreach (var i in label)
            {
                color = Color.FromRgb(30,144,255);
                if (i.Id == 1)
                {
                    color = Color.FromRgb(50, 205, 50);
                }
                var l = new CustomLabel()
                {
                    Caption = i.Caption,
                    Color = color,
                    Id = i.Id
                };
               labels.Add(l);
            }
            return labels;
        }

        protected CustomLabel(){ }

        public virtual int Id { get; set; }
        public virtual string Caption { get; set; }
        public virtual  Color Color { get; set; }


    }
}
