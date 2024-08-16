using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MIM_Tool.Helpers
{

    public static class CopyMSGBox
    {
        public static void Show(string text, string title = "")
        {
            Form form = new Form()
            {
                Width = 1000,
                Height = 600,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            TextBox textBox = new TextBox()
            {
                Multiline = true,
                ReadOnly = true,
                Dock = DockStyle.Fill,
                Text = text
            };

            form.Controls.Add(textBox);

            Button okButton = new Button()
            {
                Text = "OK",
                Dock = DockStyle.Bottom,
                DialogResult = DialogResult.OK
            };

            form.Controls.Add(okButton);
            form.AcceptButton = okButton; // Erlaubt die Bestätigung mit der Enter-Taste

            form.ShowDialog();
        }
    }
}
