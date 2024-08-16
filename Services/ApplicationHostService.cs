using MIM_Tool.Contracts.Activation;                                                             
using MIM_Tool.Contracts.Services;                                                               
using MIM_Tool.Contracts.Views;                                                                  
using MIM_Tool.Views;                                                                            

using Microsoft.Extensions.Hosting;                                                               // Importiert Hosting-Funktionen.

namespace MIM_Tool.Services
{
    public class ApplicationHostService : IHostedService                                          // Implementiert den IHostedService für das Hosting der Anwendung.
    {
        private readonly IServiceProvider _serviceProvider;                                       // Dienstanbieter für die Abhängigkeitsinjektion.
        private readonly INavigationService _navigationService;                                   // Dienst für die Navigation.
        private readonly IPersistAndRestoreService _persistAndRestoreService;                     // Dienst zum Speichern und Wiederherstellen von Daten.
        private readonly IThemeSelectorService _themeSelectorService;                             // Dienst zur Auswahl des Themas.
        private readonly IEnumerable<IActivationHandler> _activationHandlers;                     // Sammlung von Aktivierungshandlern.
        private IShellWindow _shellWindow;                                                        // Hauptfenster der Anwendung.
        private bool _isInitialized;                                                              // Gibt an, ob die Anwendung initialisiert wurde.

        public ApplicationHostService(IServiceProvider serviceProvider, IEnumerable<IActivationHandler> activationHandlers, INavigationService navigationService, IThemeSelectorService themeSelectorService, IPersistAndRestoreService persistAndRestoreService)
        {
            _serviceProvider = serviceProvider;                                                   // Initialisiert den Dienstanbieter.
            _activationHandlers = activationHandlers;                                             // Initialisiert die Aktivierungshandler.
            _navigationService = navigationService;                                               // Initialisiert den Navigationsdienst.
            _themeSelectorService = themeSelectorService;                                         // Initialisiert den Themendienst.
            _persistAndRestoreService = persistAndRestoreService;                                 // Initialisiert den Dienst zum Speichern und Wiederherstellen.
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
                                                                                                  // Initialisiert Dienste, die vor der Aktivierung der App benötigt werden.
            await InitializeAsync();

                                                                                                  // Handhabt die Aktivierung der App.
            await HandleActivationAsync();

                                                                                                  // Aufgaben nach der Aktivierung.
            await StartupAsync();
            _isInitialized = true;                                                                // Setzt den Initialisierungsstatus auf true.
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _persistAndRestoreService.PersistData();                                              // Speichert Daten.
            await Task.CompletedTask;                                                             // Beendet die Aufgabe.
        }

        private async Task InitializeAsync()
        {
            if (!_isInitialized)                                                                  // Überprüft, ob die Anwendung bereits initialisiert wurde.
            {
                _persistAndRestoreService.RestoreData();                                          // Stellt Daten wieder her.
                _themeSelectorService.InitializeTheme();                                          // Initialisiert das Thema.
                await Task.CompletedTask;                                                         // Beendet die Aufgabe.
            }
        }

        private async Task StartupAsync()
        {
            if (!_isInitialized)                                                                  // Überprüft, ob die Anwendung bereits initialisiert wurde.
            {
                await Task.CompletedTask;                                                         // Beendet die Aufgabe.
            }
        }

        private async Task HandleActivationAsync()
        {
            var activationHandler = _activationHandlers.FirstOrDefault(h => h.CanHandle());       // Holt den ersten Aktivierungshandler, der die Aktivierung handhaben kann.

            if (activationHandler != null)
            {
                await activationHandler.HandleAsync();                                            // Handhabt die Aktivierung.
            }

            await Task.CompletedTask;                                                             // Beendet die Aufgabe.

            if (App.Current.Windows.OfType<IShellWindow>().Count() == 0)                          // Überprüft, ob kein Hauptfenster vorhanden ist.
            {
                                                                                                  // Standardaktivierung, die zur Standardseite der App navigiert.
                _shellWindow = _serviceProvider.GetService(typeof(IShellWindow)) as IShellWindow; // Holt das Hauptfenster.
                _navigationService.Initialize(_shellWindow.GetNavigationFrame());                 // Initialisiert den Navigationsdienst mit dem Navigationsrahmen des Hauptfensters.
                _shellWindow.ShowWindow();                                                        // Zeigt das Hauptfenster an.
                _navigationService.NavigateTo(typeof(HauptseitePage));                            // Navigiert zur Hauptseite.
                await Task.CompletedTask;                                                         // Beendet die Aufgabe.
            }
        }
    }
}
