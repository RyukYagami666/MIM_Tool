using Microsoft.WindowsAPICodePack.Dialogs;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System;
using Microsoft.VisualBasic;
using MIM_Tool.Services;

namespace MIM_Tool.Funktions
{
    internal class FunktionIconListe
    {
        public static List<FileIconInfo> LastExecutedFiles { get; private set; }

        public static string GetDesktopPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
        public static List<FileIconInfo> Execute()
        {
           // string directoryPath = GetDesktopPath();
            //MessageBox.Show("Funktion1 wird ausgeführt."); // MessageBox hinzugefügt
            string deskOkDataTrim = Properties.Settings.Default.DeskOkDataTrim;
            string[] deskOkData = deskOkDataTrim.Split(';');
            List<string> deskOkNames = deskOkData.ToList();

            List<FileIconInfo> files = new List<FileIconInfo>();
            foreach (var filePath in deskOkNames)
            {
               
                if (File.Exists(filePath)) // Überprüfen, ob die Datei existiert
                {
                    Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(filePath);
                    BitmapImage bitmapImage = ConvertIconToImageSource(icon);
                    files.Add(new FileIconInfo
                    {
                        Path = filePath,
                        Icon = bitmapImage
                    });
                }
            }
            //files = SortFilesBasedOnDeskOkData(files);
            //StringBuilder sb = new StringBuilder();
            //foreach (var file in files)
            //{
            //    sb.AppendLine(file.Path);
            //}
            //MessageBox.Show(sb.ToString(), "Dateipfade", MessageBoxButton.OK, MessageBoxImage.Information);
            LastExecutedFiles = files; // Speichern der Liste in der statischen Eigenschaft
            ISPSaveState.IsReady = true;
            return files;
        }


        public static List<FileIconInfo> LastSavedFiles { get; private set; }

        public static List<FileIconInfo> gespeicherteIcons()
        {

            int iSelMonitor = Properties.Settings.Default.SelectetMonitor;

            string[] gespeicherteIconListen =
            {
            Properties.Settings.Default.eMonitorIconsZugewiesen1,
            Properties.Settings.Default.eMonitorIconsZugewiesen2,
            Properties.Settings.Default.eMonitorIconsZugewiesen3,
            Properties.Settings.Default.eMonitorIconsZugewiesen4
            };
        
            if (iSelMonitor < 4 && !string.IsNullOrEmpty(gespeicherteIconListen[iSelMonitor]))
            { 
                string gespeicherteIconListe = gespeicherteIconListen[iSelMonitor];
                string[] savedListAuswawl = gespeicherteIconListe.Split(';');
                List<string> savedListAuswawlIcons = savedListAuswawl.ToList();

                List<FileIconInfo> files = new List<FileIconInfo>();
                foreach (var filePath in savedListAuswawlIcons)
                {
                    if (File.Exists(filePath)) // Überprüfen, ob die Datei existiert
                    {
                        Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(filePath);
                        BitmapImage bitmapImage = ConvertIconToImageSource(icon);
                        files.Add(new FileIconInfo
                        {
                            Path = filePath,
                            Icon = bitmapImage
                        });
                    }
                }

                LastSavedFiles = files; // Speichern der Liste in der statischen Eigenschaft
                return files;
            }
            else
            {

                return null;
            }
        }

        private static BitmapImage ConvertIconToImageSource(Icon icon)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                icon.ToBitmap().Save(ms, ImageFormat.Png);
                ms.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        // Definieren Sie zusätzliche Klassen außerhalb der DataPage-Klasse.
        public class FileIconInfo
        {
            public string Path { get; set; }
            public BitmapImage Icon { get; set; }
        }
    }

}
