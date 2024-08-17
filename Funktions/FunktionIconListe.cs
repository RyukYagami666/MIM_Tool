using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using IWshRuntimeLibrary;
using MIM_Tool.Services;

namespace MIM_Tool.Funktions
{
    internal class FunktionIconListe//-------------------------------------------------------------------------------------------Speichern der Icons Hauptliste-----------------------------------------------------------------------------------------------------------------------
    {
        public static List<FileIconInfo> LastExecutedFiles { get; private set; }         // Statische Eigenschaft für die zuletzt ausgeführten Dateien.

        public static string GetDesktopPath()                                            // Methode zum Abrufen des Desktop-Pfads.
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);         // Gibt den Pfad zum Desktop zurück.
        }

        public static List<FileIconInfo> Execute()                                       // Methode zum Ausführen der Icon-Liste.
        {
            string deskOkDataTrim = Properties.Settings.Default.DeskOkDataTrim;          // Holt die gespeicherten Daten.
            string[] deskOkData = deskOkDataTrim.Split(';');                             // Teilt die Daten in ein Array.
            List<string> deskOkNames = deskOkData.ToList();                              // Konvertiert das Array in eine Liste.
            List<FileIconInfo> files = new List<FileIconInfo>();                         // Erstellt eine neue Liste für die Dateien.
            foreach (var filePath in deskOkNames)
            {
                if (System.IO.File.Exists(filePath))                                               // Überprüfen, ob die Datei existiert.
                {
                    Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(filePath);     // Extrahiert das Icon der Datei.
                    BitmapImage bitmapImage = ConvertIconToImageSource(icon);            // Konvertiert das Icon in ein BitmapImage.
                    files.Add(new FileIconInfo
                    {
                        Path = filePath,                                                 // Setzt den Pfad der Datei.
                        Icon = bitmapImage                                               // Setzt das Icon der Datei.
                    });
                }
            }
            LastExecutedFiles = files;                                                   // Speichern der Liste in der statischen Eigenschaft.
            ISPSaveState.IsReady = true;                                                 // Setzt den Zustand auf bereit.
            return files;                                                                // Gibt die Liste der Dateien zurück.
        }
        //------------------------------------------------------------------------------------------------------------------------Hauptseite Monitor auswahl--------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static List<FileIconInfo> LastSavedFiles { get; private set; }            // Statische Eigenschaft für die zuletzt gespeicherten Dateien.

        public static List<FileIconInfo> gespeicherteIcons()                             // Methode zum Abrufen der gespeicherten Icons.
        {
            int iSelMonitor = Properties.Settings.Default.SelectetMonitor;               // Holt den ausgewählten Monitor.

            string[] gespeicherteIconListen =
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen1,                    // Icons für Monitor 1.
                Properties.Settings.Default.eMonitorIconsZugewiesen2,                    // Icons für Monitor 2.
                Properties.Settings.Default.eMonitorIconsZugewiesen3,                    // Icons für Monitor 3.
                Properties.Settings.Default.eMonitorIconsZugewiesen4                     // Icons für Monitor 4.
            };

            if (iSelMonitor < 4 && !string.IsNullOrEmpty(gespeicherteIconListen[iSelMonitor]))
            {
                string gespeicherteIconListe = gespeicherteIconListen[iSelMonitor];      // Holt die gespeicherte Icon-Liste für den ausgewählten Monitor.
                string[] savedListAuswawl = gespeicherteIconListe.Split(';');            // Teilt die Liste in ein Array.
                List<string> savedListAuswawlIcons = savedListAuswawl.ToList();          // Konvertiert das Array in eine Liste.
                List<FileIconInfo> files = new List<FileIconInfo>();                     // Erstellt eine neue Liste für die Dateien.
                foreach (var filePath in savedListAuswawlIcons)
                {
                    if (System.IO.File.Exists(filePath))                                           // Überprüfen, ob die Datei existiert.
                    {
                        Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(filePath); // Extrahiert das Icon der Datei.
                        BitmapImage bitmapImage = ConvertIconToImageSource(icon);        // Konvertiert das Icon in ein BitmapImage.
                        files.Add(new FileIconInfo
                        {
                            Path = filePath,                                             // Setzt den Pfad der Datei.
                            Icon = bitmapImage                                           // Setzt das Icon der Datei.
                        });
                    }
                }
                LastSavedFiles = files;                                                  // Speichern der Liste in der statischen Eigenschaft.
                return files;                                                            // Gibt die Liste der Dateien zurück.
            }
            else
            {
                return null;                                                             // Gibt null zurück, wenn keine Icons gespeichert sind.
            }
        }
        //------------------------------------------------------------------------------------------------------------------------Konvertieren des Icons in ein BitmapImage-----------------------------------------------------------------------------------------------------------------------------------------------------------
      //  public static List<FileIconInfo> StatusIcons { get; private set; }            // Statische Eigenschaft für die zuletzt gespeicherten Dateien.
      //
      //  public static List<FileIconInfo> GetStatusIcon()                             // Methode zum Abrufen der gespeicherten Icons.
      //  {
      //      string pathDO = Properties.Settings.Default.pfadDeskOK + "\\DesktopOK.exe";          // Holt die gespeicherten Daten.
      //      string pathMM = Properties.Settings.Default.pfadDeskOK + "\\MultiMonitorTool.exe";               // Holt den ausgewählten Monitor.
      //      if (System.IO.File.Exists(pathDO))
      //      {
      //          Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(pathDO); // Extrahiert das Icon der Datei.
      //          BitmapImage bitmapImage = ConvertIconToImageSource(icon);        // Konvertiert das Icon in ein BitmapImage.
      //      }
      //      if (System.IO.File.Exists(pathMM)) 
      //      {
      //          Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(pathDO); // Extrahiert das Icon der Datei.
      //          BitmapImage bitmapImage = ConvertIconToImageSource(icon);        // Konvertiert das Icon in ein BitmapImage.
      //      }
      //
      //  }

        //------------------------------------------------------------------------------------------------------------------------Konvertieren des Icons in ein BitmapImage-----------------------------------------------------------------------------------------------------------------------------------------------------------
        public static BitmapImage ConvertIconToImageSource(Icon icon)                   // Methode zum Konvertieren eines Icons in ein BitmapImage.
        {
            using (MemoryStream ms = new MemoryStream())
            {
                icon.ToBitmap().Save(ms, ImageFormat.Png);                               // Speichert das Icon als PNG in den MemoryStream.
                ms.Position = 0;                                                         // Setzt die Position des MemoryStream auf 0.
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;                                           // Setzt die Quelle des BitmapImage auf den MemoryStream.
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;                      // Setzt die Cache-Option auf OnLoad.
                bitmapImage.EndInit();
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
