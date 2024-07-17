using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace App3.Helpers
{
    public static class HelperPowerShell
    {
        public static async Task RunEmbeddedPowerShellScriptAsync()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "App3.Helpers.DOD.ps1";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string scriptContent = await reader.ReadToEndAsync();

                // Temporäre Datei erstellen
                var tempFilePath = Path.GetTempFileName() + ".ps1";
                await File.WriteAllTextAsync(tempFilePath, scriptContent);

                // PowerShell-Skript ausführen
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{tempFilePath}\"",
                    //RedirectStandardOutput = true,
                    //UseShellExecute = false,
                    //CreateNoWindow = true,
                };

                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    //string output = await process.StandardOutput.ReadToEndAsync();
                    //MessageBox.Show(output);
                    process.WaitForExit();
                }

                // Temporäre Datei löschen
                File.Delete(tempFilePath);
            }
        }
    }
}
