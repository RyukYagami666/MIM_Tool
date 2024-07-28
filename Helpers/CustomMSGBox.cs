using App3.Funktions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using App3.Views;



namespace App3.Helpers
{
    internal class CustomMSGBox : Form
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Label messageLabel;

        public CustomMSGBox(string message, string title, string buttonLabel1, string buttonLabel2, string buttonLabel3)
        {
            this.Text = title;
            this.Width = 1000;
            this.Height = 450;
            this.BackColor = Color.Gray;
            //this.FormBorderStyle = FormBorderStyle.None;


            messageLabel = new Label();
            messageLabel.Text = message;
            messageLabel.Top = 20;
            messageLabel.Left = 20;
            //messageLabel.Width = 800;
            //messageLabel.Height = 700;
            messageLabel.AutoSize = true;
            messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Controls.Add(messageLabel);



            button1 = new Button();
            button1.Text = buttonLabel1;
            button1.Top = 300;
            button1.Left = 60;
            button1.AutoSize = true;
            button1.Click += new EventHandler(button1_Click);
            this.Controls.Add(button1);

            button2 = new Button();
            button2.Text = buttonLabel2;
            button2.Top = button1.Top;
            button2.Left = button1.Left + button1.Width + 20;
            button2.AutoSize = true;
            button2.Click += new EventHandler(button2_Click);
            this.Controls.Add(button2);

            button3 = new Button();
            button3.Text = buttonLabel3;
            button3.Top = button1.Top;
            button3.Left = button2.Left + button2.Width + 20;
            button3.AutoSize = true;
            button3.Click += new EventHandler(button3_Click);
            this.Controls.Add(button3);
        }
        public static DialogResult Show(string message, string title, string buttonLabel1, string buttonLabel2, string buttonLabel3)
        {
            CustomMSGBox customMessageBox = new CustomMSGBox(message, title, buttonLabel1, buttonLabel2, buttonLabel3);
            return customMessageBox.ShowDialog();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        
    }
 
}
