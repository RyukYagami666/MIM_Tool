using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIM_Tool.Helpers
{
    public class InputMSGBox : Form
    {
        private TextBox inputTextBox;
        private Button okButton;
        private Label messageLabel;

        public string InputText { get; private set; }

        public InputMSGBox(string message, string title)
        {
            Text = title;
            Width = 1000;
            Height = 800;


            messageLabel = new Label();
            messageLabel.Text = message;
            messageLabel.Top = 20;
            messageLabel.Left = 20;
            //messageLabel.Width = 800;
            //messageLabel.Height = 700;
            messageLabel.AutoSize = true;
            messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            Controls.Add(messageLabel);

            inputTextBox = new TextBox();
            inputTextBox.Top = 600;
            inputTextBox.Left = 20;
            inputTextBox.Width = 800;
            inputTextBox.KeyDown += new KeyEventHandler(InputTextBox_KeyDown);
            Controls.Add(inputTextBox);

            okButton = new Button();
            okButton.Text = "OK";
            okButton.Top = 600;
            okButton.Left = 830;
            okButton.Height = inputTextBox.Height;
            okButton.Click += new EventHandler(OkButton_Click);
            Controls.Add(okButton);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            InputText = inputTextBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Verhindert das Hinzufügen eines neuen Zeilenumbruchs
                OkButton_Click(sender, e); // Simuliert das Klicken der OK-Schaltfläche
            }
        }

        public static string Show(string message, string title)
        {
            using (InputMSGBox inputBox = new InputMSGBox(message, title))
            {
                if (inputBox.ShowDialog() == DialogResult.OK)
                {
                    return inputBox.InputText;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
