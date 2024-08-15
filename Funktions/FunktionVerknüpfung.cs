using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace MIM_Tool.Funktions
{
    internal class FunktionVerknüpfung
    {
        public void VerknüpfungStart()
        {
            
            if (Properties.Settings.Default.eMonitorVorhanden1)
            {
                string data = Properties.Settings.Default.InfoMonitor1;
                string[] splitData = data.Split(';');
                string monitorID = "MIM_" + splitData[1] + "_" + splitData[17];
                ErstelleVerknüpfung(monitorID,"0");
            }
            if (Properties.Settings.Default.eMonitorVorhanden2)
            {
                string data = Properties.Settings.Default.InfoMonitor2;
                string[] splitData = data.Split(';');
                string monitorID = "MIM_" + splitData[1] + "_" + splitData[17];
                ErstelleVerknüpfung(monitorID, "1");
            }
            if (Properties.Settings.Default.eMonitorVorhanden3)
            {
                string data = Properties.Settings.Default.InfoMonitor3;
                string[] splitData = data.Split(';');
                string monitorID = "MIM_" + splitData[1] + "_" + splitData[17];
                ErstelleVerknüpfung(monitorID, "2");
            }
            if (Properties.Settings.Default.eMonitorVorhanden4)
            {
                string data = Properties.Settings.Default.InfoMonitor4;
                string[] splitData = data.Split(';');
                string monitorID = "MIM_" + splitData[1] + "_" + splitData[17];
                ErstelleVerknüpfung(monitorID, "3");
            }


        }
        public void ErstelleVerknüpfung(string verknüpfungsName, string argumente)
        {
            string desktopPfad = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string verknüpfungsPfad = System.IO.Path.Combine(desktopPfad, verknüpfungsName + ".lnk");
            string zielPfad = Application.ExecutablePath;
            WshShell shell = new WshShell();
            IWshShortcut verknüpfung = (IWshShortcut)shell.CreateShortcut(verknüpfungsPfad);
            verknüpfung.TargetPath = zielPfad;
            verknüpfung.Arguments = argumente;
            verknüpfung.Save();

        }
    }
}
