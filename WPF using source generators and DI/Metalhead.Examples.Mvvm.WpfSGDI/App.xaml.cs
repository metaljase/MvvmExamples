using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
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
        public IHost Host { get; }

        public App()
        {
            Host = Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder()
                .ConfigureAppConfiguration(hostConfig =>
                {
                    hostConfig.SetBasePath(Directory.GetCurrentDirectory());
                })
                .ConfigureAppConfiguration((hostingContext, hostConfig) =>
                {
                    hostConfig.AddConfiguration(GetConfiguration(hostConfig));
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ILogger, DebugLogger>();
                    services.AddSingleton<Shell>();
                    services.AddSingleton<TemperatureConversion>();
                    services.AddSingleton<Name>();
                    services.AddSingleton<ShellViewModel>();
                    services.AddSingleton<TemperatureConversionViewModel>();
                    services.AddSingleton<NameViewModel>();
                })
                .Build();
        }

        public new static App Current => (App)Application.Current;

        private static IConfigurationRoot GetConfiguration(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());

            return builder.Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await Host!.StartAsync();

            try
            {
                Host.Services.GetRequiredService<Shell>().Show();

                base.OnStartup(e);
            }
            catch (Exception)
            {
                ILogger logger = Host.Services.GetRequiredService<ILogger>();
                logger.Log("Application exited unexpectedly.");
                Environment.Exit(0);
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await Host!.StopAsync();
            base.OnExit(e);
        }
    }
}
