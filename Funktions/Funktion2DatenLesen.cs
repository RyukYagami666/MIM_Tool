using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App3.Funktions
{
    internal class Funktion2DatenLesen
    {
        public void DatenLesen()
        {
            CopyMSGBox.Show("Daten werden gelesen.");

            var dosStatus = new FunktionDesktopOK();
            dosStatus.DOSKontrolle();

            var doIconPos = new FunktionDesktopOK();
            doIconPos.IconSavePos();

            var mmsStatus = new FunktionMultiMonitor();
            mmsStatus.MMSKontrolle();

            var monSaveConfig = new FunktionMultiMonitor();
            monSaveConfig.MonitorSaveConfig();

            var mmrStatus = new FunktionMultiMonitor();
            mmrStatus.MMRKontrolle();

            var monSaveData = new FunktionDeskScen();
            monSaveData.MonitorSaveData();

            var mmDataRead = new FunktionDeskScen();
            mmDataRead.DataRead(Properties.Settings.Default.pfadDeskOK + "\\MonitorDaten.txt");

            var mmDataTrim = new FunktionVergleich();
            mmDataTrim.MultiMonDataTrim();

            var dorStatus = new FunktionDesktopOK();
            dorStatus.DORKontrolle();

            var doReadData = new FunktionDesktopOK();
            doReadData.DataRead(Properties.Settings.Default.eDeskOkLastSave);

            var doConvert = new FunktionVergleich();
            doConvert.AbwandelnDerData();

            Properties.Settings.Default.Inizialisiert = true;


        }
        public void DatenLesenAbfrage()
        {

            var result = MessageBox.Show("Monitordaten erneut Laden, Gespeichertes wird überschrieben", "Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Setze die Monitor-Informationen zurück
                Properties.Settings.Default.InfoMonitor1 = "";
                Properties.Settings.Default.InfoMonitor2 = "";
                Properties.Settings.Default.InfoMonitor3 = "";
                Properties.Settings.Default.InfoMonitor4 = "";

                // Setze die Icon-Zuweisungen und -Status zurück
                Properties.Settings.Default.eMonitorVorhanden1 = false;
                Properties.Settings.Default.eMonitorAktiv1 = false;

                Properties.Settings.Default.eMonitorVorhanden2 = false;
                Properties.Settings.Default.eMonitorAktiv2 = false;

                Properties.Settings.Default.eMonitorVorhanden3 = false;
                Properties.Settings.Default.eMonitorAktiv3 = false;

                Properties.Settings.Default.eMonitorVorhanden4 = false;
                Properties.Settings.Default.eMonitorAktiv4 = false;

                // Setze die Desktop-Informationen zurück
                Properties.Settings.Default.MultiMonData = null;
                Properties.Settings.Default.MultiMonDataTrim1 = null;
                Properties.Settings.Default.MultiMonDataTrim2 = null;
                Properties.Settings.Default.MultiMonDataTrim3 = null;
                Properties.Settings.Default.MultiMonDataTrim4 = null;

                Properties.Settings.Default.SelectetMonitor = 10;

                Properties.Settings.Default.Save();


                DatenLesen();
            }

        }
    }
}
