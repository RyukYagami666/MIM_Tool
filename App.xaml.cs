using MIM_Tool.Contracts.Services;
using MIM_Tool.Contracts.Views;
using MIM_Tool.Core.Contracts.Services;
using MIM_Tool.Core.Services;
using MIM_Tool.Models;
using MIM_Tool.Services;
using MIM_Tool.Views;
using MIM_Tool.Funktions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using IniParser;
using IniParser.Model;


namespace MIM_Tool;


// For more information about application lifecycle events see https://docs.microsoft.com/dotnet/framework/wpf/app-development/application-management-overview

// WPF UI elements use language en-US by default.
// If you need to support other cultures make sure you add converters and review dates and numbers in your UI to ensure everything adapts correctly.
// Tracking issue for improving this is https://github.com/dotnet/wpf/issues/1946
public partial class App : Application
{
    private IHost _host;

    public T GetService<T>()
        where T : class
        => _host.Services.GetService(typeof(T)) as T;

    public App()
    {
    }

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        if (e.Args.Length > 0)
        {
            if (int.TryParse(e.Args[0], out int auswahl))
            {
                MessageBox.Show($"Auswahl: {auswahl}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                var kontrolle = new Funktion3MonitorKontrolle();
                kontrolle.MonitorKontrolle(auswahl);

                // Anwendung beenden, nachdem die Funktion ausgeführt wurde
                Shutdown();
            }
            else
            {
                MessageBox.Show("Das Argument muss eine Zahl sein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }
        else
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            // For more information about .NET generic host see  https://docs.microsoft.com/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
            _host = Host.CreateDefaultBuilder(e.Args)
                    .ConfigureAppConfiguration(c =>
                    {
                        c.SetBasePath(appLocation);
                    })
                    .ConfigureServices(ConfigureServices)
                    .Build();

            await _host.StartAsync();
        }
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        // TODO: Register your services, viewmodels and pages here

        // App Host
        services.AddHostedService<ApplicationHostService>();

        // Activation Handlers

        // Core Services
        services.AddSingleton<IFileService, FileService>();

        // Services
        services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();
        services.AddSingleton<ISystemService, SystemService>();
        services.AddSingleton<IPersistAndRestoreService, PersistAndRestoreService>();
        services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
        services.AddSingleton<INavigationService, NavigationService>();

        // Views
        services.AddTransient<IShellWindow, ShellWindow>();

        services.AddTransient<HauptseitePage>();

        services.AddTransient<IconSavePage>();

        services.AddTransient<FunktionPage>();

        services.AddTransient<SettingsPage>();

        // Configuration
        services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
        _host = null;
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // TODO: Please log and handle the exception as appropriate to your scenario
        // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
    }
}
