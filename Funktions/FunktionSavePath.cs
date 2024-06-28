using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Windows.Devices.Geolocation;
using App3.Funktions;


namespace App3.Funktions
{
    class FunktionSavePath
    {
        
        public string GetNewSavePath()
        {
            // Dialog zum Auswählen eines Pfades öffnen
            var dialog = new OpenFileDialog
            {
                CheckFileExists = false,
                CheckPathExists = true,
                ValidateNames = false,
                FileName = "Ordnerauswahl"
            };
            if (dialog.ShowDialog() == true)
            {
                // Rückgabe des ausgewählten Pfades
                return Path.GetDirectoryName(dialog.FileName);
            }
            // Rückgabe von null, wenn kein Pfad ausgewählt wurde
            return null;
        }
    }
}
