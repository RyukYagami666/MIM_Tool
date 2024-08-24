using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace MIM_Tool.Helpers
{

    public static class WarnMSGBox
    {
        public static void Show(string text, int timeout = 3000)
        {
            Form form = new Form()
            {
                Width = 900,
                Height = 300,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Warning!",
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label label = new Label()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Text = text,
                BackColor = Color.OrangeRed,  // Hintergrundfarbe ändern
                Font = new Font("Arial", 14, FontStyle.Regular)
            };
            form.Controls.Add(label);

            Button okButton = new Button()
            {
                Text = "OK",
                Dock = DockStyle.Bottom,
                DialogResult = DialogResult.OK,
                Height = 40,    
                FlatStyle = FlatStyle.Flat, // Flacher Stil
                Font = new Font("Arial", 9, FontStyle.Bold) ,// Schriftgröße und Stil ändern
            };

            form.Controls.Add(okButton);
            form.AcceptButton = okButton; // Erlaubt die Bestätigung mit der Enter-Taste

            System.Timers.Timer timer = new System.Timers.Timer(timeout);
            timer.Elapsed += (sender, e) => form.Invoke(new Action(() => form.Close()));
            timer.AutoReset = false;
            timer.Start();

            // Stil/Thema vom Hauptfenster übernehmen
            form.BackColor = SystemColors.Control;
            form.Font = SystemFonts.DefaultFont;

            form.ShowDialog();
        }
    }
}
