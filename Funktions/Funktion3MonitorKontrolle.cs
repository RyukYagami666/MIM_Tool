using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MIM_Tool.Funktions
{
    internal class Funktion3MonitorKontrolle
    {
        public void MonitorKontrolle(int auswahl)
        {
            bool[] vorhanden =
            {
                 Properties.Settings.Default.eMonitorVorhanden1,
                 Properties.Settings.Default.eMonitorVorhanden2,
                 Properties.Settings.Default.eMonitorVorhanden3,
                 Properties.Settings.Default.eMonitorVorhanden4
            };
            bool[] aktiv =
            {
                 Properties.Settings.Default.eMonitorAktiv1,
                 Properties.Settings.Default.eMonitorAktiv2,
                 Properties.Settings.Default.eMonitorAktiv3,
                 Properties.Settings.Default.eMonitorAktiv4
            };
            bool[] verstaut =
            {
                 Properties.Settings.Default.eMonitorIconsVerstaut1,
                 Properties.Settings.Default.eMonitorIconsVerstaut2,
                 Properties.Settings.Default.eMonitorIconsVerstaut3,
                 Properties.Settings.Default.eMonitorIconsVerstaut4
            };
            string[] zugewieseneIcons =
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen1,
                Properties.Settings.Default.eMonitorIconsZugewiesen2,
                Properties.Settings.Default.eMonitorIconsZugewiesen3,
                Properties.Settings.Default.eMonitorIconsZugewiesen4
            };

            string infoMonitor1 = Properties.Settings.Default.InfoMonitor1;
            string[] monitorData1 = infoMonitor1.Split(';');
            string infoMonitor2 = Properties.Settings.Default.InfoMonitor2;
            string[] monitorData2 = infoMonitor2.Split(';');
            string infoMonitor3 = Properties.Settings.Default.InfoMonitor3;
            string[] monitorData3 = infoMonitor3.Split(';');
            string infoMonitor4 = Properties.Settings.Default.InfoMonitor4;
            string[] monitorData4 = infoMonitor4.Split(';');

            string[][] monitorID =
            {
                monitorData1,
                monitorData2,
                monitorData3,
                monitorData4
            };
            string pathMMExe = $"{Properties.Settings.Default.pfadDeskOK}\\MultiMonitorTool.exe";

            string pathDExe = Properties.Settings.Default.pfadDeskOK + "\\DesktopOK.exe";
            string pathDLastData = Properties.Settings.Default.eDeskOkLastSave;

            if (vorhanden[auswahl]&&aktiv[auswahl])
            {
                if (!verstaut[auswahl] && !string.IsNullOrEmpty(zugewieseneIcons[auswahl]))
                {
                    var moveIcons = new FunktionVerschieben();
                    moveIcons.MoveDeskToPath(auswahl);
                }
                Thread.Sleep(1000);
                FunktionMultiMonitor.MonitorDeaktivieren(pathMMExe, monitorID[auswahl][17], auswahl);
                Thread.Sleep(1000);
                FunktionDesktopOK.IconRestore(pathDExe, pathDLastData);
            }
            else if (vorhanden[auswahl] && !aktiv[auswahl])
            {
                FunktionMultiMonitor.MonitorAktivieren(pathMMExe, monitorID[auswahl][17], auswahl);
                Thread.Sleep(1000);
                if (verstaut[auswahl] && !string.IsNullOrEmpty(zugewieseneIcons[auswahl]))
                {
                    var moveIcons = new FunktionVerschieben();
                    moveIcons.MovePathToDesk(auswahl);
                }
            }


        }
    }
}
