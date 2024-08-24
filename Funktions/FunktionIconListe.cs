using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using IWshRuntimeLibrary;
using MIM_Tool.Services;
using MIM_Tool.Helpers;

namespace MIM_Tool.Funktions
{
    internal class FunktionIconListe//-------------------------------------------------------------------------------------------Speichern der Icons Hauptliste-----------------------------------------------------------------------------------------------------------------------
    {
        public static List<FileIconInfo> LastExecutedFiles { get; private set; }         // Statische Eigenschaft für die zuletzt ausgeführten Dateien.

        public static string GetDesktopPath()                                            // Methode zum Abrufen des Desktop-Pfads.
        {
            Log.inf("Desktoppfad auslesen.");
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);         // Gibt den Pfad zum Desktop zurück.
        }

        public static List<FileIconInfo> Execute()                                       // Methode zum Ausführen der Icon-Liste.
        {
            Log.inf("Star Statt mit den Holen von Bilddateien aus Bildschirm Icons");
            try
            {
                string deskOkDataTrim = Properties.Settings.Default.DeskOkDataTrim;          // Holt die gespeicherten Daten.
                Log.inf("Daten von Einstellung lesesen, und in Liste wandeln:" + deskOkDataTrim);
                string[] deskOkData = deskOkDataTrim.Split(';');                             // Teilt die Daten in ein Array.
                List<string> deskOkNames = deskOkData.ToList();                              // Konvertiert das Array in eine Liste.
                List<FileIconInfo> files = new List<FileIconInfo>();                         // Erstellt eine neue Liste für die Dateien.
                Log.inf("Liste für Dateien erstellen.");
                foreach (var filePath in deskOkNames)
                {
                    if (System.IO.File.Exists(filePath))                                               // Überprüfen, ob die Datei existiert.
                    {
                        Log.inf("Icon Bild existiert von: " + filePath);
                        Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(filePath);     // Extrahiert das Icon der Datei.
                        BitmapImage bitmapImage = ConvertIconToImageSource(icon);            // Konvertiert das Icon in ein BitmapImage.
                        files.Add(new FileIconInfo
                        {
                            Path = filePath,                                                 // Setzt den Pfad der Datei.
                            Icon = bitmapImage                                               // Setzt das Icon der Datei.
                        });
                    }
                }
                Log.inf("Speichern der Daten");
                LastExecutedFiles = files;                                                   // Speichern der Liste in der statischen Eigenschaft.
                ISPSaveState.IsReady = true;                                                 // Setzt den Zustand auf bereit.
                Log.inf("fertig");//ex,alarm

                return files;
                // Gibt die Liste der Dateien zurück.
            }
            catch (Exception ex)
            {
                Log.err("Fehler beim Ausführen der Icon-Liste.", ex, true);                  // Loggt den Fehler.
                return null;                                                                // Gibt null zurück, wenn ein Fehler auftritt.
            }

        }
        //------------------------------------------------------------------------------------------------------------------------Hauptseite Monitor auswahl--------------------------------------------------------------------------------------------------------------------------------------------------------------------
      
        public static List<FileIconInfo> LastSavedFiles { get; private set; }            // Statische Eigenschaft für die zuletzt gespeicherten Dateien.

        public static List<FileIconInfo> gespeicherteIcons()                             // Methode zum Abrufen der gespeicherten Icons.
        {
            Log.inf("Starte holender Bilddateien aus Icons für die zugewisenen IconMonitorlisten ");
            int iSelMonitor = Properties.Settings.Default.SelectetMonitor;               // Holt den ausgewählten Monitor.

            string[] gespeicherteIconListen =
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen1,                    // Icons für Monitor 1.
                Properties.Settings.Default.eMonitorIconsZugewiesen2,                    // Icons für Monitor 2.
                Properties.Settings.Default.eMonitorIconsZugewiesen3,                    // Icons für Monitor 3.
                Properties.Settings.Default.eMonitorIconsZugewiesen4                     // Icons für Monitor 4.
            };
            Log.inf("holen von allen zugewiesenen Tabelle  ");
            if (iSelMonitor < 4 && !string.IsNullOrEmpty(gespeicherteIconListen[iSelMonitor]))
            {
                Log.inf($"Ausgewählter Monitor: {iSelMonitor}");
                string gespeicherteIconListe = gespeicherteIconListen[iSelMonitor];      // Holt die gespeicherte Icon-Liste für den ausgewählten Monitor.
                string[] savedListAuswawl = gespeicherteIconListe.Split(';');            // Teilt die Liste in ein Array.
                List<string> savedListAuswawlIcons = savedListAuswawl.ToList();          // Konvertiert das Array in eine Liste.
                List<FileIconInfo> files = new List<FileIconInfo>();                     // Erstellt eine neue Liste für die Dateien.

                foreach (var filePath in savedListAuswawlIcons)
                {
                    Log.inf($"Überprüfen, ob die Datei existiert: {filePath}");
                    if (System.IO.File.Exists(filePath))                                           // Überprüfen, ob die Datei existiert.
                    {
                        Log.inf($"Datei existiert: {filePath}");
                        Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(filePath); // Extrahiert das Icon der Datei.
                        BitmapImage bitmapImage = ConvertIconToImageSource(icon);        // Konvertiert das Icon in ein BitmapImage.
                        files.Add(new FileIconInfo
                        {
                            Path = filePath,                                             // Setzt den Pfad der Datei.
                            Icon = bitmapImage                                           // Setzt das Icon der Datei.
                        });
                        Log.inf($"Icon hinzugefügt: {filePath}");
                    }
                    else
                    {
                        Log.inf($"Datei existiert nicht: {filePath}");
                    }
                }
                Log.inf("Speichern der Liste in der statischen Eigenschaft.");
                LastSavedFiles = files;                                                  // Speichern der Liste in der statischen Eigenschaft.
                return files;                                                            // Gibt die Liste der Dateien zurück.
            }
            else
            {
                Log.inf("Kein gültiger Monitor ausgewählt oder keine Icons gespeichert.");
                return null;                                                              // Gibt null zurück, wenn keine Icons gespeichert sind.
            }
        }
        //------------------------------------------------------------------------------------------------------------------------Konvertieren des Icons in ein BitmapImage-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static BitmapImage ConvertIconToImageSource(Icon icon)                   // Methode zum Konvertieren eines Icons in ein BitmapImage.
        {
            Log.inf("Konvertieren des Icons in ein BitmapImage.");
            using (MemoryStream ms = new MemoryStream())
            {
                Log.inf("Speichern des Icons als PNG in den MemoryStream.");
                icon.ToBitmap().Save(ms, ImageFormat.Png);                               // Speichert das Icon als PNG in den MemoryStream.
                ms.Position = 0;                                                         // Setzt die Position des MemoryStream auf 0.
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;                                           // Setzt die Quelle des BitmapImage auf den MemoryStream.
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;                      // Setzt die Cache-Option auf OnLoad.
                bitmapImage.EndInit();
                Log.inf("Konvertierung abgeschlossen.");
                return bitmapImage;                                                      // Gibt das BitmapImage zurück.
            }
        }
                                                                                       
        public class FileIconInfo                                                        // Definieren Sie zusätzliche Klassen außerhalb der DataPage-Klasse.
        {
            public string Path { get; set; }                                             // Pfad der Datei.
            public BitmapImage Icon { get; set; }                                        // Icon der Datei.
        }
    }
}
