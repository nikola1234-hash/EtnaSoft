using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using EtnaSoft.Bll.BulkSms.Helpers;
using EtnaSoft.WPF.Events;
using EtnaSoft.WPF.Services.Configuration;
using EtnaSoft.WPF.Services.SmsService;
using Prism.Events;


namespace EtnaSoft.WPF.ViewModels
{
    public sealed class SmsManagerViewModel : EtnaBaseViewModel
    {
        
        private ICurrentWindowService CurrentWindow => GetService<ICurrentWindowService>();
        private IDialogService DialogService => GetService<IDialogService>();
        public ICommand LoadCommand { get; }
        public ICommand CloseWindowCommand { get; }
        private readonly IEventAggregator _eventAggregator;

        private readonly SmsDialogViewModel _smsDialogViewModel;
        public SmsManagerViewModel(SmsDialogViewModel smsDialogViewModel, IEventAggregator eventAggregator)
        {
            _smsDialogViewModel = smsDialogViewModel;
            _eventAggregator = eventAggregator;
            LoadCommand = new DelegateCommand(OnLoad);
            CloseWindowCommand = new DelegateCommand(CloseWindow);
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
        private void OnLoad()
        {
            var isValueEmpty = ConfigurationFileManager.CheckIfThereIsValue(ConfigKeys.SmsUrl.ToString());
            if (isValueEmpty)
            {
                ShowDialog();
            }
        }
    }
}
