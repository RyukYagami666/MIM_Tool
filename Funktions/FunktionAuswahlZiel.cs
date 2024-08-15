using MIM_Tool.Properties;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MIM_Tool.Helpers;
using MIM_Tool.Views;

namespace MIM_Tool.Funktions
{
    class FunktionAuswahlZiel
    {
        
        public void MonitorGewählt1()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen1))
            {
                var result = CustomMSGBox.Show("Es sind schon Icons zugewiesen in 1!\nZugewiesener Strin: " + Properties.Settings.Default.eMonitorIconsZugewiesen1 + "\nWählen Sie eine Option:", "Aktion wählen!", "Alle Überschreiben", "Einzeln hinzufügen","Abbrechen");

                switch (result)
                {
                    case DialogResult.Yes:
                    // Logik zum Überschreiben aller Icons
                        Properties.Settings.Default.eMonitorIconsZugewiesen1 = Properties.Settings.Default.DeskIconPfadMTemp;
                        Properties.Settings.Default.eMonitorIconsCount1 = Properties.Settings.Default.eMonitorIconsCountTemp;
                        MessageBox.Show("Alle Icons wurden überschrieben.");
                        break;
                    case DialogResult.No:
                        int mNummer = 1;
                        MessageBox.Show("einzelne Icons werden hinzu.");
                        EinzelnesIconHinzufügen einzelnesIconHinzufügen = new EinzelnesIconHinzufügen();
                        einzelnesIconHinzufügen.Vergleich(mNummer);
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("Aktion abgebrochen.");
                        break;
                }
            }
            else
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen1 = Properties.Settings.Default.DeskIconPfadMTemp;
                Properties.Settings.Default.eMonitorIconsCount1 = Properties.Settings.Default.eMonitorIconsCountTemp;
                MessageBox.Show("Icons wurden zugewiesen.");
            }
            Properties.Settings.Default.DeskIconPfadMTemp = "";
            Properties.Settings.Default.eMonitorIconsCountTemp = 0;
            Properties.Settings.Default.Save();
        }

        public void MonitorGewählt2() 
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen2))
            {
                var result = CustomMSGBox.Show("Es sind schon Icons zugewiesen in 2! \nZugewiesener Strin: " + Properties.Settings.Default.eMonitorIconsZugewiesen2 + " \nWählen Sie eine Option:", "Aktion wählen", "Alle Überschreiben", "Einen hinzufügen", "Abbrechen");

                switch (result)
                {
                    case DialogResult.Yes:
                        // Logik zum Überschreiben aller Icons
                        Properties.Settings.Default.eMonitorIconsZugewiesen2 = Properties.Settings.Default.DeskIconPfadMTemp;
                        Properties.Settings.Default.eMonitorIconsCount2 = Properties.Settings.Default.eMonitorIconsCountTemp;
                        MessageBox.Show("Alle Icons wurden überschrieben.");
                        break;
                    case DialogResult.No:
                        int mNummer = 2;
                        MessageBox.Show("einzelne Icons werden hinzu.");
                        EinzelnesIconHinzufügen einzelnesIconHinzufügen = new EinzelnesIconHinzufügen();
                        einzelnesIconHinzufügen.Vergleich(mNummer);
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("Aktion abgebrochen.");
                        break;
                }
            }
            else
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen2 = Properties.Settings.Default.DeskIconPfadMTemp;
                Properties.Settings.Default.eMonitorIconsCount2 = Properties.Settings.Default.eMonitorIconsCountTemp;
                MessageBox.Show("Icons wurden zugewiesen.");
            }
            Properties.Settings.Default.DeskIconPfadMTemp = "";
            Properties.Settings.Default.eMonitorIconsCountTemp = 0;
            Properties.Settings.Default.Save();
        }

        public void MonitorGewählt3() 
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen3))
            {
                var result = CustomMSGBox.Show("Es sind schon Icons zugewiesen in 3! \nZugewiesener Strin: " + Properties.Settings.Default.eMonitorIconsZugewiesen3 + " \nWählen Sie eine Option:", "Aktion wählen", "Alle Überschreiben", "Einen hinzufügen", "Abbrechen");

                switch (result)
                {
                    case DialogResult.Yes:
                        // Logik zum Überschreiben aller Icons
                        Properties.Settings.Default.eMonitorIconsZugewiesen3 = Properties.Settings.Default.DeskIconPfadMTemp;
                        Properties.Settings.Default.eMonitorIconsCount3 = Properties.Settings.Default.eMonitorIconsCountTemp;
                        MessageBox.Show("Alle Icons wurden überschrieben.");
                        break;
                    case DialogResult.No:
                        int mNummer = 3;
                        MessageBox.Show("einzelne Icons werden hinzu.");
                        EinzelnesIconHinzufügen einzelnesIconHinzufügen = new EinzelnesIconHinzufügen();
                        einzelnesIconHinzufügen.Vergleich(mNummer);
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("Aktion abgebrochen.");
                        break;
                }
            }
            else
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen3 = Properties.Settings.Default.DeskIconPfadMTemp;
                Properties.Settings.Default.eMonitorIconsCount3 = Properties.Settings.Default.eMonitorIconsCountTemp;
                MessageBox.Show("Icons wurden zugewiesen.");
            }
            Properties.Settings.Default.DeskIconPfadMTemp = "";
            Properties.Settings.Default.eMonitorIconsCountTemp = 0;
            Properties.Settings.Default.Save();
        }

        public void MonitorGewählt4() 
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.eMonitorIconsZugewiesen4))
            {
                var result = CustomMSGBox.Show("Es sind schon Icons zugewiesen in 4!\nZugewiesener Strin: " + Properties.Settings.Default.eMonitorIconsZugewiesen4 + "\nWählen Sie eine Option:", "Aktion wählen", "Alle Überschreiben", "Einen hinzufügen", "Abbrechen");

                switch (result)
                {
                    case DialogResult.Yes:
                        // Logik zum Überschreiben aller Icons
                        Properties.Settings.Default.eMonitorIconsZugewiesen4 = Properties.Settings.Default.DeskIconPfadMTemp;
                        Properties.Settings.Default.eMonitorIconsCount4 = Properties.Settings.Default.eMonitorIconsCountTemp;
                        MessageBox.Show("Alle Icons wurden überschrieben.");
                        break;
                    case DialogResult.No:
                        int mNummer = 4;
                        MessageBox.Show("einzelne Icons werden hinzu.");
                        EinzelnesIconHinzufügen einzelnesIconHinzufügen = new EinzelnesIconHinzufügen();
                        einzelnesIconHinzufügen.Vergleich(mNummer);
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("Aktion abgebrochen.");
                        break;
                }
            }
            else
            {
                Properties.Settings.Default.eMonitorIconsZugewiesen4 = Properties.Settings.Default.DeskIconPfadMTemp;
                Properties.Settings.Default.eMonitorIconsCount4 = Properties.Settings.Default.eMonitorIconsCountTemp;
                MessageBox.Show("Icons wurden zugewiesen.");
            }
            Properties.Settings.Default.DeskIconPfadMTemp = "";
            Properties.Settings.Default.eMonitorIconsCountTemp = 0;
            Properties.Settings.Default.Save();
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
                        MessageBox.Show($"Das Icon '{icon}' wurde erfolgreich hinzugefügt.", "Keine Übereinstimmung gefunden", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    


                }
            }

        }


    }
}
