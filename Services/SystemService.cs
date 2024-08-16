using MIM_Tool.Contracts.Services;               
using System.Diagnostics;                        

namespace MIM_Tool.Services
{
    public class SystemService : ISystemService  // Implementiert den ISystemService.
    {
        public SystemService()
        {
        }

        public void OpenInWebBrowser(string url) // Methode zum Öffnen einer URL im Webbrowser.
        {
          // Weitere Informationen finden Sie unter https://github.com/dotnet/corefx/issues/10361
            var psi = new ProcessStartInfo
            {
                FileName = url,                  // Setzt die URL als Dateiname.
                UseShellExecute = true           // Verwendet die Shell zum Ausführen des Prozesses.
            };
            Process.Start(psi);                  // Startet den Prozess zum Öffnen der URL im Standard-Webbrowser.
        }
    }
}


