using System;
using System.Configuration;
using System.Windows;
using DevExpress.Mvvm.POCO;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal;
using EtnaSoft.Dal.Infrastucture;
using EtnaSoft.Dal.Repositories;
using EtnaSoft.Dal.Services;
using EtnaSoft.Dal.Services.Authorization;
using EtnaSoft.WPF.Services;
using EtnaSoft.WPF.Services.Authentication;
using EtnaSoft.WPF.Services.Reception;
using EtnaSoft.WPF.Stores;
using EtnaSoft.WPF.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Prism.Events;

namespace EtnaSoft.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider ServiceProvider { get; }
        public App()
        {
            ServiceCollection service = new ServiceCollection();
            ConfigureServices(service);
            ServiceProvider = service.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection service)
        {
            service.AddSingleton<AdminService>();
            service.AddSingleton<IPasswordHasher, PasswordHasher>();
            service.AddSingleton<IAuthorization, AuthorizationService>();
            service.AddSingleton<IAuthenticator, Authenticator>();


            service.AddSingleton<IGuestSearchService,GuestSearchService>();
            service.AddSingleton<IDetailsManager, DetailsManager>();
            service.AddSingleton<IUpdateBookingService, UpdateBookingService>();
            service.AddSingleton<IResourceService, ResourceService>();
            service.AddSingleton<ISchedulerService, SchedulerService>();
            service.AddSingleton<IBookingService, BookingService>();
            service.AddSingleton<ICreateGuestService, CreateGuestService>();
            service.AddSingleton<IRepository<User>, UserRepository>();
            service.AddSingleton<IRepository<Guest>, GuestRepository>();
            service.AddSingleton<IRepository<Room>, RoomRepository>();
            service.AddSingleton<IRepository<Reservation>, ReservationRepository>();
            service.AddSingleton<IRepository<RoomReservation>, RoomReservationRepository>();
            service.AddSingleton<IRepository<StayType>, StayTypeRepository>();
            service.AddSingleton<IRepository<CustomLabel>, LabelRepository>();

            service.AddSingleton<IUnitOfWork, UnitOfWork>();
            service.AddSingleton<IGenericDbContext, GenericDbContext>();

            service.AddSingleton<IRenavigate, Renavigate>();
            service.AddSingleton<IViewStore, ViewStore>();
            service.AddSingleton<IEtnaViewModelFactory, ViewModelFactory>();
            service.AddSingleton<IEventAggregator, EventAggregator>();

            service.AddTransient(typeof(MainViewModel), ViewModelSource.GetPOCOType(typeof(MainViewModel)));
            service.AddTransient<CreateAppointmentViewModel>();
            service.AddTransient<AppointmentViewModel>();
            service.AddTransient<ReceptionViewModel>();
            service.AddTransient<LoginViewModel>();
            service.AddTransient<HomeViewModel>();
            service.AddTransient<MainWindow>();
        }

        
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            EtnaSettings.ConnectionString = ConfigurationManager.ConnectionStrings["SqlDb"].ToString();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            
            var mainViewModel = ServiceProvider.GetRequiredService<MainViewModel>();
            mainWindow.DataContext = mainViewModel;

            var adminService = ServiceProvider.GetRequiredService<AdminService>();
            bool accountExists = adminService.CheckIfAccountExists();
            if (!accountExists)
            {
                adminService.FirstUserCreation();
            }

            mainWindow.Show();
        }
    }
}
