using MIM_Tool.Helpers;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace MIM_Tool.Funktions
{
    class FunktionDeskScen
    {
        //-------------------------------------------------------------Speichern der Monitor Infos-----------------------------------------------------------------------------------------------------------
        public void MonitorSaveData()                                                           // Methode zum Speichern von Monitor-Daten mit MultiMonitorTool.
        {
            Log.inf("Starten des Speichern der MonitorDaten mit MultiMonitorTool, setten der notwendigen Variablen");
            string pathFolder = Properties.Settings.Default.pfadDeskOK;                         // Pfad zum Verzeichnis.
            string pathExe = $"{Properties.Settings.Default.pfadDeskOK}\\MultiMonitorTool.exe"; // Pfad zur MultiMonitorTool.exe.
            string pathData = $"{Properties.Settings.Default.pfadDeskOK}\\MonitorDaten.txt";    // Pfad zur MonitorDaten.txt.

            if (File.Exists(pathData)) File.Delete(pathData);                                                          // Löscht die Datei, wenn sie existiert.
            Log.inf("Löschen der bisherigen Speicherdatei");

            if (Properties.Settings.Default.eMultiMonSaveDataReady && string.IsNullOrEmpty(Properties.Settings.Default.InfoMonitor1))
            {
                Log.inf("Was keine Daten gespeichert und KonfigDatei wurde erfolgreich gespeichert ");
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();                        // Prozess für das Öffnen des Dokuments starten.
                    startInfo.FileName = pathExe;                                               // Setzt den Dateinamen.
                    startInfo.Arguments = $"/stext {pathData}";                                 // Setzt die Argumente.
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;                          // Setzt den Fensterstil auf versteckt.
                    Log.inf("Setzen der programmparameter und MultiMonitorTool ausführen ");
                    Process process = Process.Start(startInfo);                                 // Startet den Prozess.
                    process.WaitForExit();                                                      // Wartet, bis der Prozess beendet ist.
                    Log.inf("Gewartet bis MultiMonitorTool fertig ist ");
                    if (System.IO.File.Exists(pathData))
                    {
                        Log.inf("Speicher der Monitor erfolgreich Status wird aktualisiert ");
                        // Pfad der zuletzt erstellten Datei speichern.
                        Properties.Settings.Default.eMultiMonSaveDataDone = true;
                        Properties.Settings.Default.eMultiMonSaveDataReady = false;
                        Properties.Settings.Default.eMultiMonSaveDataDate = Convert.ToString(DateTime.Now);
                        Properties.Settings.Default.Save();                                     // Speichert die Einstellungen.
                        Log.inf("Speichern der Daten erfolgreich abgeschlossen");
                    }
                    else
                    {
                        Log.inf($"Die SpeicherDatei: {pathData} \nder Monitordaten wurde nicht gefunden"); // Zeigt eine Fehlermeldung an.
                        if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;       // Setzt den DatenLesen Ablauf auf true.
                        Properties.Settings.Default.Save();
                    }
                }
                catch (Exception ex)
                {
                    Log.err("Fehler beim Speichern der MonitorDateien über MultiMonitorTool",ex,true);         // Zeigt eine Fehlermeldung an.
                    if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;       // Setzt den DatenLesen Ablauf auf true.
                    Properties.Settings.Default.Save();
                }
                
                Thread.Sleep(1000);                                                             // Wartet eine Sekunde.
            }
            else if (Properties.Settings.Default.eMultiMonSaveDataReady && !string.IsNullOrEmpty(Properties.Settings.Default.InfoMonitor1))
            {
                Log.inf("Monitordaten Wurdan schon gespeichert Abfrage ob überschreiben ");
                var result = MessageBox.Show("Möchten Sie Wirklich alle Monitor Daten Neu Einlesen", "Neu Lesen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);    // Benutzer fragen, ob die Daten gespeichert werden sollen.
                if (result == DialogResult.Yes)                                                 // Überprüfen der Benutzerantwort.
                {
                    try
                    {
                        Log.inf("Das Überschreiben der Daten wurde akzeptiert ");
                        ProcessStartInfo startInfo = new ProcessStartInfo();                    // Prozess für das Öffnen des Dokuments starten.
                        startInfo.FileName = pathExe;                                           // Setzt den Dateinamen.
                        startInfo.Arguments = $"/stext {pathData}";                             // Setzt die Argumente.
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;                      // Setzt den Fensterstil auf versteckt.
                        Log.inf("Setzen der programmparameter und MultiMonitorTool ausführen ");
                        Process process = Process.Start(startInfo);                             // Startet den Prozess.
                        process.WaitForExit();                                                  // Wartet, bis der Prozess beendet ist.
                        Log.inf("Gewartet bis MultiMonitorTool fertig ist ");
                        if (System.IO.File.Exists(pathData))
                        {
                            Log.inf("Speichern war erfolgreich, deswegen startest du anpassen ");
                            // Pfad der zuletzt erstellten Datei speichern.
                            Properties.Settings.Default.eMultiMonSaveDataDone = true;
                            Properties.Settings.Default.eMultiMonSaveDataReady = false;
                            Properties.Settings.Default.eMultiMonSaveDataDate = Convert.ToString(DateTime.Now);
                            Properties.Settings.Default.Save();                                 // Speichert die Einstellungen.
                            Log.inf("Das Speichern der Monitordaten war erfolgreich.");
                        }
                        else
                        {
                            Log.err($"Die SpeicherDatei: {pathData} \nder Monitordaten wurde nicht neu überspeichert",null,true);                           // Zeigt eine Fehlermeldung an.
                            if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;       // Setzt den DatenLesen Ablauf auf true.
                            Properties.Settings.Default.Save();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.err("Beim erneuten Speichern mit Multi Monitor Tool ist etwas schief gelaufen ",ex,true);       // Zeigt eine Fehlermeldung an.
                        if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;       // Setzt den DatenLesen Ablauf auf true.
                        Properties.Settings.Default.Save();
                    }

                    Thread.Sleep(1000);                                                         // Wartet eine Sekunde.
                }
                else
                {
                    Log.war("Das Neuladen Monitordaten abgebrochen ");//2s                              // Benachrichtigung, dass das Speichern abgebrochen wurde.
                    if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;       // Setzt den DatenLesen Ablauf auf true.
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                Log.err("das Speichern der Monitor konfig ist noch nicht fertig oder es ist da ein Fehler unterlaufen ", null, true);                                 // Zeigt eine Fehlermeldung an, wenn die Bedingungen nicht erfüllt sind.
                if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;       // Setzt den DatenLesen Ablauf auf true.
                Properties.Settings.Default.Save();
            }
        }
        //-------------------------------------------------------------Lesen der Monitor Infos-----------------------------------------------------------------------------------------------------------
        public string[] DataRead(string pathData)                                               // Methode zum Lesen und Vergleichen von Positionen mit DesktopOK.
        {
            Log.inf("Start lesen der Daten der Monitore");
            if (Properties.Settings.Default.eMultiMonSaveDataDone)
            {
                try
                {
                    if (File.Exists(pathData))
                    {
                        Log.inf("Bedingungen zum Lesen der Daten erfüllt, Daten werden gelesen und zwischengespeichert ");
                        string[] zeilen = File.ReadAllLines(pathData);                          // Lesen aller Zeilen der Datei in ein Array.
                        string multiMonData = string.Join(";", zeilen);                         // Verbindet die Zeilen zu einem String.
                        Properties.Settings.Default.MultiMonData = multiMonData;                // Speichert die Daten in den Einstellungen.
                        Properties.Settings.Default.Save();                                     // Speichert die Einstellungen.
                        Log.inf("Das Lesen der Monitordaten war erfolgreich ");
                        return zeilen;                                                          // Gibt die Zeilen zurück.
                    }
                    else
                    {
                        Log.err($"Datei: {pathData} nicht gefunden.",null,true);                               // Zeigt eine Fehlermeldung an.
                        if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;       // Setzt den DatenLesen Ablauf auf true.
                        Properties.Settings.Default.Save();
                        return new string[0];                                                   // Gibt ein leeres Array zurück.
                    }
                }
                catch (Exception ex)
                {
                    Log.err($"Beim Lesen der Monitor Daten ist ein Fehler aufgetreten",ex,true);               // Zeigt eine Fehlermeldung an.
                    if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;       // Setzt den DatenLesen Ablauf auf true.
                    Properties.Settings.Default.Save();
                    return new string[0];                                                       // Gibt ein leeres Array zurück.
                }
            }
            else
            {
                Log.err("Das speichern der Monitor Dateien von MultiMonitorTool ist nicht abgeschlossen ");//ex,alarm                                   // Zeigt eine Fehlermeldung an, wenn die Bedingungen nicht erfüllt sind.
                if (Properties.Settings.Default.DatenLesenAktiv) Properties.Settings.Default.DatenLesenFehler = true;       // Setzt den DatenLesen Ablauf auf true.
                Properties.Settings.Default.Save();
                return new string[0];                                                           // Gibt ein leeres Array zurück.
            }
        }
    }
}