using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Configuration;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bll.Stores;
using EtnaSoft.Dal.Services;

namespace EtnaSoft.WPF.ViewModels
{
    public class DialogServiceViewModel : EtnaBaseViewModel
    {
      public List<UICommand> DialogCommands { get; private set; }
      protected UICommand RegisterUICommand { get; private set; }
      protected UICommand CancelUICommand { get; private set; }
      private bool _allowCloseDialog = false;

      private string _firstName;

      public string FirstName
      {
          get { return _firstName; }
          set
          {
              _firstName = value;
              SetProperty(ref _firstName, value, () => FirstName);
          }
      }

      private string _lastName;

      public string LastName
      {
          get { return _lastName; }
          set
          {
              _lastName = value;
              SetProperty(ref _lastName, value, () => LastName);
          }
      }

      private string _address;

      public string Address
      {
          get { return _address; }
          set
          {
              _address = value;
              SetProperty(ref _address, value, () => Address);
          }
      }

      private string _telephone;

      public string Telephone
      {
          get { return _telephone; }
          set
          {
              _telephone = value;
              SetProperty(ref _telephone, value, () => Telephone);
          }
      }

      private string _emailAddress;

      public string EmailAddress
      {
          get { return _emailAddress; }
          set
          {
              _emailAddress = value;
              SetProperty(ref _emailAddress, value, () => EmailAddress);
          }
      }

      private DateTime _birthDate = DateTime.Now.Date.AddYears(-20);

      public DateTime  BirthDate

      {
          get { return _birthDate; }
          set
          {
              _birthDate = value;
              SetProperty(ref _birthDate, value, () => BirthDate);
          }
      }

      private string _uniqueNumber;

      public string UniqueNumber
      {
          get { return _uniqueNumber; }
          set
          {
              _uniqueNumber = value;
              SetProperty(ref _uniqueNumber, value, () => UniqueNumber);
          }
      }
      public Guest Guest { get; set; }
      private readonly ICreateGuestService _guestService;
      public DialogServiceViewModel(ICreateGuestService guestService)
      {
          _guestService = guestService;
          DialogCommands = new List<UICommand>();
          RegisterUICommand = new UICommand(
              id:MessageBoxResult.OK,
              command: new DelegateCommand<CancelEventArgs>(RegisterCommand, CanRegisterExecute),
              isDefault:true,
              isCancel:false,
              caption: "Registruj");
          CancelUICommand = new UICommand
              (
              id: MessageBoxResult.Cancel,
              isCancel:true,
              isDefault:false,
              command:new DelegateCommand<CancelEventArgs>(CancelExecute),
              caption:"Odustani"
              );
          DialogCommands.Add(RegisterUICommand);
          DialogCommands.Add(CancelUICommand);
          
      }

      private void CancelExecute(CancelEventArgs args)
      {
          if (!_allowCloseDialog)
          {
              args.Cancel = true;
          }
      }

      private bool CanRegisterExecute(CancelEventArgs args)
      {
          bool canRegister = !string.IsNullOrEmpty(FirstName)
                             && !string.IsNullOrEmpty(LastName)
                             && !string.IsNullOrEmpty(Telephone);
          return canRegister;
      }

      private void RegisterCommand(CancelEventArgs args)
      {
          Guest GuestMappingLogic()
          {
              Guest = new Guest
              {
                  FirstName = FirstName,
                  LastName = LastName,
                  Telephone = Telephone,
                  Address = Address,
                  EmailAddress = EmailAddress,
                  BirthDate = BirthDate,
                  UniqueNumber = UniqueNumber,
                  CreatedBy = UserStore.CurrentUser,
                  DateCreated = DateTime.Now
              };
             
              return Guest;
          }

          var newGuest = GuestMappingLogic();
          var createdGuest = _guestService.CreateGuest(newGuest);
          RegisterUICommand.Id = createdGuest.Id;
      }

      public override void Dispose()
      {
          base.Dispose();
      }
    }
}
