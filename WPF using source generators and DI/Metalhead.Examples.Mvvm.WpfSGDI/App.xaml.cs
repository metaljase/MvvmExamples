using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

using Metalhead.Examples.Mvvm.WpfSGDI.ViewModels;
using Metalhead.Examples.Mvvm.WpfSGDI.Views;

namespace Metalhead.Examples.Mvvm.WpfSGDI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IHost Host { get; private set; }

        public App()
        {
            var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder();

            builder.Services.AddSingleton<ILogger, DebugLogger>();
            builder.Services.AddSingleton<Shell>();
            builder.Services.AddSingleton<TemperatureConversion>();
            builder.Services.AddSingleton<Name>();
            builder.Services.AddSingleton<ShellViewModel>();
            builder.Services.AddSingleton<TemperatureConversionViewModel>();
            builder.Services.AddSingleton<NameViewModel>();

            Host = builder.Build();
        }

        public new static App Current => (App)Application.Current;

        protected override async void OnStartup(StartupEventArgs e)
        {
            await Host.StartAsync();

            using var serviceScope = Host.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            try
            {
                serviceProvider.GetRequiredService<Shell>().Show();

                base.OnStartup(e);
            }
            catch (Exception)
            {
                ILogger logger = serviceProvider.GetRequiredService<ILogger>();
                logger.Log("Application exited unexpectedly.");
                Environment.Exit(0);
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await Host.StopAsync();
            base.OnExit(e);
        }
    }
}
