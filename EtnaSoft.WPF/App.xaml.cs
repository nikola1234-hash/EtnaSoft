using System;
using System.Configuration;
using System.Windows;
using DevExpress.Mvvm.POCO;
using EtnaSoft.Dal;
using EtnaSoft.WPF.Services;
using EtnaSoft.WPF.Stores;
using EtnaSoft.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;

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
            service.AddSingleton<IViewStore, ViewStore>();
            service.AddSingleton<IEtnaViewModelFactory, ViewModelFactory>();

            service.AddScoped(typeof(MainViewModel), ViewModelSource.GetPOCOType(typeof(MainViewModel)));
            service.AddScoped<LoginViewModel>();
            service.AddScoped<HomeViewModel>();
            service.AddScoped<MainWindow>();
        }

        
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            EtnaSettings.ConnectionString = ConfigurationManager.ConnectionStrings["SqlDb"].ToString();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            
            var mainViewModel = ServiceProvider.GetRequiredService<MainViewModel>();
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();
        }
    }
}
