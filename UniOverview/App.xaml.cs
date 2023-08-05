using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Windows;
using UniOverview.Services;
using UniOverview.ViewModels;
using UniOverview.ViewModels.Grades;
using UniOverview.ViewModels.Materials;
using UniOverview.ViewModels.Subjects;

namespace UniOverview
{
    public partial class App : Application
    {
        private readonly IHost _host;
        private ServiceProvider _serviceProvider;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(
                    (context, services) =>
                    {
                        // SERVICES
                        services.AddSingleton<NavigationService>();

                        //VIEWMODELS
                        services.AddSingleton<MainViewModel>();
                        //// Flipping table view models
                        services.AddSingleton<GradesBaseViewModel>();
                        services.AddSingleton<MaterialsBaseViewModel>();
                        services.AddSingleton<SubjectsBaseViewModel>();
                        services.AddSingleton<SubjectsBaseViewModel>();
                        services.AddSingleton<SideMenuViewModel>();

                        _serviceProvider = services.BuildServiceProvider();
                    }
                )
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationService navigationService = _serviceProvider.GetService<NavigationService>();
            navigationService.CurrentViewModel = _serviceProvider.GetService<GradesBaseViewModel>();

            MainWindow mainWindow =
                new() { DataContext = _serviceProvider.GetService<MainViewModel>() };

            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
