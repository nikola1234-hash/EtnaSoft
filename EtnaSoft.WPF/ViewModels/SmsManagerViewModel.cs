using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using EtnaSoft.Bll.BulkSms.Helpers;
using EtnaSoft.Bll.BulkSms.Services;
using EtnaSoft.Bll.Dto;
using EtnaSoft.WPF.Events;
using EtnaSoft.WPF.Services.Configuration;
using EtnaSoft.WPF.Services.SmsService;
using EtnaSoft.WPF.ViewModels.SmsServiceViewModels;
using EtnaSoft.WPF.Window;
using Prism.Events;


namespace EtnaSoft.WPF.ViewModels
{
    public sealed class SmsManagerViewModel : EtnaBaseViewModel
    {
        public string MessageTypeImagePath
        {
            get 
            {
                if (LookForSentMessages)
                {
                    return ".\\Icons\\nextcomment.png";
                }
                else
                {
                    return ".\\Icons\\previouscomment.png";
                }

            }
        }
        public string MessageTypeContent
        {
            get
            {
                if (LookForSentMessages)
                {
                    return "Primljene poruke";
                }
                else
                {
                    return "Poslate poruke";
                }
            }
        }
        private ICurrentWindowService CurrentWindow => GetService<ICurrentWindowService>();
        private IDialogService DialogService => GetService<IDialogService>();
        public ICommand LoadCommand { get; }
        public ICommand CloseWindowCommand { get; }
        public ICommand TypeOfMessagesCommand { get; }
        public ICommand OpenProfileCommand { get; }
        private readonly IEventAggregator _eventAggregator;
        private IMessageDetailsService _messageDetailsService;
        private readonly SmsDialogViewModel _smsDialogViewModel;
        private ObservableCollection<MessageDetailsDto> _messageDetailsCollection;
        private bool _lookForSentMessages = true;

        public bool LookForSentMessages
        {
            get { return _lookForSentMessages; }
            set
            {
                _lookForSentMessages = value; 
                RaisePropertyChanged(nameof(LookForSentMessages));
            }
        }
        public ObservableCollection<MessageDetailsDto> MessageDetailsCollection
        {
            get { return _messageDetailsCollection; }
            set
            {
                _messageDetailsCollection = value;
                RaisePropertyChanged(nameof(MessageDetailsCollection));
            }
        }

        private MessageDetailsDto _selectedItem;

        public MessageDetailsDto SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }
        public SmsManagerViewModel(SmsDialogViewModel smsDialogViewModel, IEventAggregator eventAggregator)
        {
            _smsDialogViewModel = smsDialogViewModel;
            _eventAggregator = eventAggregator;
            LoadCommand = new DelegateCommand(OnLoad);
            CloseWindowCommand = new DelegateCommand(CloseWindow);
            OpenProfileCommand = new DelegateCommand(OpenProfile);
            TypeOfMessagesCommand = new DelegateCommand(ChangeMessageLookUp);
        }

        private void ChangeMessageLookUp()
        {
            LookForSentMessages = !LookForSentMessages;
            RaisePropertyChanged(nameof(MessageTypeContent));
            RaisePropertyChanged(nameof(MessageTypeImagePath));
            PopulateDataGrid();
        }

        private void OpenProfile()
        {
            var viewModel = new SmsProfileViewModel();
            var window = new SmsProfileWindow
            {
                DataContext = viewModel
            };
            window.ShowDialog();
        }

        private void WindowClosed()
        {
            _eventAggregator.GetEvent<WindowManagerOpenEvent>().Publish();
        }
        private void CloseWindow()
        {
            WindowClosed();
            CurrentWindow.Close();
        }


        private void ShowDialog()
        {
            DialogService.ShowDialog(_smsDialogViewModel.Commands, "Podesavanje Servisa",
                viewModel: _smsDialogViewModel);
        }

        private async void PopulateDataGrid()
        {
            var user = ConfigurationFileManager.GetSetting(ConfigKeys.SmsUser.ToString());
            var pass = ConfigurationFileManager.GetSetting(ConfigKeys.SmsSecret.ToString());
         
            _messageDetailsService = new MessageDetailsService(user, pass, LookForSentMessages);
            

            var messageList = await _messageDetailsService.ReturnMessageDetailsAsync();
            if (MessageDetailsCollection != null 
                && MessageDetailsCollection.Any())
            {
                MessageDetailsCollection.Clear();
                
            }
            MessageDetailsCollection = new ObservableCollection<MessageDetailsDto>(messageList);
        }
        private void OnLoad()
        {
            bool isValueEmpty = ConfigurationFileManager.CheckIfThereIsValue(ConfigKeys.SmsUrl.ToString());
            if (isValueEmpty)
            {
                ShowDialog();
            }
            PopulateDataGrid();
            
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
