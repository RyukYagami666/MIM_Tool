using MIM_Tool.Contracts.Services;                                                      // Importiert Schnittstellen für Dienste.
using System.Diagnostics;                                                               // Importiert Funktionen zur Diagnose.
using System.Reflection;                                                                // Importiert Funktionen zur Reflexion.

namespace MIM_Tool.Services
{
    public class ApplicationInfoService : IApplicationInfoService                       // Implementiert den IApplicationInfoService.
    {
        public ApplicationInfoService()
        {
        }

        public Version GetVersion()                                                     // Methode zum Abrufen der Anwenderversion.
        {
            // Setzt die App-Version in MIM_Tool > Properties > Package > PackageVersion.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;         // Holt den Speicherort der aktuellen Assembly.
            var version = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion; // Holt die Dateiversion der Assembly.
            return new Version(version);                                                // Gibt die Version als Version-Objekt zurück.
        }
    }
}

