using System.Windows.Forms;

namespace MIM_Tool.Funktions
{
    internal class Funktion1Initialisieren
    {
        // Methode zur Initialisierung der wichtigen Funktionen
        public void Initialisieren()
        {
            var defaultPath = new FunktionDefaultPath();            // Initialisiert den Standardpfad
            defaultPath.DefaultPath();

            var dodStatus = new FunktionDesktopOK();                // Überprüft den Desktop-Status
            dodStatus.DODKontrolle();

            var dodStart = new FunktionDesktopOK();                 // Startet die Desktop-Überwachung
            dodStart.DODStart();

            var mmdStatus = new FunktionMultiMonitor();             // Überprüft den Multi-Monitor-Status
            mmdStatus.MMDKontrolle();

            var mmdStart = new FunktionMultiMonitor();              // Startet die Multi-Monitor-Überwachung
            mmdStart.MMDStart();

            var datenLesen = new Funktion2DatenLesen();             // Liest die Daten ein
            datenLesen.DatenLesen();
        }

        // Methode zur Abfrage, ob die Initialisierung erneut durchgeführt werden soll
        public void InitialisierenAbfrage()
        {
            var result = MessageBox.Show("Initialisieren erneut durchführen, Gespeichertes wird überschrieben", "Initialisieren", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Zeigt eine Nachricht an, um zu fragen, ob die Initialisierung erneut durchgeführt werden soll

            if (result == DialogResult.Yes)                        // Wenn der Benutzer "Ja" wählt, wird die Initialisierung erneut durchgeführt
            {
                Initialisieren();
            }
        }
    }
}

