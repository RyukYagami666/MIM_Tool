namespace MIM_Tool.Funktions
{
    internal class Funktion3MonitorKontrolle
    {
        public void MonitorKontrolle(int auswahl)
        {
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
            bool[] verstaut =
            {
                 Properties.Settings.Default.eMonitorIconsVerstaut1,  // Überprüft, ob die Icons von Monitor 1 verstaut sind
                 Properties.Settings.Default.eMonitorIconsVerstaut2,  // Überprüft, ob die Icons von Monitor 2 verstaut sind
                 Properties.Settings.Default.eMonitorIconsVerstaut3,  // Überprüft, ob die Icons von Monitor 3 verstaut sind
                 Properties.Settings.Default.eMonitorIconsVerstaut4   // Überprüft, ob die Icons von Monitor 4 verstaut sind
            };
            string[] zugewieseneIcons =
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen1, // Zuweisung der Icons für Monitor 1
                Properties.Settings.Default.eMonitorIconsZugewiesen2, // Zuweisung der Icons für Monitor 2
                Properties.Settings.Default.eMonitorIconsZugewiesen3, // Zuweisung der Icons für Monitor 3
                Properties.Settings.Default.eMonitorIconsZugewiesen4  // Zuweisung der Icons für Monitor 4
            };

            string infoMonitor1 = Properties.Settings.Default.InfoMonitor1;
            string[] monitorData1 = infoMonitor1.Split(';');          // Daten von Monitor 1
            string infoMonitor2 = Properties.Settings.Default.InfoMonitor2;
            string[] monitorData2 = infoMonitor2.Split(';');          // Daten von Monitor 2
            string infoMonitor3 = Properties.Settings.Default.InfoMonitor3;
            string[] monitorData3 = infoMonitor3.Split(';');          // Daten von Monitor 3
            string infoMonitor4 = Properties.Settings.Default.InfoMonitor4;
            string[] monitorData4 = infoMonitor4.Split(';');          // Daten von Monitor 4

            string[][] monitorID =
            {
                monitorData1,                                         // ID-Daten von Monitor 1
                monitorData2,                                         // ID-Daten von Monitor 2
                monitorData3,                                         // ID-Daten von Monitor 3
                monitorData4                                          // ID-Daten von Monitor 4
            };
            string pathMMExe = $"{Properties.Settings.Default.pfadDeskOK}\\MultiMonitorTool.exe";     // Pfad zur MultiMonitorTool.exe

            string pathDExe = Properties.Settings.Default.pfadDeskOK + "\\DesktopOK.exe";             // Pfad zur DesktopOK.exe
            string pathDLastData = Properties.Settings.Default.eDeskOkLastSave;                       // Pfad zu den letzten gespeicherten Desktop-Daten

            if (vorhanden[auswahl] && aktiv[auswahl])
            {
                if (!verstaut[auswahl] && !string.IsNullOrEmpty(zugewieseneIcons[auswahl]))
                {
                    var moveIcons = new FunktionVerschieben();
                    moveIcons.MoveDeskToPath(auswahl);                                                // Verschiebt die Icons vom Desktop zum Pfad
                }
                Thread.Sleep(1000);                                                                   // Wartet 1 Sekunde
                FunktionMultiMonitor.MonitorDeaktivieren(pathMMExe, monitorID[auswahl][17], auswahl); // Deaktiviert den Monitor
                Thread.Sleep(1000);                                                                   // Wartet 1 Sekunde
                FunktionDesktopOK.IconRestore(pathDExe, pathDLastData);                               // Stellt die Icons auf dem Desktop wieder her
            }
            else if (vorhanden[auswahl] && !aktiv[auswahl])
            {
                FunktionMultiMonitor.MonitorAktivieren(pathMMExe, monitorID[auswahl][17], auswahl);   // Aktiviert den Monitor
                Thread.Sleep(1000);                                                                   // Wartet 1 Sekunde
                if (verstaut[auswahl] && !string.IsNullOrEmpty(zugewieseneIcons[auswahl]))
                {
                    var moveIcons = new FunktionVerschieben();
                    moveIcons.MovePathToDesk(auswahl);                                                // Verschiebt die Icons vom Pfad zum Desktop
                }
            }
        }
    }
}

