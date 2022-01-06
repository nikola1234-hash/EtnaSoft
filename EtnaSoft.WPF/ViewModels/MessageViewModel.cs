namespace EtnaSoft.WPF.ViewModels
{
    public class MessageViewModel : EtnaBaseViewModel
    {

        public bool HasMessage => !string.IsNullOrWhiteSpace(Message);

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertiesChanged(nameof(Message));
                RaisePropertiesChanged(nameof(HasMessage));
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
