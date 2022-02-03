using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Mvvm;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.WPF.Helpers;
using EtnaSoft.WPF.Views;
using EtnaSoft.WPF.Window;
using Microsoft.Extensions.Logging;

namespace EtnaSoft.WPF.ViewModels
{
    public sealed class CreateGuestContentViewModel : ContentViewModel
    {
        #region Fields

        
        private Guest _selectedGuest;

        public Guest SelectedGuest
        {
            get { return _selectedGuest; }
            set
            {
                _selectedGuest = value;
                RaisePropertyChanged(nameof(SelectedGuest));
            }
        }

        private ObservableCollection<Guest> _dataGrid = new ObservableCollection<Guest>();

        public ObservableCollection<Guest> DataGrid
        {
            get { return ReturnGuests(); }
            set
            {
                if (value == _dataGrid)
                {
                    return;
                }

                _dataGrid = value;
                RaisePropertyChanged(nameof(DataGrid));
            }
        }

        #endregion


        private readonly ICreateGuestService _createGuestService;
        private readonly IGuestSearchService _guestSearchService;
        private readonly IUpdateGuestService _updateGuestService;
        private readonly IGuestHistoryService _guestHistoryService;
        private readonly ILogger<EditGuestViewModel> _editGuestLogger;
        public ICommand NewGuestCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand<object> CellDoubleClickCommand { get; }
        public ICommand<object> EditGuestCommand { get; }
        public ICommand<object> DeactivateGuestCommand { get; }
        public CreateGuestContentViewModel(ICreateGuestService createGuestService,
            IGuestSearchService guestSearchService, IUpdateGuestService updateGuestService,
            IGuestHistoryService guestHistoryService, ILogger<EditGuestViewModel> editGuestLogger)
        {
            _createGuestService = createGuestService;
            _guestSearchService = guestSearchService;
            _updateGuestService = updateGuestService;
            _guestHistoryService = guestHistoryService;
            _editGuestLogger = editGuestLogger;
            NewGuestCommand = new DelegateCommand(OpenNewGuestWindow);
            EditGuestCommand = new DelegateCommand<object>(OnCellDoubleClick);
            DeactivateGuestCommand = new DelegateCommand<object>(DeactivateGuest);
            LoadCommand = new DelegateCommand(OnLoad);
            CellDoubleClickCommand = new DelegateCommand<object>(OnCellDoubleClick);

        }

        private void DeactivateGuest(object obj)
        {
            if (obj is Guest guest)
            {
                _createGuestService.DeactivateGuest(guest.Id);
                PopulateDataGrid();
            }
        }

        private void OpenNewGuestWindow()
        {
            var viewModel = new CreateGuestViewModel(_createGuestService);
            viewModel.OnDataChange += DataGridDataChanged;
            var window = new CreateGuestWindow
            {
                DataContext = viewModel
            };
            window.ShowDialog();
            PopulateDataGrid();
            viewModel.OnDataChange -= DataGridDataChanged;
            viewModel.Dispose();
        }

        private void OnCellDoubleClick(object obj)
        {
            var guest = (Guest)obj;
            var viewModel = new EditGuestViewModel(guest, _updateGuestService, _guestHistoryService, _editGuestLogger);
            var window = new EditGuestWindow()
            {
                DataContext = viewModel
            };
            viewModel.OnUserDataChange += ViewModel_OnUserDataChange1;
            window.ShowDialog();

            viewModel.OnUserDataChange -= ViewModel_OnUserDataChange1;
            viewModel = null;

        }

        private void ViewModel_OnUserDataChange1(object sender)
        {
            PopulateDataGrid();
        }

        private void PopulateDataGrid()
        {
            if (DataGrid == null)
            {
                DataGrid = new ObservableCollection<Guest>();
            }
            if (DataGrid.Any())
            {
                DataGrid.Clear();
            }

            DataGrid = ReturnGuests();
        }


        private void OnLoad()
        {


        }

        private ObservableCollection<Guest> ReturnGuests()
        {
            var guests = _guestSearchService.GetGuests();
            return new ObservableCollection<Guest>(guests);
        }

        void DataGridDataChanged()
        {
            RaisePropertyChanged(nameof(DataGrid));
        }

    }

}
