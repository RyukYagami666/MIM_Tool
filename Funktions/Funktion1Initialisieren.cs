using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App3.Funktions
{
    internal class Funktion1Initialisieren
    {
        public void Initialisieren()
        {
            CopyMSGBox.Show("Initialisieren wird ausgeführt.");

            var defaultPath = new FunktionDefaultPath();
            defaultPath.DefaultPath();

            var dodStatus = new FunktionDesktopOK();
            dodStatus.DODKontrolle();

            var dodStart = new FunktionDesktopOK();
            dodStart.DODStart();

            var mmdStatus = new FunktionMultiMonitor();
            mmdStatus.MMDKontrolle();

            var mmdStart = new FunktionMultiMonitor();
            mmdStart.MMDStart();

            var datenLesen = new Funktion2DatenLesen();
            datenLesen.DatenLesen();

            


        }
        
    }
}
