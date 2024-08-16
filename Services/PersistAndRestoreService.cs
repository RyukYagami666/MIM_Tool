using MIM_Tool.Contracts.Services;                                                                                         
using MIM_Tool.Core.Contracts.Services;                                                                                    
using MIM_Tool.Models;                                                                                                     
using Microsoft.Extensions.Options;                                                                                        
using System.Collections;                                                                                                  
using System.IO;                                                                                                           

namespace MIM_Tool.Services
{
    public class PersistAndRestoreService : IPersistAndRestoreService                                                      // Implementiert den IPersistAndRestoreService.
    {
        private readonly IFileService _fileService;                                                                        // Dienst zum Lesen und Speichern von Dateien.
        private readonly AppConfig _appConfig;                                                                             // Anwendungskonfiguration.
        private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); // Pfad zum lokalen Anwendungsdatenordner.

        public PersistAndRestoreService(IFileService fileService, IOptions<AppConfig> appConfig)
        {
            _fileService = fileService;                                                                                    // Initialisiert den Dateidienst.
            _appConfig = appConfig.Value;                                                                                  // Initialisiert die Anwendungskonfiguration.
        }

        public void PersistData()                                                                                          // Methode zum Speichern von Daten.
        {
            if (App.Current.Properties != null)                                                                            // Überprüft, ob Anwendungs-Eigenschaften vorhanden sind.
            {
                var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);                             // Erstellt den Pfad zum Konfigurationsordner.
                var fileName = _appConfig.AppPropertiesFileName;                                                           // Holt den Dateinamen für die Eigenschaften.
                _fileService.Save(folderPath, fileName, App.Current.Properties);                                           // Speichert die Eigenschaften in einer Datei.
            }
        }

        public void RestoreData()                                                                                          // Methode zum Wiederherstellen von Daten.
        {
            var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);                                 // Erstellt den Pfad zum Konfigurationsordner.
            var fileName = _appConfig.AppPropertiesFileName;                                                               // Holt den Dateinamen für die Eigenschaften.
            var properties = _fileService.Read<IDictionary>(folderPath, fileName);                                         // Liest die Eigenschaften aus der Datei.
            if (properties != null)                                                                                        // Überprüft, ob Eigenschaften vorhanden sind.
            {
                foreach (DictionaryEntry property in properties)                                                           // Iteriert über die Eigenschaften.
                {
                    App.Current.Properties.Add(property.Key, property.Value);                                              // Fügt jede Eigenschaft zur aktuellen Anwendung hinzu.
                }
            }
        }
    }
}


