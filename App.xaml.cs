using MIM_Tool.Contracts.Services;
using MIM_Tool.Contracts.Views;
using MIM_Tool.Core.Contracts.Services;
using MIM_Tool.Core.Services;
using MIM_Tool.Models;
using MIM_Tool.Helpers;
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
        Log.inf("App-Konstruktor aufgerufen.");
    }

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        Log.inf("Anwendung startet.");
        if (e.Args.Length > 0)
        {
            Log.inf("Startargumente vorhanden.");
            if (int.TryParse(e.Args[0], out int auswahl))
            {
                Log.inf($"Startargument ist eine Zahl: {auswahl}");
                SystemSounds.Exclamation.Play();
                Log.inf("Systemsound abgespielt.");
                var kontrolle = new Funktion3MonitorKontrolle();
                kontrolle.MonitorKontrolle(auswahl);                                        // Führt die MonitorKontrolle-Funktion aus
                Log.inf("MonitorKontrolle ausgeführt.");
                Shutdown();                                                                 // Anwendung beenden, nachdem die Funktion ausgeführt wurde
                Log.inf("Anwendung wird beendet.");
            }
            else
            {
                Log.err("Startargument ist keine Zahl.", null, true);
                MessageBox.Show("Das Argument muss eine Zahl sein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                Log.inf("Fehlermeldung angezeigt.");
                Shutdown();                                                                 // Anwendung beenden, wenn das Argument keine Zahl ist
                Log.inf("Anwendung wird beendet.");
            }
        }
        else
        {
            Log.inf("Keine Startargumente vorhanden.");
            var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Log.inf($"Anwendungspfad: {appLocation}");
            // Weitere Informationen zum .NET Generic Host finden Sie unter https://docs.microsoft.com/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
            _host = Host.CreateDefaultBuilder(e.Args)
                    .ConfigureAppConfiguration(c =>
                    {
                        c.SetBasePath(appLocation);                                         // Setzt den Basispfad für die Konfiguration
                        Log.inf("Basispfad für Konfiguration gesetzt.");
                    })
                    .ConfigureServices(ConfigureServices)                                   // Konfiguriert die Dienste
                    .Build();
            Log.inf("Host erstellt.");
            await _host.StartAsync();                                                       // Startet den Host asynchron
            Log.inf("Host gestartet.");
        }
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        Log.inf("Konfiguriere Dienste.");
        // App Host
        services.AddHostedService<ApplicationHostService>();                                // Fügt den gehosteten Dienst hinzu
        Log.inf("Gehosteter Dienst hinzugefügt.");

        // Activation Handlers

        // Core Services
        services.AddSingleton<IFileService, FileService>();                                 // Fügt den Dateidienst hinzu
        Log.inf("Dateidienst hinzugefügt.");

        // Services
        services.AddSingleton<IApplicationInfoService, ApplicationInfoService>();           // Fügt den Anwendungsinformationsdienst hinzu
        services.AddSingleton<ISystemService, SystemService>();                             // Fügt den Systemdienst hinzu
        services.AddSingleton<IPersistAndRestoreService, PersistAndRestoreService>();       // Fügt den Persistenz- und Wiederherstellungsdienst hinzu
        services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();               // Fügt den Themenselektionsdienst hinzu
        services.AddSingleton<INavigationService, NavigationService>();                     // Fügt den Navigationsdienst hinzu
        Log.inf("Weitere Dienste hinzugefügt.");

        // Views
        services.AddTransient<IShellWindow, ShellWindow>();                                 // Fügt das Shell-Fenster hinzu
        services.AddTransient<HauptseitePage>();                                            // Fügt die Hauptseite hinzu
        services.AddTransient<IconSavePage>();                                              // Fügt die Icon-Speicherseite hinzu
        services.AddTransient<FunktionPage>();                                              // Fügt die Funktionsseite hinzu
        services.AddTransient<SettingsPage>();                                              // Fügt die Einstellungsseite hinzu
        Log.inf("Views hinzugefügt.");

        // Configuration
        services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig))); // Konfiguriert die App-Konfiguration
        Log.inf("App-Konfiguration konfiguriert.");
    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        Log.inf("Anwendung wird beendet.");
        await _host.StopAsync();                                                            // Stoppt den Host asynchron
        Log.inf("Host gestoppt.");
        _host.Dispose();                                                                    // Gibt die Ressourcen des Hosts frei
        Log.inf("Host-Ressourcen freigegeben.");
        _host = null;                                                                       // Setzt den Host auf null
        Log.inf("Host auf null gesetzt.");
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        Log.err("Unbehandelte Ausnahme aufgetreten.", e.Exception, true);
        // TODO: Bitte protokollieren und behandeln Sie die Ausnahme entsprechend Ihrem Szenario
        // Weitere Informationen finden Sie unter https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
    }
}
