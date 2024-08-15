using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MIM_Tool.Funktions
{
    public class FunktionVerschieben
    {
        string storagePath = Properties.Settings.Default.pfadDeskOK + "\\Icons";
        public void Verschieben1Control()      //TODO:
        {
            
            
            if (Properties.Settings.Default.eMonitorVorhanden1 == true && !string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen1) && Properties.Settings.Default.eMonitorIconsVerstaut1 == true && !System.IO.Directory.Exists(storagePath))
            {
                Properties.Settings.Default.eMonitorIconsVerstaut1 = false;
                MessageBox.Show($"Fehler {storagePath} nicht mehr Vorhanden? \n Bitte konntroliere", "Verschieben zu Speicher");
            }
            else if (Properties.Settings.Default.eMonitorVorhanden1 == true && !string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen1) && Properties.Settings.Default.eMonitorIconsVerstaut1 == false)
            {
                var result = MessageBox.Show("Icons in Speicher Verschieben?", "Verschieben zu Speicher", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    MoveDeskToPath(0);
                }
            }
            else if (Properties.Settings.Default.eMonitorVorhanden1 == true && !string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen1) && Properties.Settings.Default.eMonitorIconsVerstaut1 == true)
            {
                var result = MessageBox.Show("Icons zurück aus Desktop Verschieben?", "Verschieben zu Desktop", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    MovePathToDesk(0);
                }
            }
        }

 

// --------1----------------------------------------------------------Ab hier beginnt die Sektion für das Verschieben -----------------------------------------------------------------------------------------------------------------------------

        public void MoveDeskToPath(int index)                                                                //Gespeicherte liste verschieben von Desktop zum Speicher --------------------------------------------------------------------------------------------
        {
            string[] zugewieseneIcons =
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen1,
                Properties.Settings.Default.eMonitorIconsZugewiesen2,
                Properties.Settings.Default.eMonitorIconsZugewiesen3,
                Properties.Settings.Default.eMonitorIconsZugewiesen4
            };

            string iconsZugewiesen = zugewieseneIcons[index];
            string[] deskIconPath = iconsZugewiesen.Split(';');
            foreach (string icon in deskIconPath)
            {
                if (System.IO.File.Exists(icon))
                {
                    System.IO.File.Move(icon, storagePath + "\\" + System.IO.Path.GetFileName(icon));
                }
                else if (System.IO.File.Exists(storagePath + "\\" + System.IO.Path.GetFileName(icon)))
                {
                    MessageBox.Show($"Fehler {System.IO.Path.GetFileName(icon)} schon Verschoben \n Bitte konntroliere", "Verschieben zu Speicher");
                }
                else
                {
                    MessageBox.Show($"Fehler {System.IO.Path.GetFileName(icon)} nicht mehr Vorhanden? \nIcons bitte neu zuweisen.", "Verschieben zu Speicher");
                }
            }
            if (index == 0) Properties.Settings.Default.eMonitorIconsVerstaut1 = true;
            if (index == 1) Properties.Settings.Default.eMonitorIconsVerstaut2 = true;
            if (index == 2) Properties.Settings.Default.eMonitorIconsVerstaut3 = true;
            if (index == 3) Properties.Settings.Default.eMonitorIconsVerstaut4 = true;
            Properties.Settings.Default.Save();
            MessageBox.Show($"Fertig", "Verschieben zu Speicher");
        }

        public void MovePathToDesk(int index)                                                                //Gespeicherte liste verschieben von Speicher zurück zum Desktop --------------------------------------------------------------------------------------
        {
            string[] zugewieseneIcons =
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen1,
                Properties.Settings.Default.eMonitorIconsZugewiesen2,
                Properties.Settings.Default.eMonitorIconsZugewiesen3,
                Properties.Settings.Default.eMonitorIconsZugewiesen4
            };

            string iconsZugewiesen = zugewieseneIcons[index];
            string[] deskIconPath = iconsZugewiesen.Split(';');
            string pathExe = Properties.Settings.Default.pfadDeskOK + "\\DesktopOK.exe";
            string pathLastData = Properties.Settings.Default.eDeskOkLastSave;
            foreach (string icon in deskIconPath)
            {
                if (System.IO.File.Exists(storagePath + "\\" + System.IO.Path.GetFileName(icon)))
                {
                    System.IO.File.Move(storagePath + "\\" + System.IO.Path.GetFileName(icon), icon);
                }
                else if (System.IO.File.Exists(icon))
                {
                    MessageBox.Show($"Fehler {System.IO.Path.GetFileName(icon)} schon Verschoben \n Bitte konntroliere", "Verschieben zu Desktop");
                }
                else
                {
                    MessageBox.Show($"Fehler {System.IO.Path.GetFileName(icon)} nicht mehr Vorhanden? \nIcons bitte neu zuweisen.", "Verschieben zu Desktop");
                }
            }
            Thread.Sleep(2000);
            FunktionDesktopOK.IconRestore(pathExe, pathLastData);
            if (index == 0) Properties.Settings.Default.eMonitorIconsVerstaut1 = false;
            if (index == 1) Properties.Settings.Default.eMonitorIconsVerstaut2 = false;
            if (index == 2) Properties.Settings.Default.eMonitorIconsVerstaut3 = false;
            if (index == 3) Properties.Settings.Default.eMonitorIconsVerstaut4 = false;
            Properties.Settings.Default.Save();
            MessageBox.Show($"Fertig", "Verschieben zu Desktop");
        }
    }
}
