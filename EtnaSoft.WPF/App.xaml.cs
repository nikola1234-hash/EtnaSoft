using System;
using System.Configuration;
using System.IO;
using System.Windows;
using DevExpress.Mvvm.POCO;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bll.Services;
using EtnaSoft.Bll.Services.Facade;
using EtnaSoft.Dal;
using EtnaSoft.Dal.Infrastucture;
using EtnaSoft.Dal.Repositories;
using EtnaSoft.Dal.Services;
using EtnaSoft.Dal.Services.Authorization;
using EtnaSoft.Dal.Services.Converter;
using EtnaSoft.Dal.Services.Database;
using EtnaSoft.Dal.Services.UserServices;
using EtnaSoft.WPF.Infrastructure;
using EtnaSoft.WPF.Services;
using EtnaSoft.WPF.Services.Authentication;
using EtnaSoft.WPF.Services.Reception;
using EtnaSoft.WPF.Stores;
using EtnaSoft.WPF.ViewModels;
using EtnaSoft.WPF.Window;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Events;
using Serilog;
using ILogger = Serilog.ILogger;

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
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(Directory.GetCurrentDirectory()+"\\log.log")
                .CreateLogger();
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            
        }

        private void ConfigureServices(IServiceCollection service)
        {


            service.AddLogging(configuration => configuration.AddSerilog()).AddTransient<LoginViewModel>();
            service.AddSingleton<AdminService>();
            service.AddSingleton<DatabaseService>();
            service.AddSingleton<DatabaseCreationService>();
            service.AddSingleton<FileConverter>();
            

            service.AddSingleton<IPasswordHasher, PasswordHasher>();
            service.AddSingleton<IAuthorization, AuthorizationService>();
            service.AddSingleton<IAuthenticator, Authenticator>();
            service.AddSingleton<IComboboxFacade, ComboboxFacade>();
            service.AddSingleton<ICreateReservationService, CreateReservationTransaction>();
            service.AddSingleton<IAvailableRoomsService, AvailableRoomsService>();
            service.AddSingleton<IRoomsManagerService, RoomsManagerService>();
            // IStayTypesManager currently not used
            service.AddSingleton<IStayTypesManagerService, StayTypesManagerService>();
            service.AddSingleton<ISpecialTypeService, SpecialTypeService>();
            //Content page navigation
            service.AddSingleton<IContentViewStore, ContentViewStore>();
            service.AddSingleton<IContentViewFactory, ContentViewFactory>();
            service.AddSingleton<IWindowViewModelFactory, WindowViewModelFactory>();
            service.AddSingleton<ICreateUserViewFactory, CreateUserViewFactory>();
            service.AddTransient<ContentViewModel>();
            service.AddTransient<UserContentViewModel>();
            service.AddTransient<GuestContentViewModel>();
            service.AddTransient<CreateGuestContentViewModel>();
            service.AddTransient<CreateUserViewModel>();
            service.AddTransient<RoomsManagerViewModel>();
            service.AddTransient<StayTypesManagerViewModel>();
            service.AddTransient<UserManagerViewModel>();

            service.AddSingleton<IUserService, UserService>();
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
            service.AddSingleton<IRepository<DataGridGuest>, GuestDataGridRepository>();
            service.AddSingleton<IRepository<Promotion>, PromotionRepository>();
            service.AddSingleton<IRepository<RoomStatus>, RoomStatusRepository>();

            service.AddSingleton<IUpdateReservationDateDragService, UpdateReservationDateDragService>();
            service.AddSingleton<IUnitOfWork, UnitOfWork>();
            service.AddSingleton<IGenericDbContext, GenericDbContext>();
            service.AddSingleton<IGuestService, GuestService>();
            service.AddSingleton<IUpdateGuestService, UpdateGuestService>();
            service.AddSingleton<IGuestDataGridService, GuestDataGridService>();
            service.AddSingleton<IGuestHistoryService, GuestHistoryService>();


            service.AddTransient<IUserManagerService, UserManagerService>();
            service.AddTransient<IUserManagerSubViewModel, UserSubViewModel>();
            service.AddSingleton<IRenavigate, Renavigate>();
            service.AddSingleton<IViewStore, ViewStore>();
            service.AddSingleton<IEtnaViewModelFactory, ViewModelFactory>();
            service.AddSingleton<IEventAggregator, EventAggregator>();
            service.AddTransient<SearchGuestDialogViewModel>();
            service.AddTransient<DialogServiceViewModel>();

            service.AddTransient(typeof(MainViewModel), ViewModelSource.GetPOCOType(typeof(MainViewModel)));
            service.AddTransient<CreateAppointmentViewModel>();
            service.AddTransient<AppointmentViewModel>();
            service.AddTransient<ReceptionViewModel>();
            service.AddTransient<LoginViewModel>();
            service.AddTransient<HomeViewModel>();
            service.AddTransient<MainWindow>();
            service.AddTransient<CreateStayTypeDialogViewModel>();

            //service.AddTransient<CreateUserWindow>();
            //service.AddTransient<RoomsManagerWindow>();
            //service.AddTransient<EditGuestWindow>();
            //service.AddTransient<StayTypesManagerWindow>();
            //service.AddTransient<UserManagerWindow>();
        }

        
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var logger = ServiceProvider.GetRequiredService<ILogger<App>>();
            logger.LogInformation("[Logging App_OnStartup]");
            string dirName = ConfigurationManager.AppSettings["DirectoryName"];
            string fileName = ConfigurationManager.AppSettings["FileName"];
            var fc = ServiceProvider.GetRequiredService<FileConverter>();
            string dbScript = "\\Database.sql";//TODO: insert this to Config file
            void InitializeDatabaseScript()
            {
                logger.LogInformation("Initialize database script");

                try
                {
                    string path = Directory.GetCurrentDirectory();

                    bool doesFileExist = File.Exists(path + dirName + dbScript);
                    if (doesFileExist)
                    {
                        var script = File.ReadAllText(path + dirName + dbScript);
                        var success = fc.Save(path + dirName, script);
                        if (!success)
                        {
                            throw new Exception("FileConverter Save throws exception");
                        }

                        File.Delete(path + dirName + dbScript);
                    }

                }
                catch (Exception ex)
                {
                    logger.LogError("InitializeDatabaseScript throws error: " + ex);
                    throw;
                }
                finally
                {
                    logger.LogInformation("Finished with InitializeDatabaseScript");
                }
            }
            InitializeDatabaseScript();
            string dbName = string.Empty;
            try
            {
                logger.LogInformation("Assigning strings from configuration file");
                EtnaSettings.ConnectionString = ConfigurationManager.ConnectionStrings["SqlDb"].ToString();
                dbName = ConfigurationManager.AppSettings["DatabaseName"];
                EtnaSettings.DbName = dbName;
            }
            catch (Exception ex)
            {
                logger.LogError("Errors found on assigning strings from configuration file: " + ex);
                throw;
            }
            finally
            {
                logger.LogInformation("[Finished with logging configuration strings]");
            }

            try
            {
                logger.LogInformation("Getting DatabaseService And Checking if it exists");
                var databaseService = ServiceProvider.GetRequiredService<DatabaseService>();
                var databaseExists = databaseService.DoesDatabaseExist(dbName);
                if (databaseExists)
                {
                    
                    logger.LogInformation("Database does exist starting main window");
                    
                    CreateMainWindow();
                }
                else
                {
                    logger.LogInformation("Database doesnt exist attempting to create a new one");
                    var dbCreation = ServiceProvider.GetRequiredService<DatabaseCreationService>();
                    var success = dbCreation.CreateDatabase(dbName, dirName, fileName, fc);
                    if (success)
                    {
                        logger.LogInformation("Database exists.. Checking if master user exists");
                        var adminService = ServiceProvider.GetRequiredService<AdminService>();
                        bool accountExists = adminService.CheckIfAccountExists();
                        if (!accountExists)
                        {
                            logger.LogInformation("User does not exists, attempting to create a master user.");
                            adminService.FirstUserCreation();
                        }

                        logger.LogInformation("Starting main windows");
                        CreateMainWindow();
                    }
                    else
                    {
                        MessageBox.Show("Greska u instaliranju baze pokusajte ponovo!");
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogCritical("Application couldnt start: " + ex);
                throw;
            }
            finally
            {
                logger.LogInformation("Finished loggin main application startup.");
            }
        }

        private void CreateMainWindow()
        {
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            
            var mainViewModel = ServiceProvider.GetRequiredService<MainViewModel>();
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();
        }
    }
}
