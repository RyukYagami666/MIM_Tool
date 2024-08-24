using System.IO; 
using System.Windows; 
using System.Text.RegularExpressions;
using MIM_Tool.Helpers;

namespace MIM_Tool.Funktions
{
    internal class FunktionVergleich
    {
        public static string GetDesktopPath()                                                                     // Methode zum Abrufen des Desktop-Pfads.
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);                                  // Gibt den Pfad zum Desktop-Ordner zurück.
        }

        public void AbwandelnDerData()                                                                            // Methode zum Anpassen der Daten.
        {
            Log.inf("AbwandelnDerData gestartet.");
            string deskOkData = Properties.Settings.Default.DeskOkData;                                           // Holt die DeskOk-Daten aus den Einstellungen.
            Log.inf("DeskOkData aus den Einstellungen geladen.");
            string[] deskOkDataArray = deskOkData.Split(';');                                                     // Teilt die Daten in ein Array auf.
            string[] trimmedArray = TrimIconPos(deskOkDataArray);                                                 // Trimmt die Icon-Positionen.
            Log.inf("DeskOkData in Array aufgeteilt und getrimmt.");
            List<string> trimmedList = new List<string>(trimmedArray);                                            // Konvertiert das Array in eine Liste.
            List<string> adjustedFilePaths = AdjustFilePaths(trimmedList);                                        // Passt die Dateipfade an.
            Log.inf("Getrimmtes Array in Liste konvertiert und Dateipfade angepasst.");
            List<string> fullPaths = new List<string>();                                                          // Liste für die vollständigen Pfade.

            foreach (var path in adjustedFilePaths)
            {
                Log.inf("Verarbeite Dateipfad: " + path);
                string directoryPath = GetDesktopPath();                                                          // Holt den Desktop-Pfad.
                string fullPath = Path.Combine(directoryPath, path);                                              // Kombiniert den Desktop-Pfad mit dem Dateipfad.
                if (File.Exists(fullPath))
                {
                    Log.inf($"Datei existiert: {fullPath}");
                }                                                                                                 // Überprüft, ob die Datei existiert.
                else
                {
                    Log.inf("Datei existiert nicht, überprüfe Verzeichnis: " + fullPath);
                    fullPath = fullPath.Replace($".lnk", "");                                                     // Entfernt die Dateiendung .lnk.
                    if (System.IO.Directory.Exists(fullPath)) { Log.inf($"Verzeichnis existiert: {fullPath}"); }  // Überprüft, ob das Verzeichnis existiert.
                    else
                    {
                        Log.err($"Der Pfad {fullPath} existiert nicht.", null, true);                             // Zeigt eine Fehlermeldung an, wenn der Pfad nicht existiert.
                    }
                }
                Log.inf("Füge vollständigen Pfad zur Liste hinzu: " + fullPath);
                fullPaths.Add(fullPath);                                                                          // Fügt den vollständigen Pfad zur Liste hinzu.
            }

            Log.inf("Erstelle getrimmte DeskOk-Daten.");
            string deskOkDataTrim = string.Join(";", fullPaths);                                                  // Verbindet die vollständigen Pfade zu einem String.
            Properties.Settings.Default.DeskOkDataTrim = deskOkDataTrim;                                          // Speichert die getrimmten Daten in den Einstellungen.
            Properties.Settings.Default.Save();
            Log.inf("Getrimmte Daten in den Einstellungen gespeichert und Einstellungen gespeichert.");           // Speichert die Einstellungen.
        }

        public static T[] TrimIconPos<T>(T[] ursprungsArray)                                                      // Methode zum Trimmen der Icon-Positionen.
        {
            Log.inf("TrimIconPos gestartet.");
            List<T> neueListe = new List<T>();                                                                    // Neue Liste für die getrimmten Positionen.
            for (int i = 0; i < ursprungsArray.Length; i++)
            {
                if (i % 2 == 0 && i != 0)                                                                         // Fügt jedes zweite Element hinzu, außer das erste.
                {
                    neueListe.Add(ursprungsArray[i]);
                }
            }
            Log.inf($"Getrimmtes Array Länge: {neueListe.Count}");
            Log.inf("TrimIconPos abgeschlossen.");
            return neueListe.ToArray();                                                                      // Gibt die getrimmte Liste als Array zurück.
        }

        public static List<string> AdjustFilePaths(List<string> neueListe)                                   // Methode zum Anpassen der Dateipfade.
        {
            Log.inf("AdjustFilePaths gestartet.");
            string directoryPath = GetDesktopPath();                                                         // Holt den Desktop-Pfad.
            List<string> adjustedFilePaths = new List<string>();                                             // Liste für die angepassten Dateipfade.
            foreach (var filePath in neueListe)
            {
                Log.inf($"Verarbeite Dateipfad: {filePath}");
                string adjustedFilePath = filePath;
                if (string.IsNullOrEmpty(Path.GetExtension(filePath)))                                       // Überprüft, ob die Dateiendung fehlt.
                {
                    adjustedFilePath += ".lnk";                                                              // Fügt die Dateiendung .lnk hinzu.
                }
                Log.inf($"Angepasster Dateipfad: {adjustedFilePath}");
                adjustedFilePaths.Add(adjustedFilePath);                                                     // Fügt den angepassten Dateipfad zur Liste hinzu.
            }
            Log.inf($"Angepasste Liste Länge: {adjustedFilePaths.Count}");
            Log.inf("AdjustFilePaths abgeschlossen.");
            return adjustedFilePaths;                                                                        // Gibt die Liste der angepassten Dateipfade zurück.
        }

        public void MultiMonDataTrim()                                                                       // Methode zum Trimmen der MultiMon-Daten.
        {
            Log.inf("MultiMonDataTrim gestartet.");
            string multiMonData = Properties.Settings.Default.MultiMonData;                                  // Holt die MultiMon-Daten aus den Einstellungen.
            Log.inf("MultiMonData aus den Einstellungen geladen.");
            multiMonData = multiMonData.Replace($": ;", ": - ;");                                            // Ersetzt ": ;" durch ": - ;".
            multiMonData = multiMonData.Replace($"==================================================;", ""); // Entfernt bestimmte Zeichenfolgen.
            multiMonData = multiMonData.Replace($"==================================================;;", ";");
            multiMonData = multiMonData.Replace($";;==================================================", ";");
            multiMonData = multiMonData.Replace($";==================================================;", "");
            multiMonData = Regex.Replace(multiMonData, @"\s*:\s*", ":");                                     // Entfernt Leerzeichen um Doppelpunkte.
            Log.inf("MultiMonData ersetzt und bereinigt.");

            string[] splitContent = multiMonData.Split(new string[] { ";;" }, StringSplitOptions.None);      // Teilt die Daten in ein Array auf.
            Log.inf($"MultiMonData in {splitContent.Length} Teile aufgeteilt.");

            if (splitContent.Length == 1)
            {
                Properties.Settings.Default.MultiMonDataTrim1 = splitContent[0];                             // Speichert die getrimmten Daten in den Einstellungen.
                Properties.Settings.Default.MultiMonDataTrim2 = "";
                Properties.Settings.Default.MultiMonDataTrim3 = "";
                Properties.Settings.Default.MultiMonDataTrim4 = "";
            }
            else if (splitContent.Length == 2)
            {
                Properties.Settings.Default.MultiMonDataTrim1 = splitContent[0];
                Properties.Settings.Default.MultiMonDataTrim2 = splitContent[1];
                Properties.Settings.Default.MultiMonDataTrim3 = "";
                Properties.Settings.Default.MultiMonDataTrim4 = "";
            }
            else if (splitContent.Length == 3)
            {
                Properties.Settings.Default.MultiMonDataTrim1 = splitContent[0];
                Properties.Settings.Default.MultiMonDataTrim2 = splitContent[1];
                Properties.Settings.Default.MultiMonDataTrim3 = splitContent[2];
                Properties.Settings.Default.MultiMonDataTrim4 = "";
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
                Log.err("Zu viele Daten gefunden.", null, true);                                                                 // Zeigt eine Fehlermeldung an, wenn zu viele Daten gefunden wurden.
                if (Properties.Settings.Default.InizialisierungAktiv) Properties.Settings.Default.InizialisierungsFehler = true; // Setzt den Initialisierungsfehler auf true.
                Properties.Settings.Default.Save();                                                                              // Speichert die Einstellungen. 
            }

            Log.inf("Einstellungen gespeichert.");
            Properties.Settings.Default.Save();                                                                                  // Speichert die Einstellungen.
            MultiMonDataNeuOrdnen();                                                                                             // Ruft die Methode zum Neuordnen der Daten auf.
        }

        public void MultiMonDataNeuOrdnen()                                                                                      // Methode zum Neuordnen der MultiMon-Daten.
        {
            Log.inf("MultiMonDataNeuOrdnen gestartet.");
            string[] multiMonDataTrims = {
                Properties.Settings.Default.MultiMonDataTrim1,
                Properties.Settings.Default.MultiMonDataTrim2,
                Properties.Settings.Default.MultiMonDataTrim3,
                Properties.Settings.Default.MultiMonDataTrim4
            };
            for (int i = 0; i < multiMonDataTrims.Length; i++)
            {
                if (!string.IsNullOrEmpty(multiMonDataTrims[i]))                                                                 // Überprüft, ob die Daten nicht leer sind.
                {
                    Log.inf($"Ordne Monitor-Daten neu für Index {i}.");
                    OrdneMonitorDatenNeu(multiMonDataTrims[i], i);                                                               // Ruft die Methode zum Neuordnen der Monitor-Daten auf.
                }
            }
            Log.inf("MultiMonDataNeuOrdnen abgeschlossen.");
        }

        private void OrdneMonitorDatenNeu(string stringMultiMon, int index)                                                      // Methode zum Neuordnen der Monitor-Daten.
        {
            Log.inf($"OrdneMonitorDatenNeu gestartet für Index {index}.");
            string[] MultiMonDataArray = stringMultiMon.Split(';');                                                              // Teilt die Daten in ein Array auf.
            Log.inf($"MultiMonDataArray Länge: {MultiMonDataArray.Length}");

            string[] esSeMonitor = {
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

            string infoMonitor = string.Join(";", esSeMonitor);                                                                                // Verbindet die Daten zu einem String.
            infoMonitor = infoMonitor.Replace(":", ";");                                                                                       // Ersetzt Doppelpunkte durch Semikolons.
            infoMonitor = infoMonitor.Replace(";Name;", ";Monitor Nummer;");                                                                   // Ersetzt "Name" durch "Monitor Nummer".
            infoMonitor = infoMonitor.Replace(";Left-Top;", ";Position;");                                                                     // Ersetzt "Left-Top" durch "Position".

            Log.inf($"infoMonitor: {infoMonitor}");
            if (index == 0)
            {
            if (MultiMonDataArray[3] == "Active:Yes"){ Properties.Settings.Default.eMonitorAktiv1 = true; Log.inf("Monitor 1 ist aktiv."); }   // Setzt den Status des Monitors auf aktiv.
            else{ Properties.Settings.Default.eMonitorAktiv1 = false; Log.inf("Monitor 1 ist inaktiv."); }                                     // Setzt den Status des Monitors auf inaktiv.
            Properties.Settings.Default.InfoMonitor1 = infoMonitor;                                                                            // Speichert die Monitor-Informationen.
            Properties.Settings.Default.eMonitorVorhanden1 = true;                                                                             // Setzt den Status des Monitors auf vorhanden.
            }
            else if (index == 1)
            {
            if (MultiMonDataArray[3] == "Active:Yes"){ Properties.Settings.Default.eMonitorAktiv2 = true; Log.inf("Monitor 2 ist aktiv.");}    // Setzt den Status des Monitors auf aktiv.
            else{ Properties.Settings.Default.eMonitorAktiv2 = false; Log.inf("Monitor 2 ist inaktiv.");}                                      // Setzt den Status des Monitors auf inaktiv.
            Properties.Settings.Default.InfoMonitor2 = infoMonitor;                                                                            // Speichert die Monitor-Informationen.
            Properties.Settings.Default.eMonitorVorhanden2 = true;                                                                             // Setzt den Status des Monitors auf vorhanden.
            }
            else if (index == 2)
            {
            if (MultiMonDataArray[3] == "Active:Yes") {Properties.Settings.Default.eMonitorAktiv3 = true; Log.inf("Monitor 3 ist aktiv."); }   // Setzt den Status des Monitors auf aktiv.
            else{ Properties.Settings.Default.eMonitorAktiv3 = false; Log.inf("Monitor 3 ist inaktiv.");}                                      // Setzt den Status des Monitors auf inaktiv.
            Properties.Settings.Default.InfoMonitor3 = infoMonitor;                                                                            // Speichert die Monitor-Informationen.
            Properties.Settings.Default.eMonitorVorhanden3 = true;                                                                             // Setzt den Status des Monitors auf vorhanden.
            }
            else if (index == 3)
            {
            if (MultiMonDataArray[3] == "Active:Yes") {Properties.Settings.Default.eMonitorAktiv4 = true; Log.inf("Monitor 4 ist aktiv."); }   // Setzt den Status des Monitors auf aktiv.
            else{ Properties.Settings.Default.eMonitorAktiv4 = false; Log.inf("Monitor 4 ist inaktiv.");}                                      // Setzt den Status des Monitors auf inaktiv.
            Properties.Settings.Default.InfoMonitor4 = infoMonitor;                                                                            // Speichert die Monitor-Informationen.
            Properties.Settings.Default.eMonitorVorhanden4 = true;                                                                             // Setzt den Status des Monitors auf vorhanden.
            }
            else
            {
                Log.err("Monitor Nummer nicht gefunden.", null, true);                                                                         // Zeigt eine Fehlermeldung an, wenn der Monitor nicht gefunden wurde.
            }
            Properties.Settings.Default.Save();                                                                                                // Speichert die Einstellungen.
            Log.inf("Einstellungen gespeichert.");
            Log.inf("OrdneMonitorDatenNeu abgeschlossen.");
        }
    }
}
