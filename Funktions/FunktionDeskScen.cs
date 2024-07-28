using System.Management;
using System.Windows.Forms;
using App3.Helpers;
using Windows.UI.Accessibility;



namespace App3.Funktions
{
    class FunktionDeskScen
    {
        
        string[][] esSeMonitor;
        string monitorListen;

        public void AuslesenMonitoreUndPositionen()
        {
            var screens = Screen.AllScreens;
            esSeMonitor = new string[Screen.AllScreens.Length][];
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

                CopyMSGBox.Show($"Monitor Name: {monitType} als {(monitPrimary ? "Primär" : "Sekundär")}, Position: {monitBounds.X}, {monitBounds.Y}, Größe: {monitBounds.Width}x{monitBounds.Height}, Arbeitsbereich: " +
                    $"{workingArea.Width}x{workingArea.Height}");



                if (i <= 3 || i >= 0)
                {
                    esSeMonitor[i] = new string[] {"Richtiger Name",            //0
                        "",                                                     //1
                        "Monitor Nummer",                                       //2
                        "",                                                     //3
                        "Primär/Sekundär",                                      //4
                        (monitPrimary ? "Primär" : "Sekundär") ,                //5
                        "Position - X",                                         //6
                        Convert.ToString(monitBounds.X),                        //7
                        "Position - Y",                                         //8
                        Convert.ToString(monitBounds.Y),                        //9
                        "Größe - Width",                                        //10
                        Convert.ToString(monitBounds.Width),                    //11
                        "Größe - Height",                                       //12
                        Convert.ToString(monitBounds.Height),                   //13
                        "Arbeitsbereich - Width",                               //14
                        Convert.ToString(workingArea.Width),                    //15
                        "Arbeitsbereich - Height",                              //16
                        Convert.ToString(workingArea.Height) };                 //17
                    i++;
                }
                else
                {
                    i = 0;
                }



            }
            switch (esSeMonitor.Length)
            {
                case 0:
                    monitorListen = "fehler";
                    break;
                case 1:
                    monitorListen = "\n\nMonitor Daten 1: ist ein " + esSeMonitor[0][5] + " Monitor; \nPosition(" + esSeMonitor[0][7] + "," + esSeMonitor[0][9] + "); Größe(" + esSeMonitor[0][11] + "," + esSeMonitor[0][13] +")";
                    break;
                case 2:
                    monitorListen = "\n\nMonitor Daten 1: ist ein " + esSeMonitor[0][5] + " Monitor; \nPosition(" + esSeMonitor[0][7] + "," + esSeMonitor[0][9] + "); Größe(" + esSeMonitor[0][11] + "," + esSeMonitor[0][13] + ")" +
                        "\n\nMonitor Daten 2: ist ein " + esSeMonitor[1][5] + " Monitor; \nPosition(" + esSeMonitor[1][7] + "," + esSeMonitor[1][9] + "); Größe(" + esSeMonitor[1][11] + "," + esSeMonitor[1][13] + ")";
                    break;
                case 3:
                    monitorListen = "\n\nMonitor Daten 1: ist ein " + esSeMonitor[0][5] + " Monitor; \nPosition(" + esSeMonitor[0][7] + "," + esSeMonitor[0][9] + "); Größe(" + esSeMonitor[0][11] + "," + esSeMonitor[0][13] + ")" +
                        "\n\nMonitor Daten 2: ist ein " + esSeMonitor[1][5] + " Monitor; \nPosition(" + esSeMonitor[1][7] + "," + esSeMonitor[1][9] + "); Größe(" + esSeMonitor[1][11] + "," + esSeMonitor[1][13] + ")" +
                        "\n\nMonitor Daten 3: ist ein " + esSeMonitor[2][5] + " Monitor; \nPosition(" + esSeMonitor[2][7] + "," + esSeMonitor[2][9] + "); Größe(" + esSeMonitor[2][11] + "," + esSeMonitor[2][13] + ")";
                    break;
                case 4:
                    monitorListen = "\n\nMonitor Daten 1: ist ein " + esSeMonitor[0][5] + " Monitor; \nPosition(" + esSeMonitor[0][7] + "," + esSeMonitor[0][9] + "); Größe(" + esSeMonitor[0][11] + "," + esSeMonitor[0][13] + ")" +
                        "\n\nMonitor Daten 2: ist ein " + esSeMonitor[1][5] + " Monitor; \nPosition(" + esSeMonitor[1][7] + "," + esSeMonitor[1][9] + "); Größe(" + esSeMonitor[1][11] + "," + esSeMonitor[1][13] + ")" +
                        "\n\nMonitor Daten 3: ist ein " + esSeMonitor[2][5] + " Monitor; \nPosition(" + esSeMonitor[2][7] + "," + esSeMonitor[2][9] + "); Größe(" + esSeMonitor[2][11] + "," + esSeMonitor[2][13] + ")" +
                        "\n\nMonitor Daten 4: ist ein " + esSeMonitor[3][5] + " Monitor; \nPosition(" + esSeMonitor[3][7] + "," + esSeMonitor[3][9] + "); Größe(" + esSeMonitor[3][11] + "," + esSeMonitor[3][13] + ")";
                    break;
            }//abfrage ob ein Wert in den Einstellungen gespeichert ist 

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
                        monitName = monitName.Replace("\0", "");
                        //CopyMSGBox.Show($"UserFriendlyName: {monitName}");
                        //MessageBox.Show(monitorListen);
                        if (iz < esSeMonitor.Length && monitName != null && monitName != "")
                        {
                            int auswahl = WählenArrayPosition(monitName);
                            if (auswahl >= 0 && auswahl < esSeMonitor.Length)
                            {
                                esSeMonitor[auswahl][1] = monitName;
                                esSeMonitor[auswahl][3] = Convert.ToString(iz + 1);
                            }
                            else
                            {
                                CopyMSGBox.Show("Ungültige Auswahl. Name wird nicht gespeichert.");
                            }
                            //esSeMonitor[iz][1] = monitName;
                            //esSeMonitor[iz][3] = Convert.ToString(iz + 1);
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
        public int WählenArrayPosition(string monitName)
        {
            
            string input = InputMSGBox.Show("Monitor Name " + monitName + " zu Monitor Daten hinzugefügen (1 bis " + esSeMonitor.Length + ")" + monitorListen,"Monitor Name zuweisen ");
            int position;
            if (int.TryParse(input, out position))
            {
                return position -1;
            }
            else
            {
                
                return 10; // Standardposition
            }
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
                    //CopyMSGBox.Show(infoMonitor);
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