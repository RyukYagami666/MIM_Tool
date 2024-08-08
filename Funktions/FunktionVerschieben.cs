using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace App3.Funktions
{
    public class FunktionVerschieben
    {
        string storagePath = Properties.Settings.Default.pfadDeskOK + "\\Icons";
        public void Verschieben1Control()      //TODO:
        {
            if (!System.IO.Directory.Exists(storagePath))
            {
                OrdnerAbfrage();
            }
            // Hier können Sie den Code für das Verschieben von Dateien einfügen
            if (Properties.Settings.Default.eMonitorVorhanden1 == true && Properties.Settings.Default.eMonitorAktiv1 == false && !string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen1) && Properties.Settings.Default.eMonitorIconsVerstaut1 == false)
            {
                MessageBox.Show("Monitor 1 auf Aktiv setzen. (Neu drü.)", "Monitor Status");
                Properties.Settings.Default.eMonitorAktiv1 = true;
            }
            else if (Properties.Settings.Default.eMonitorVorhanden2 == true && Properties.Settings.Default.eMonitorAktiv2 == false && !string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen2) && Properties.Settings.Default.eMonitorIconsVerstaut2 == false)
            {
                MessageBox.Show("Monitor 2 auf Aktiv setzen. (Neu drü.)", "Monitor Status");
                Properties.Settings.Default.eMonitorAktiv2 = true;
            }
            else if (Properties.Settings.Default.eMonitorVorhanden3 == true && Properties.Settings.Default.eMonitorAktiv3 == false && !string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen3) && Properties.Settings.Default.eMonitorIconsVerstaut3 == false)
            {
                MessageBox.Show("Monitor 3 auf Aktiv setzen. (Neu drü.)", "Monitor Status");
                Properties.Settings.Default.eMonitorAktiv3 = true;
            }
            else if (Properties.Settings.Default.eMonitorVorhanden1 == true && Properties.Settings.Default.eMonitorAktiv1 == true && !string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen1) && Properties.Settings.Default.eMonitorIconsVerstaut1 == true && !System.IO.Directory.Exists(storagePath))
            {
                Properties.Settings.Default.eMonitorIconsVerstaut1 = false;
                MessageBox.Show($"Fehler {storagePath} nicht mehr Vorhanden? \n Bitte konntroliere", "Verschieben zu Speicher");
                OrdnerAbfrage();
            }
            else if (Properties.Settings.Default.eMonitorVorhanden1 == true && Properties.Settings.Default.eMonitorAktiv1 == true && !string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen1) && Properties.Settings.Default.eMonitorIconsVerstaut1 == false)
            {
                var result = MessageBox.Show("Icons in Speicher Verschieben?", "Verschieben zu Speicher", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    MoveDeskToPath1();
                }
            }
            else if (Properties.Settings.Default.eMonitorVorhanden1 == true && Properties.Settings.Default.eMonitorAktiv1 == false && !string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen1) && Properties.Settings.Default.eMonitorIconsVerstaut1 == true)
            {
                var result = MessageBox.Show("Icons zurück aus Desktop Verschieben?", "Verschieben zu Desktop", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    MovePathToDesk1();
                }
            }
        }

        public void OrdnerAbfrage()
        {
            
            if (System.IO.Directory.Exists(Properties.Settings.Default.pfadDeskOK) && !System.IO.Directory.Exists(storagePath))
            {
                    System.IO.Directory.CreateDirectory(storagePath);
            }
            else
            {
                MessageBox.Show("Der Pfad: \n" + Properties.Settings.Default.pfadDeskOK + "\n wurde nicht gefunden", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

// --------1----------------------------------------------------------Ab hier beginnt die Sektion für das Verschieben -----------------------------------------------------------------------------------------------------------------------------

        public void MoveDeskToPath1()                                                                //Gespeicherte liste "1" verschieben von Desktop zum Speicher --------------------------------------------------------------------------------------------
        {
            string iconsZugewiesen1 = Properties.Settings.Default.eMonitorIconsZugewiesen1;
            string[] deskIconPath1 = iconsZugewiesen1.Split(';');
            foreach (string icon in deskIconPath1)
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
            Properties.Settings.Default.eMonitorIconsVerstaut1 = true;
            Properties.Settings.Default.eMonitorAktiv1 = false; //TODO:
            Properties.Settings.Default.Save();
            MessageBox.Show($"Fertig", "Verschieben zu Speicher");
        }

        public void MovePathToDesk1()                                                                //Gespeicherte liste "1" verschieben von Speicher zurück zum Desktop --------------------------------------------------------------------------------------
        {
            string iconsZugewiesen1 = Properties.Settings.Default.eMonitorIconsZugewiesen1;
            string[] deskIconPath1 = iconsZugewiesen1.Split(';');
            string pathExe = Properties.Settings.Default.pfadDeskOK + "\\DesktopOK.exe";
            string pathLastData = Properties.Settings.Default.eDeskOkLastSave;
            foreach (string icon in deskIconPath1)
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
            Properties.Settings.Default.eMonitorIconsVerstaut1 = false;
            Properties.Settings.Default.eMonitorAktiv1 = true; //TODO:
            Properties.Settings.Default.Save();
            MessageBox.Show($"Fertig", "Verschieben zu Desktop");
        }
//-----------2
        public void MoveDeskToPath2()                                                               //Gespeicherte liste "2" verschieben von Desktop zum Speicher --------------------------------------------------------------------------------------------
        {
            string iconsZugewiesen2 = Properties.Settings.Default.eMonitorIconsZugewiesen2;
            string[] deskIconPath2 = iconsZugewiesen2.Split(';');
            foreach (string icon in deskIconPath2)
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
            Properties.Settings.Default.eMonitorIconsVerstaut2 = true;
            Properties.Settings.Default.eMonitorAktiv2 = false; //TODO:
            Properties.Settings.Default.Save();
            MessageBox.Show($"Fertig", "Verschieben zu Speicher");
        }

        public void MovePathToDesk2()                                                                //Gespeicherte liste "2" verschieben von Speicher zurück zum Desktop --------------------------------------------------------------------------------------
        {
            string iconsZugewiesen2 = Properties.Settings.Default.eMonitorIconsZugewiesen2;
            string[] deskIconPath2 = iconsZugewiesen2.Split(';');
            string pathExe = Properties.Settings.Default.pfadDeskOK + "\\DesktopOK.exe";
            string pathLastData = Properties.Settings.Default.eDeskOkLastSave;
            foreach (string icon in deskIconPath2)
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
            Properties.Settings.Default.eMonitorIconsVerstaut2 = false;
            Properties.Settings.Default.eMonitorAktiv2 = true; //TODO:
            Properties.Settings.Default.Save();
            MessageBox.Show($"Fertig", "Verschieben zu Desktop");
        }
//-----------3
        public void MoveDeskToPath3()                                                               //Gespeicherte liste "3" verschieben von Desktop zum Speicher --------------------------------------------------------------------------------------------
        {
            string iconsZugewiesen3 = Properties.Settings.Default.eMonitorIconsZugewiesen3;
            string[] deskIconPath3 = iconsZugewiesen3.Split(';');
            foreach (string icon in deskIconPath3)
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
            Properties.Settings.Default.eMonitorIconsVerstaut3 = true;
            Properties.Settings.Default.eMonitorAktiv3 = false; //TODO:
            Properties.Settings.Default.Save();
            MessageBox.Show($"Fertig", "Verschieben zu Speicher");
        }

        public void MovePathToDesk3()                                                                //Gespeicherte liste "3" verschieben von Speicher zurück zum Desktop --------------------------------------------------------------------------------------
        {
            string iconsZugewiesen3 = Properties.Settings.Default.eMonitorIconsZugewiesen3;
            string[] deskIconPath3 = iconsZugewiesen3.Split(';');
            string pathExe = Properties.Settings.Default.pfadDeskOK + "\\DesktopOK.exe";
            string pathLastData = Properties.Settings.Default.eDeskOkLastSave;
            foreach (string icon in deskIconPath3)
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
            Properties.Settings.Default.eMonitorIconsVerstaut3 = false;
            Properties.Settings.Default.eMonitorAktiv3 = true; //TODO:
            Properties.Settings.Default.Save();
            MessageBox.Show($"Fertig", "Verschieben zu Desktop");
        }
//-----------4
        public void MoveDeskToPath4()                                                               //Gespeicherte liste "4" verschieben von Desktop zum Speicher --------------------------------------------------------------------------------------------
        {
            string iconsZugewiesen4 = Properties.Settings.Default.eMonitorIconsZugewiesen4;
            string[] deskIconPath4 = iconsZugewiesen4.Split(';');
            foreach (string icon in deskIconPath4)
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
            Properties.Settings.Default.eMonitorIconsVerstaut4 = true;
            Properties.Settings.Default.eMonitorAktiv4 = false; //TODO:
            Properties.Settings.Default.Save();
            MessageBox.Show($"Fertig", "Verschieben zu Speicher");
        }

        public void MovePathToDesk4()                                                                //Gespeicherte liste "4" verschieben von Speicher zurück zum Desktop --------------------------------------------------------------------------------------
        {
            string iconsZugewiesen4 = Properties.Settings.Default.eMonitorIconsZugewiesen4;
            string[] deskIconPath4 = iconsZugewiesen4.Split(';');
            string pathExe = Properties.Settings.Default.pfadDeskOK + "\\DesktopOK.exe";
            string pathLastData = Properties.Settings.Default.eDeskOkLastSave;
            foreach (string icon in deskIconPath4)
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
            Properties.Settings.Default.eMonitorIconsVerstaut4 = false;
            Properties.Settings.Default.eMonitorAktiv4 = true; //TODO:
            Properties.Settings.Default.Save();
            MessageBox.Show($"Fertig", "Verschieben zu Desktop");
        }
    }
}
