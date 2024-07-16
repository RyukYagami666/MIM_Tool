using System.Management;
using System.Windows.Forms;



namespace App3.Funktions
{
    class FunktionDeskScen
    {
        string[][] esSeMonitor = new string[Screen.AllScreens.Length][];


        public void AuslesenMonitoreUndPositionen()
        {
            int i = 0;


            foreach (var screen in Screen.AllScreens)
            {
                // Gibt an, ob der Monitor der primäre Monitor ist
                bool monitPrimary = screen.Primary;
                string monitNum = screen.DeviceName.ToString();
                
                Type monitType = screen.GetType();
                // Die Position und Größe des Monitors
                var monitBounds = screen.Bounds;

                // Die Arbeitsbereichsgröße des Monitors (ohne Taskleiste und andere Desktop-Elemente)
                var workingArea = screen.WorkingArea;

                CopyMSGBox.Show($"Monitor Name: {monitNum} als {(monitPrimary ? "Primär" : "Sekundär")}, Position: {monitBounds.X}, {monitBounds.Y}, Größe: {monitBounds.Width}x{monitBounds.Height}, Arbeitsbereich: " +
                    $"{workingArea.Width}x{workingArea.Height}");



                if (i <= 3 || i >= 0)
                {
                    esSeMonitor[i] = new string[] {"Richtiger Name", "", "Monitor Nummer",monitNum, "Primär/Sekundär",  (monitPrimary ? "Primär" : "Sekundär") , "Position - X", Convert.ToString(monitBounds.X), "Position - Y", Convert.ToString(monitBounds.Y), "Größe - Width",
                    Convert.ToString(monitBounds.Width), "Größe - Height", Convert.ToString(monitBounds.Height), "Arbeitsbereich - Width", Convert.ToString(workingArea.Width), "Arbeitsbereich - Height",  Convert.ToString(workingArea.Height) };
                    i++;
                }
                else
                {
                    i = 0;
                }



            }
            GetMonitorName();
        }
        public void GetMonitorName()
        {
            int i = 0;
            int iz = 0;
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\WMI",
                    "SELECT * FROM WmiMonitorID");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    i++;

                    if (queryObj["UserFriendlyName"] == null)
                    {
                        CopyMSGBox.Show("UserFriendlyName: Nicht verfügbar");
                    }
                    else
                    {
                        // Konvertieren des UInt16-Arrays in einen lesbaren String
                        UInt16[] arrUserFriendlyName = (UInt16[])(queryObj["UserFriendlyName"]);
                        string monitName = new string(Array.ConvertAll(arrUserFriendlyName, item => Convert.ToChar(item)));
                        CopyMSGBox.Show($"UserFriendlyName: {monitName}");

                        if (iz < esSeMonitor.Length && monitName != null && monitName != "")
                        {

                            esSeMonitor[iz][1] = monitName;
                            iz++;
                        }
                        else
                        {
                            iz = 0;
                            CopyMSGBox.Show("UserFriendlyName: Nicht verfügbar");
                        }

                    }
                    // Verhindern, dass iz die Länge von esSeMonitor überschreitet
                    if (iz >= esSeMonitor.Length +2)
                    {
                        CopyMSGBox.Show("Alarm");
                    }
                }
            }
            catch (ManagementException e)
            {
                CopyMSGBox.Show("An error occurred while querying for WMI data: " + e.Message);
            }
            CheckAndSaveMonitorData();

        }


        public void CheckAndSaveMonitorData()
        {
            // Überprüfen, ob es Monitore gibt, um zu speichern
            if (esSeMonitor.Length == 0)
            {
                MessageBox.Show("Keine Monitore gefunden. Speichern nicht möglich.");
                return; // Abbruch, da keine Daten zu speichern sind
            }

            // Benutzer fragen, ob die Daten gespeichert werden sollen
            var result = MessageBox.Show("Möchten Sie die Monitordaten speichern?", "Daten speichern", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Überprüfen der Benutzerantwort
            if (result == DialogResult.Yes)
            {
                SaveMonitorData(); // Speichern der Daten, wenn der Benutzer zustimmt
            }
            else
            {
                MessageBox.Show("Speichern abgebrochen."); // Benachrichtigung, dass das Speichern abgebrochen wurde
            }
        }

        public void SaveMonitorData()
        {
            {
                if (esSeMonitor.Length == 0)
                {
                    CopyMSGBox.Show("Keine Monitore gefunden.");
                    return;
                }

                for (int i = 0; i < esSeMonitor.Length; i++)
                {
                    if (i >= 4) // Begrenzung auf 4 Monitore, wie in Ihrem ursprünglichen Code
                    {
                        Console.WriteLine("Keine Daten vorhanden");
                        break;
                    }

                    Console.WriteLine($"Case {i + 1}");

                    string infoMonitor = string.Join(";", esSeMonitor[i]);
                    switch (i)
                    {
                        case 0:
                            Properties.Settings.Default.InfoMonitor1 = infoMonitor;
                            break;
                        case 1:
                            Properties.Settings.Default.InfoMonitor2 = infoMonitor;
                            break;
                        case 2:
                            Properties.Settings.Default.InfoMonitor3 = infoMonitor;
                            break;
                        case 3:
                            Properties.Settings.Default.InfoMonitor4 = infoMonitor;
                            break;
                    }
                    Properties.Settings.Default.Save();
                    CopyMSGBox.Show(infoMonitor);
                }
            }


        }


    }


}

////Wertte aus ainstellung lesen
//
//string meineEinstellungWert = Properties.Settings.Default.MeineEinstellung;
//
////Wert in Einstellung schreiben und speichern
//
//Properties.Settings.Default.MeineEinstellung = "Neuer Wert";
//Properties.Settings.Default.Save();