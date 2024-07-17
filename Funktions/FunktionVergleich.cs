using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace App3.Funktions
{
    internal class FunktionVergleich
    {
        public static string GetDesktopPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
        public void AbwandelnDerData()
        {
            string deskOkData = Properties.Settings.Default.DeskOkData;
            string[] deskOkDataArray = deskOkData.Split(';');

            // TrimIconPos aufrufen und das Ergebnis in eine Liste umwandeln
            string[] trimmedArray = TrimIconPos(deskOkDataArray);
            List<string> trimmedList = new List<string>(trimmedArray);

            // AdjustFilePaths aufrufen, um die Dateiendungen zu überprüfen und anzupassen
            List<string> adjustedFilePaths = AdjustFilePaths(trimmedList);

            List<string> fullPaths = new List<string>(); // Liste für die vollständigen Pfade

            // Hier können Sie mit den angepassten Dateipfaden weiterarbeiten
            foreach (var path in adjustedFilePaths)
            {
                string directoryPath = GetDesktopPath();
                string fullPath = Path.Combine(directoryPath, path);
                if (File.Exists(fullPath)){}
                else
                {
                    fullPath = fullPath.Replace($".lnk", "");
                    if (System.IO.Directory.Exists(fullPath)){}
                    else
                    {
                        MessageBox.Show($"Der Pfad {fullPath} Nicht, ALARM ALARM.");
                    }
                }
                    fullPaths.Add(fullPath); // Füge den vollständigen Pfad zur Liste hinzu, wenn es kein Verzeichnis ist
            }

            // Überprüfen, ob die Liste mindestens einen Eintrag enthält, bevor der erste Eintrag entfernt wird
            if (fullPaths.Count > 0)
            {
                fullPaths.RemoveAt(0); // Entfernt den ersten Eintrag aus der Liste
            }

            string deskOkDataTrim = string.Join(";", fullPaths);
            Properties.Settings.Default.DeskOkDataTrim = deskOkDataTrim;
            Properties.Settings.Default.Save();
            int i = 0;
            StringBuilder sb = new StringBuilder();
            foreach (var fullPath in fullPaths) // Iteriere über die Liste der vollständigen Pfade
            {
                sb.AppendLine(fullPath);
            }
            MessageBox.Show(sb.ToString(), "Dateipfade", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public static T[] TrimIconPos<T>(T[] ursprungsArray)
        {
            List<T> neueListe = new List<T>();

            for (int i = 0; i < ursprungsArray.Length; i++)
            {
                if (i % 2 == 0)
                {
                    neueListe.Add(ursprungsArray[i]);
                }
            }
            return neueListe.ToArray();
        }

        public static List<string> AdjustFilePaths(List<string> neueListe)
        {
            string directoryPath = GetDesktopPath();

            //string[] filePaths = Directory.GetFiles(directoryPath, "*");
            List<string> adjustedFilePaths = new List<string>();

            foreach (var filePath in neueListe)
            {
                string adjustedFilePath = filePath;

                // Überprüfen, ob die Datei eine Erweiterung hat
                if (string.IsNullOrEmpty(Path.GetExtension(filePath)))
                {
                    // Falls nicht, .lnk hinzufügen
                    adjustedFilePath += ".lnk";
                }

                adjustedFilePaths.Add(adjustedFilePath);
            }

            return adjustedFilePaths;
        }

        
    }
}
