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
            ObservableCollection<CustomLabel> labels = new ObservableCollection<CustomLabel>();

            foreach (var l in label)
            {
                
                var c = new CustomLabel
                {
                    Id = l.Id,
                    Caption = l.Caption,
                    Color = (Color)ColorConverter.ConvertFromString(l.Color)
                };
                labels.Add(c);
            }
        
            
            return labels;
        }

        protected CustomLabel(){ }

        public virtual int Id { get; set; }
        public virtual string Caption { get; set; }
        public virtual  Color Color { get; set; }


    }
}
