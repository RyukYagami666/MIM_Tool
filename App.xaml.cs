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
using System.Media;

namespace MIM_Tool;

// Weitere Informationen zu Anwendungslebenszyklusereignissen finden Sie unter https://docs.microsoft.com/dotnet/framework/wpf/app-development/application-management-overview

// WPF-UI-Elemente verwenden standardmäßig die Sprache en-US.
// Wenn Sie andere Kulturen unterstützen müssen, stellen Sie sicher, dass Sie Konverter hinzufügen und überprüfen Sie Datums- und Zahlenformate in Ihrer UI, um sicherzustellen, dass alles korrekt angepasst wird.
// Das Problem zur Verbesserung wird hier verfolgt: https://github.com/dotnet/wpf/issues/1946
public partial class App : Application
{
    private IHost _host;                                                                    // Host für die Anwendung

    public T GetService<T>()
        where T : class
        => _host.Services.GetService(typeof(T)) as T;                                       // Methode zum Abrufen eines Dienstes

    public App()
    {
    }

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        if (e.Args.Length > 0)
        {
            if (int.TryParse(e.Args[0], out int auswahl))
            {
                SystemSounds.Exclamation.Play();

                var kontrolle = new Funktion3MonitorKontrolle();
                kontrolle.MonitorKontrolle(auswahl);                                        // Führt die MonitorKontrolle-Funktion aus
                Shutdown();                                                                          // Anwendung beenden, nachdem die Funktion ausgeführt wurde
            }
            else
            {
                MessageBox.Show("Das Argument muss eine Zahl sein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();                                                                 // Anwendung beenden, wenn das Argument keine Zahl ist
            }
        }
        else
        {
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                                                                                            // Weitere Informationen zum .NET Generic Host finden Sie unter https://docs.microsoft.com/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
            _host = Host.CreateDefaultBuilder(e.Args)
                    .ConfigureAppConfiguration(c =>
                    {
                        c.SetBasePath(appLocation);                                         // Setzt den Basispfad für die Konfiguration
                    })
                    .ConfigureServices(ConfigureServices)                                   // Konfiguriert die Dienste
                    .Build();

            await _host.StartAsync();                                                       // Startet den Host asynchron
        }
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        // App Host
        services.AddHostedService<ApplicationHostService>();                                // Fügt den gehosteten Dienst hinzu

        // Activation Handlers

        // Core Services
        services.AddSingleton<IFileService, FileService>();                                 // Fügt den Dateidienst hinzu

        // Services
        services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();           // Fügt den Anwendungsinformationsdienst hinzu
        services.AddSingleton<ISystemService, SystemService>();                             // Fügt den Systemdienst hinzu
        services.AddSingleton<IPersistAndRestoreService, PersistAndRestoreService>();       // Fügt den Persistenz- und Wiederherstellungsdienst hinzu
        services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();               // Fügt den Themenselektionsdienst hinzu
        services.AddSingleton<INavigationService, NavigationService>();                     // Fügt den Navigationsdienst hinzu

        // Views
        services.AddTransient<IShellWindow, ShellWindow>();                                 // Fügt das Shell-Fenster hinzu
        services.AddTransient<HauptseitePage>();                                            // Fügt die Hauptseite hinzu
        services.AddTransient<IconSavePage>();                                              // Fügt die Icon-Speicherseite hinzu
        services.AddTransient<FunktionPage>();                                              // Fügt die Funktionsseite hinzu
        services.AddTransient<SettingsPage>();                                              // Fügt die Einstellungsseite hinzu

        // Configuration
        services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig))); // Konfiguriert die App-Konfiguration
    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();                                                            // Stoppt den Host asynchron
        _host.Dispose();                                                                    // Gibt die Ressourcen des Hosts frei
        _host = null;                                                                       // Setzt den Host auf null
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // TODO: Bitte protokollieren und behandeln Sie die Ausnahme entsprechend Ihrem Szenario
        // Weitere Informationen finden Sie unter https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
    }
}
