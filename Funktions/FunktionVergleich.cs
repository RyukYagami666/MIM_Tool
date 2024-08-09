using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;

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

            foreach (var path in adjustedFilePaths)
            {
                string directoryPath = GetDesktopPath();
                string fullPath = Path.Combine(directoryPath, path);
                if (File.Exists(fullPath)) { }
                else
                {
                    fullPath = fullPath.Replace($".lnk", "");
                    if (System.IO.Directory.Exists(fullPath)) { }
                    else
                    {
                        MessageBox.Show($"Der Pfad {fullPath} Nicht, ALARM ALARM.");
                    }
                }
                fullPaths.Add(fullPath); // Füge den vollständigen Pfad zur Liste hinzu, wenn es kein Verzeichnis ist
            }

            string deskOkDataTrim = string.Join(";", fullPaths);
            Properties.Settings.Default.DeskOkDataTrim = deskOkDataTrim;
            Properties.Settings.Default.Save();
        }


        public static T[] TrimIconPos<T>(T[] ursprungsArray)
        {
            List<T> neueListe = new List<T>();

            for (int i = 0; i < ursprungsArray.Length; i++)
            {
                if (i % 2 == 0 && i != 0)
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

        public void MultiMonDataTrim()
        {
            string multiMonData = Properties.Settings.Default.MultiMonData;
            multiMonData = multiMonData.Replace($": ;", ": - ;");
            multiMonData = multiMonData.Replace($"==================================================;", "");
            multiMonData = multiMonData.Replace($"==================================================;;", ";");
            multiMonData = multiMonData.Replace($";;==================================================", ";");
            multiMonData = multiMonData.Replace($";==================================================;", "");
            multiMonData = Regex.Replace(multiMonData, @"\s*:\s*", ":");

            string[] splitContent = multiMonData.Split(new string[] { ";;" }, StringSplitOptions.None);

            if (splitContent.Length == 1)
            {
                Properties.Settings.Default.MultiMonDataTrim1 = splitContent[0];
            }
            else if (splitContent.Length == 2)
            {
                Properties.Settings.Default.MultiMonDataTrim1 = splitContent[0];
                Properties.Settings.Default.MultiMonDataTrim2 = splitContent[1];
            }
            else if (splitContent.Length == 3)
            {
                Properties.Settings.Default.MultiMonDataTrim1 = splitContent[0];
                Properties.Settings.Default.MultiMonDataTrim2 = splitContent[1];
                Properties.Settings.Default.MultiMonDataTrim3 = splitContent[2];
            }
            else if (splitContent.Length == 4)
            {
                Properties.Settings.Default.MultiMonDataTrim1 = splitContent[0];
                Properties.Settings.Default.MultiMonDataTrim2 = splitContent[1];
                Properties.Settings.Default.MultiMonDataTrim3 = splitContent[2];
                Properties.Settings.Default.MultiMonDataTrim4 = splitContent[3];
            }
            else
            {
                MessageBox.Show("Zu viele Daten gefunden.");
            }

            Properties.Settings.Default.Save();
            MultiMonDataNeuOrdnen();
        }

        public void MultiMonDataNeuOrdnen()
        {
            string[] multiMonDataTrims = {
                Properties.Settings.Default.MultiMonDataTrim1,
                Properties.Settings.Default.MultiMonDataTrim2,
                Properties.Settings.Default.MultiMonDataTrim3,
                Properties.Settings.Default.MultiMonDataTrim4
            };

            for (int i = 0; i < multiMonDataTrims.Length; i++)
            {
                if (!string.IsNullOrEmpty(multiMonDataTrims[i]))
                {
                    OrdneMonitorDatenNeu(multiMonDataTrims[i]);
                }
            }
        }

        private void OrdneMonitorDatenNeu(string stringMultiMon)
        {
            string[] MultiMonDataArray = stringMultiMon.Split(';');
            string[] esSeMonitor =
            {
                MultiMonDataArray[18],
                MultiMonDataArray[10],
                MultiMonDataArray[0],
                MultiMonDataArray[1],
                MultiMonDataArray[3],
                MultiMonDataArray[5],
                MultiMonDataArray[7],
                MultiMonDataArray[9],
                MultiMonDataArray[15],
                MultiMonDataArray[19]
            };

            string infoMonitor = string.Join(";", esSeMonitor);
            infoMonitor = infoMonitor.Replace(":",";");
            infoMonitor = infoMonitor.Replace(";Name;", ";Monitor Nummer;");
            infoMonitor = infoMonitor.Replace(";Left-Top;", ";Position;");

            if (MultiMonDataArray[10] == "Name:\\\\.\\DISPLAY1")
            {
                if (MultiMonDataArray[3] == "Active:Yes") { Properties.Settings.Default.eMonitorAktiv1 = true; }
                else { Properties.Settings.Default.eMonitorAktiv1 = false; }
                Properties.Settings.Default.InfoMonitor1 = infoMonitor;
            }
            else if (MultiMonDataArray[10] == "Name:\\\\.\\DISPLAY2")
            {
                if (MultiMonDataArray[3] == "Active:Yes"){Properties.Settings.Default.eMonitorAktiv2 = true;}
                else{Properties.Settings.Default.eMonitorAktiv2 = false;}
                Properties.Settings.Default.InfoMonitor2 = infoMonitor;
            }
            else if (MultiMonDataArray[10] == "Name:\\\\.\\DISPLAY3")
            {
                if (MultiMonDataArray[3] == "Active:Yes") { Properties.Settings.Default.eMonitorAktiv3 = true; }
                else { Properties.Settings.Default.eMonitorAktiv3 = false; }
                Properties.Settings.Default.InfoMonitor3 = infoMonitor;
            }
            else if (MultiMonDataArray[10] == "Name:\\\\.\\DISPLAY4")
            {
                if (MultiMonDataArray[3] == "Active:Yes") { Properties.Settings.Default.eMonitorAktiv4 = true; }
                else { Properties.Settings.Default.eMonitorAktiv4 = false; }
                Properties.Settings.Default.InfoMonitor4 = infoMonitor;
            }
            else
            {
                MessageBox.Show("Monitor Nummer nicht gefunden.");
            }
            Properties.Settings.Default.Save();

        }
    }
}
