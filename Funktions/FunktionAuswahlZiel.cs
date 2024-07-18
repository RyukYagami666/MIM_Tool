using App3.Properties;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App3.Funktions
{
    class FunktionAuswahlZiel
    {
        public void Prüfen()
        {

            if (!string.IsNullOrEmpty(Properties.Settings.Default.InfoMonitor1))
            {
                Properties.Settings.Default.eMonitorVorhanden1 = true;
            }
            else { Properties.Settings.Default.eMonitorVorhanden1 = false; }
            if (!string.IsNullOrEmpty(Properties.Settings.Default.InfoMonitor2))
            {
                Properties.Settings.Default.eMonitorVorhanden2 = true;
            }
            else { Properties.Settings.Default.eMonitorVorhanden2 = false; }
            if (!string.IsNullOrEmpty(Properties.Settings.Default.InfoMonitor3))
            {
                Properties.Settings.Default.eMonitorVorhanden3 = true;
            }
            else { Properties.Settings.Default.eMonitorVorhanden3 = false; }
            if (!string.IsNullOrEmpty(Properties.Settings.Default.InfoMonitor4))
            {
                Properties.Settings.Default.eMonitorVorhanden4 = true;
            }
            else { Properties.Settings.Default.eMonitorVorhanden4 = false; }
            Properties.Settings.Default.Save();




        }
        public void MonitorGewählt1()
        {
            if (Properties.Settings.Default.eMonitorIconsZugewiesen1 != null)
            {
                    var result = MessageBox.Show("Es sind schon Icons zugewiesen! Wählen Sie eine Option: Ja (Alle überschreiben), Nein (Einen hinzufügen), Abbrechen (Aktion abbrechen).", "Aktion wählen", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (result)
                    {
                        case DialogResult.Yes:
                        // Logik zum Überschreiben aller Icons
                        Properties.Settings.Default.eMonitorIconsZugewiesen1 = Properties.Settings.Default.DeskIconPfadMTemp;
                        Properties.Settings.Default.eMonitorIconsCount1 = Properties.Settings.Default.eMonitorIconsCountTemp;
                        Properties.Settings.Default.DeskIconPfadMTemp = "";
                        Properties.Settings.Default.eMonitorIconsCountTemp = 0;
                        MessageBox.Show("Alle Icons wurden überschrieben.");
                            break;
                        case DialogResult.No:
                            int mNummer = 1;
                            // Logik zum Hinzufügen eines Icons
                            MessageBox.Show("Fügen Sie ein einzelnes Icon hinzu.");
                            break;
                        case DialogResult.Cancel:
                            
                            MessageBox.Show("Aktion abgebrochen.");
                            break;
                    }
                    Properties.Settings.Default.Save();
                

            }
            else
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen1 = Properties.Settings.Default.DeskIconPfadMTemp;
                Properties.Settings.Default.eMonitorIconsCount1 = Properties.Settings.Default.eMonitorIconsCountTemp;
                Properties.Settings.Default.DeskIconPfadMTemp = "";
                Properties.Settings.Default.eMonitorIconsCountTemp = 0;
                Properties.Settings.Default.Save();
                MessageBox.Show("Icons wurden zugewiesen.");

            }

        }

        public void MonitorGewählt2() 
        {
        }
        public void MonitorGewählt3() 
        {
        }
        public void MonitorGewählt4() 
        {
        }



        public class EinzelnesIconHinzufügen
        {
            public void Vergleich(int mNummer)
            {
                String geWählteIcons = Properties.Settings.Default.DeskIconPfadMTemp;
                string[] geWählteIconsArray = geWählteIcons.Split(';');
                string vorhandeneIcons = "";

                switch (mNummer)
                {
                    case 1:
                        vorhandeneIcons = Properties.Settings.Default.eMonitorIconsZugewiesen1;
                        break;
                    case 2:
                        vorhandeneIcons = Properties.Settings.Default.eMonitorIconsZugewiesen2;
                        break;
                    case 3:
                        vorhandeneIcons = Properties.Settings.Default.eMonitorIconsZugewiesen3;
                        break;
                    case 4:
                        vorhandeneIcons = Properties.Settings.Default.eMonitorIconsZugewiesen4;
                        break;
                }

                foreach (var icon in geWählteIconsArray)
                {
                    if (!string.IsNullOrEmpty(icon) && vorhandeneIcons.Contains(icon))
                    {
                        MessageBox.Show($"Fehler: Das Icon '{icon}' ist bereits zugewiesen.", "Übereinstimmung gefunden", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Beendet die Methode, wenn eine Übereinstimmung gefunden wurde
                    }else
                    {
                        // Hinzufügen des neuen Icons zum vorhandenen String, getrennt durch ";"
                        vorhandeneIcons = vorhandeneIcons + (string.IsNullOrEmpty(vorhandeneIcons) ? "" : ";") + icon;

                        switch (mNummer)
                        {
                            case 1:
                                Properties.Settings.Default.eMonitorIconsZugewiesen1 = vorhandeneIcons;
                                Properties.Settings.Default.eMonitorIconsCount1 += 1;
                                break;
                            case 2:
                                Properties.Settings.Default.eMonitorIconsZugewiesen2 = vorhandeneIcons;
                                Properties.Settings.Default.eMonitorIconsCount2 += 1;
                                break;
                            case 3:
                                Properties.Settings.Default.eMonitorIconsZugewiesen3 = vorhandeneIcons;
                                Properties.Settings.Default.eMonitorIconsCount3 += 1;
                                break;
                            case 4:
                                Properties.Settings.Default.eMonitorIconsZugewiesen4 = vorhandeneIcons;
                                Properties.Settings.Default.eMonitorIconsCount4 += 1;
                                break;
                        }
                    }
                    Properties.Settings.Default.Save();


                }

                // Fügen Sie hier die Logik zum Hinzufügen der Icons hinzu, falls keine Übereinstimmungen gefunden wurden
            }

        }


    }
}
