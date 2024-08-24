using System.IO;
using Microsoft.Win32; 
using MIM_Tool.Helpers; 

namespace MIM_Tool.Funktions
{
    class FunktionSavePath
    {
        public string GetNewSavePath()                         // Methode zum Abrufen eines neuen Speicherpfads.
        {
            Log.inf("GetNewSavePath gestartet.");
            var dialog = new OpenFileDialog                    // Dialog zum Auswählen eines Pfades öffnen
            {
                CheckFileExists = false,                       // Überprüft nicht, ob die Datei existiert.
                CheckPathExists = true,                        // Überprüft, ob der Pfad existiert.
                ValidateNames = false,                         // Validiert die Namen nicht.
                FileName = "Ordnerauswahl"                     // Setzt den Dateinamen auf "Ordnerauswahl".
            };
            Log.inf("OpenFileDialog initialisiert.");
            if (dialog.ShowDialog() == true)                   // Zeigt den Dialog an und überprüft, ob der Benutzer "OK" geklickt hat.
            {
                string selectedPath = Path.GetDirectoryName(dialog.FileName); // Gibt das Verzeichnis des ausgewählten Pfades zurück.
                Log.inf($"Pfad ausgewählt: {selectedPath}");
                return selectedPath;                                 // Gibt das Verzeichnis des ausgewählten Pfades zurück.
            }
            Log.inf("Kein Pfad ausgewählt.");
            return null;                                       // Gibt null zurück, wenn kein Pfad ausgewählt wurde.
        }
    }
}
