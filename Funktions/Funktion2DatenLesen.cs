using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            FunktionIconListe.Execute();

            Properties.Settings.Default.Inizialisiert = true;


        }
    }
}
