using System;
using System.Windows.Forms;
using System.Management;
using System.Xml.Linq;
using IniParser;
using IniParser.Model;
using IniParser.Parser; 
using App3.Funktions;



namespace App3.Funktions
{
    class FunktionDeskScen
    {
        public void AuslesenMonitoreUndPositionen()
        {

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

                CopyMSGBox.Show($"Monitor Name: {monitNum} als {(monitPrimary ? "Primär" : "Sekundär")}, Position: {monitBounds.X}, {monitBounds.Y}, Größe: {monitBounds.Width}x{monitBounds.Height}, Arbeitsbereich: {workingArea.Width}x{workingArea.Height}");
               

            }
            GetMonitorName();
        }
        public void GetMonitorName()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\WMI",
                    "SELECT * FROM WmiMonitorID");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    CopyMSGBox.Show("WmiMonitorID instance");

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
                    }
                }
            }
            catch (ManagementException e)
            {
                CopyMSGBox.Show("An error occurred while querying for WMI data: " + e.Message);
            }
        
        }




       
    }


}
