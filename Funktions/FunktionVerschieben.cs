using System.Windows.Forms;
using System.Threading;// Importiert Funktionen für Windows-Formulare.

namespace MIM_Tool.Funktions
{
    public class FunktionVerschieben
    {
        string storagePath = Properties.Settings.Default.pfadDeskOK + "\\Icons";                                                          // Definiert den Speicherpfad für die Icons.

        public void Verschieben1Control()                                                                                                 // Methode zur Steuerung des Verschiebens der Icons für Monitor 1.
        {
                                                                                                                                          // Überprüft, ob Monitor 1 vorhanden ist, Icons zugewiesen sind, die Icons verstaut sind und der Speicherpfad existiert.
            if (Properties.Settings.Default.eMonitorVorhanden1 == true && !string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen1) && Properties.Settings.Default.eMonitorIconsVerstaut1 == true && !System.IO.Directory.Exists(storagePath))
            {
                Properties.Settings.Default.eMonitorIconsVerstaut1 = false;                                                               // Setzt den Status der verstauten Icons auf false.
                MessageBox.Show($"Fehler {storagePath} nicht mehr Vorhanden? \n Bitte konntroliere", "Verschieben zu Speicher");          // Zeigt eine Fehlermeldung an.
            }
                                                                                                                                          // Überprüft, ob Monitor 1 vorhanden ist, Icons zugewiesen sind und die Icons nicht verstaut sind.
            else if (Properties.Settings.Default.eMonitorVorhanden1 == true && !string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen1) && Properties.Settings.Default.eMonitorIconsVerstaut1 == false)
            {
                var result = MessageBox.Show("Icons in Speicher Verschieben?", "Verschieben zu Speicher", MessageBoxButtons.YesNo);       // Fragt den Benutzer, ob die Icons in den Speicher verschoben werden sollen.
                if (result == DialogResult.Yes)
                {
                    MoveDeskToPath(0);                                                                                                    // Verschiebt die Icons in den Speicher.
                }
            }
                                                                                                                                          // Überprüft, ob Monitor 1 vorhanden ist, Icons zugewiesen sind und die Icons verstaut sind.
            else if (Properties.Settings.Default.eMonitorVorhanden1 == true && !string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen1) && Properties.Settings.Default.eMonitorIconsVerstaut1 == true)
            {
                var result = MessageBox.Show("Icons zurück aus Desktop Verschieben?", "Verschieben zu Desktop", MessageBoxButtons.YesNo); // Fragt den Benutzer, ob die Icons zurück auf den Desktop verschoben werden sollen.
                if (result == DialogResult.Yes)
                {
                    MovePathToDesk(0);                                                                                                    // Verschiebt die Icons zurück auf den Desktop.
                }
            }
        }

        // --------1----------------------------------------------------------Ab hier beginnt die Sektion für das Verschieben -----------------------------------------------------------------------------------------------------------------------------

        public void MoveDeskToPath(int index)                                                                                                                   // Methode zum Verschieben der gespeicherten Liste vom Desktop zum Speicher.
        {
            string[] zugewieseneIcons =
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen1,
                Properties.Settings.Default.eMonitorIconsZugewiesen2,
                Properties.Settings.Default.eMonitorIconsZugewiesen3,
                Properties.Settings.Default.eMonitorIconsZugewiesen4
            };

            string iconsZugewiesen = zugewieseneIcons[index];                                                                                                   // Holt die zugewiesenen Icons für den angegebenen Monitor.
            string[] deskIconPath = iconsZugewiesen.Split(';');                                                                                                 // Teilt die Icon-Pfade in ein Array auf.
            foreach (string icon in deskIconPath)
            {
                if (System.IO.File.Exists(icon))                                                                                                                // Überprüft, ob die Datei existiert.
                {
                    System.IO.File.Move(icon, storagePath + "\\" + System.IO.Path.GetFileName(icon));                                                           // Verschiebt die Datei in den Speicherpfad.
                }
                else if (System.IO.File.Exists(storagePath + "\\" + System.IO.Path.GetFileName(icon)))                                                          // Überprüft, ob die Datei bereits im Speicherpfad existiert.
                {
                    MessageBox.Show($"Fehler {System.IO.Path.GetFileName(icon)} schon Verschoben \n Bitte konntroliere", "Verschieben zu Speicher");            // Zeigt eine Fehlermeldung an.
                }
                else
                {
                    MessageBox.Show($"Fehler {System.IO.Path.GetFileName(icon)} nicht mehr Vorhanden? \nIcons bitte neu zuweisen.", "Verschieben zu Speicher"); // Zeigt eine Fehlermeldung an.
                }
            }
            if (index == 0) Properties.Settings.Default.eMonitorIconsVerstaut1 = true;                                                                          // Setzt den Status der verstauten Icons für Monitor 1 auf true.
            if (index == 1) Properties.Settings.Default.eMonitorIconsVerstaut2 = true;                                                                          // Setzt den Status der verstauten Icons für Monitor 2 auf true.
            if (index == 2) Properties.Settings.Default.eMonitorIconsVerstaut3 = true;                                                                          // Setzt den Status der verstauten Icons für Monitor 3 auf true.
            if (index == 3) Properties.Settings.Default.eMonitorIconsVerstaut4 = true;                                                                          // Setzt den Status der verstauten Icons für Monitor 4 auf true.
            Properties.Settings.Default.Save();                                                                                                                 // Speichert die Einstellungen.
            Thread.Sleep(1000);                                                                                                                                // Wartet 2 Sekunden.
        }

        public void MovePathToDesk(int index)                                                                                                                  // Methode zum Verschieben der gespeicherten Liste vom Speicher zurück zum Desktop.
        {
            string[] zugewieseneIcons =
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen1,
                Properties.Settings.Default.eMonitorIconsZugewiesen2,
                Properties.Settings.Default.eMonitorIconsZugewiesen3,
                Properties.Settings.Default.eMonitorIconsZugewiesen4
            };

            string iconsZugewiesen = zugewieseneIcons[index];                                                                                                  // Holt die zugewiesenen Icons für den angegebenen Monitor.
            string[] deskIconPath = iconsZugewiesen.Split(';');                                                                                                // Teilt die Icon-Pfade in ein Array auf.
            string pathExe = Properties.Settings.Default.pfadDeskOK + "\\DesktopOK.exe";                                                                       // Holt den Pfad zur DesktopOK.exe.
            string pathLastData = Properties.Settings.Default.eDeskOkLastSave;                                                                                 // Holt den Pfad zur letzten gespeicherten Daten.
            foreach (string icon in deskIconPath)
            {
                if (System.IO.File.Exists(storagePath + "\\" + System.IO.Path.GetFileName(icon)))                                                              // Überprüft, ob die Datei im Speicherpfad existiert.
                {
                    System.IO.File.Move(storagePath + "\\" + System.IO.Path.GetFileName(icon), icon);                                                          // Verschiebt die Datei zurück auf den Desktop.
                }
                else if (System.IO.File.Exists(icon))                                                                                                          // Überprüft, ob die Datei bereits auf dem Desktop existiert.
                {
                    MessageBox.Show($"Fehler {System.IO.Path.GetFileName(icon)} schon Verschoben \n Bitte konntroliere", "Verschieben zu Desktop");            // Zeigt eine Fehlermeldung an.
                }
                else
                {
                    MessageBox.Show($"Fehler {System.IO.Path.GetFileName(icon)} nicht mehr Vorhanden? \nIcons bitte neu zuweisen.", "Verschieben zu Desktop"); // Zeigt eine Fehlermeldung an.
                }
            }
            Thread.Sleep(2000);                                                                                                                                // Wartet 2 Sekunden.
            FunktionDesktopOK.IconRestore(pathExe, pathLastData);                                                                                              // Ruft die Methode zum Wiederherstellen der Icons auf.
            if (index == 0) Properties.Settings.Default.eMonitorIconsVerstaut1 = false;                                                                        // Setzt den Status der verstauten Icons für Monitor 1 auf false.
            if (index == 1) Properties.Settings.Default.eMonitorIconsVerstaut2 = false;                                                                        // Setzt den Status der verstauten Icons für Monitor 2 auf false.
            if (index == 2) Properties.Settings.Default.eMonitorIconsVerstaut3 = false;                                                                        // Setzt den Status der verstauten Icons für Monitor 3 auf false.
            if (index == 3) Properties.Settings.Default.eMonitorIconsVerstaut4 = false;                                                                        // Setzt den Status der verstauten Icons für Monitor 4 auf false.
            Properties.Settings.Default.Save();                                                                                                                // Speichert die Einstellungen.
            Thread.Sleep(1000);                                                                                                                                // Wartet 2 Sekunden.
        }
    }
}
