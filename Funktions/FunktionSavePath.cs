using System.IO;
using Microsoft.Win32; 

namespace MIM_Tool.Funktions
{
    class FunktionSavePath
    {
        public string GetNewSavePath()                         // Methode zum Abrufen eines neuen Speicherpfads.
        {
            var dialog = new OpenFileDialog                    // Dialog zum Auswählen eines Pfades öffnen
            {
                CheckFileExists = false,                       // Überprüft nicht, ob die Datei existiert.
                CheckPathExists = true,                        // Überprüft, ob der Pfad existiert.
                ValidateNames = false,                         // Validiert die Namen nicht.
                FileName = "Ordnerauswahl"                     // Setzt den Dateinamen auf "Ordnerauswahl".
            };
            if (dialog.ShowDialog() == true)                   // Zeigt den Dialog an und überprüft, ob der Benutzer "OK" geklickt hat.
            {
                return Path.GetDirectoryName(dialog.FileName); // Gibt das Verzeichnis des ausgewählten Pfades zurück.
            }
            return null;                                       // Gibt null zurück, wenn kein Pfad ausgewählt wurde.
        }
    }
}
