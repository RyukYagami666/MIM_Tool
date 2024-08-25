using System.Windows;
using System.Threading;
using System.Media;
using MIM_Tool.Helpers;

namespace MIM_Tool.Funktions
{
    internal class Funktion3MonitorKontrolle
    {
        public void MonitorKontrolle(string monitorID)
        {
            Log.inf("Start des automatischen Prozesses zum Steuern der Monitore und dessen Icons.");
            bool[] vorhanden =
            {
                 Properties.Settings.Default.eMonitorVorhanden1,      // Überprüft, ob Monitor 1 vorhanden ist
                 Properties.Settings.Default.eMonitorVorhanden2,      // Überprüft, ob Monitor 2 vorhanden ist
                 Properties.Settings.Default.eMonitorVorhanden3,      // Überprüft, ob Monitor 3 vorhanden ist
                 Properties.Settings.Default.eMonitorVorhanden4       // Überprüft, ob Monitor 4 vorhanden ist
            };
            bool[] aktiv =
            {
                 Properties.Settings.Default.eMonitorAktiv1,          // Überprüft, ob Monitor 1 aktiv ist
                 Properties.Settings.Default.eMonitorAktiv2,          // Überprüft, ob Monitor 2 aktiv ist
                 Properties.Settings.Default.eMonitorAktiv3,          // Überprüft, ob Monitor 3 aktiv ist
                 Properties.Settings.Default.eMonitorAktiv4           // Überprüft, ob Monitor 4 aktiv ist
            };

            string infoMonitor1 = Properties.Settings.Default.InfoMonitor1;
            string[] monitorData1 = infoMonitor1.Split(';');          // Daten von Monitor 1
            string infoMonitor2 = Properties.Settings.Default.InfoMonitor2;
            string[] monitorData2 = infoMonitor2.Split(';');          // Daten von Monitor 2
            string infoMonitor3 = Properties.Settings.Default.InfoMonitor3;
            string[] monitorData3 = infoMonitor3.Split(';');          // Daten von Monitor 3
            string infoMonitor4 = Properties.Settings.Default.InfoMonitor4;
            string[] monitorData4 = infoMonitor4.Split(';');          // Daten von Monitor 4

            int auswahl;
            if (monitorID == monitorData1[17]) auswahl = 0;         // Auswahl des Monitors
            else if (monitorID == monitorData2[17]) auswahl = 1;
            else if (monitorID == monitorData3[17]) auswahl = 2;
            else if (monitorID == monitorData4[17]) auswahl = 3;
            else{ auswahl = 10; Log.err("Es wurde von der Verknüpfung etwas Falsches übergeben oder die Monitordaten wurden zurückgesetzt.",null,true); }                                      // Fehler bei der Auswahl

            string[][] infoMonitor =                                                                    //Zusammenfassen der Motordaten zum Arbeiten 
            { 
                monitorData1, 
                monitorData2, 
                monitorData3, 
                monitorData4 
            };                                                                                        // Speichert die Daten der Monitore

            string pathMMExe = $"{Properties.Settings.Default.pfadDeskOK}\\MultiMonitorTool.exe";     // Pfad zur MultiMonitorTool.exe
            string pathDOExe = $"{Properties.Settings.Default.pfadDeskOK}\\DesktopOK.exe";     // Pfad zur 
            Log.inf("Gespeicherte und geladene Daten lesen zum Verwenden.");
            Log.inf($"Beim Starten der Verknüpfung wurde das Argument: {monitorID} weitergegeben, dies entspricht MonitorListe: {Convert.ToString(auswahl)}");
            if (vorhanden[auswahl] && aktiv[auswahl])
            {
                Log.inf("Abfrage ob Icons noch nicht verstaut sind und der Monitor zu IconSpeicherliste zugewiesen wurde, positiv  ");
                var moveIcons = new FunktionVerschieben();
                moveIcons.MoveDeskToPath(auswahl);                                                    // Verschiebt die Icons vom Desktop zum Pfad
                Log.inf("Icons verschieben abgeschlossen weiter mit schalten des Monitors ");
                Thread.Sleep(1000);                                                                   // Wartet 1 Sekunde
                FunktionMultiMonitor.MonitorDeaktivieren(pathMMExe, monitorID, auswahl);              // Deaktiviert den Monitor
                Log.inf("Das ausschalten des ausgewählten Monitors beendet");
            }
            else if (vorhanden[auswahl] && !aktiv[auswahl])
            {
                int deaktivierteMonitore = 0;                                                         // Zählt die Anzahl der deaktivierten Monitore
                for (int i = 0; i < 4; i++)
                {
                    if (vorhanden[i] && !aktiv[i])
                    {
                        deaktivierteMonitore++;                                                       // Erhöht den Zähler für jeden deaktivierten Monitor
                    }
                }
                if (deaktivierteMonitore == 1)                                                        // Wenn nur ein Monitor deaktiviert ist
                {
                    Log.inf("Bedingung erfüllt das Ausgewähltr Monitore Aktiviert wird");
                    FunktionMultiMonitor.MonitorAktivieren(pathMMExe, monitorID, auswahl);            // Aktiviert den ausgewählten Monitor
                    Thread.Sleep(1000);                                                               // Wartet 1 Sekunde

                    Log.inf("Abfrage ob Icons verstaut sind und der Monitor zu IconSpeicherliste zugewiesen wurde, positiv ");
                    var moveIconsTD = new FunktionVerschieben();
                    moveIconsTD.MovePathToDesk(auswahl);                                              // Verschiebt die Icons vom Pfad zum Desktop

                    Log.inf("Das anschalten des ausgewählten Monitors beendet");
                    Thread.Sleep(1000);

                    var configLoad = new FunktionMultiMonitor();
                    configLoad.MonitorLoadConfig();                                                         // Lädt die Konfiguration einmal

                    Thread.Sleep(2000);
                    Log.inf("Icons wiederherstellen.");                                                                                                                      // Wartet 2 Sekunden.
                    FunktionDesktopOK.IconRestore(pathDOExe, Properties.Settings.Default.eDeskOkLastSave);
                }
                else if (deaktivierteMonitore >= 2)                                                         // Wenn mehr als ein Monitor deaktiviert ist
                {
                    Log.inf("Mehr als ein Monitor ist deaktiviert, Konfiguration wird mehrfach geladen.");
                    for (int i = 0; i < deaktivierteMonitore; i++)
                    {
                        var configLoad = new FunktionMultiMonitor();
                        configLoad.MonitorLoadConfig();                                                     // Lädt die Konfiguration entsprechend der Anzahl der deaktivierten Monitore
                    }
                    for (int i = 0; i < 4; i++)                                                             // Deaktiviert alle nicht gewählten Monitore
                    {
                        if (i != auswahl && vorhanden[i] && !aktiv[i])                                      // Deaktiviert alle nicht gewählten Monitore
                        {
                            Log.inf($"Deaktiviere nicht gewählten Monitor {i + 1}");
                            FunktionMultiMonitor.MonitorDeaktivieren(pathMMExe, infoMonitor[i][17], i);     // Deaktiviert den nicht gewählten Monitor
                        }
                    }
                    Log.inf("Mehrere ungewählte Monitor gehandelt.");
                    Thread.Sleep(2000);
                    Log.inf("Icons rausholen die zu Monitor IconSpeicherliste zugewiesen wurde. ");
                    var moveIcons = new FunktionVerschieben();                                                  // Verschiebt die Icons vom Pfad zum Desktop
                    moveIcons.MovePathToDesk(auswahl);
                    Log.inf("Icons rausholen abgeschlossen");

                    Thread.Sleep(2000);
                    Log.inf("Icons wiederherstellen.");                                                                                                                      // Wartet 2 Sekunden.
                    FunktionDesktopOK.IconRestore(pathDOExe, Properties.Settings.Default.eDeskOkLastSave);

                }
                Log.inf("Das Einschalten des ausgewählten Monitors beendet");
                if (auswahl == 0) Properties.Settings.Default.eMonitorAktiv1 = true;                        // Setzt den Status von Monitor 1 auf aktiv.
                else if (auswahl == 1) Properties.Settings.Default.eMonitorAktiv2 = true;                   // Setzt den Status von Monitor 2 auf aktiv.
                else if (auswahl == 2) Properties.Settings.Default.eMonitorAktiv3 = true;                   // Setzt den Status von Monitor 3 auf aktiv.
                else if (auswahl == 3) Properties.Settings.Default.eMonitorAktiv4 = true;                   // Setzt den Status von Monitor 4 auf aktiv.
                Properties.Settings.Default.Save();                                                         // Speichert die Einstellungen.
                
            }
            else
            {
                Log.err("Monitor ist nicht vorhanden oder nicht aktiviert, es wird nichts gemacht.",null,true);
            }
            SystemSounds.Exclamation.Play();
            Thread.Sleep(1000);                                                                             // Wartet 1 Sekunde
            Log.inf("Monitore schalten abgeschkossen. ");
        }
    }
}

