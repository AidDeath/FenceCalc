using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace FenceCalc
{
    /// <summary>
    /// Configured DI without IHost. HostedServices - NOT works, other services - works.
    /// In app.xaml we have : StartupUri="Views/MainWindow.xaml"
    /// </summary>
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        public App()
        {
            Services = ConfigureServices();
            this.InitializeComponent();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //Here we add services

            return services.BuildServiceProvider();
        }
    }
}