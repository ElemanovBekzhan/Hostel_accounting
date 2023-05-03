using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hostel_accounting
{
    public partial class Pyament_history : Form
    {
        public Pyament_history()
        {
            InitializeComponent();
        }

        private void Pyament_history_Load(object sender, EventArgs e)
        {
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            int x = (screenBounds.Width - this.ClientSize.Width) / 2;
            int y = (screenBounds.Height - this.ClientSize.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
            
        }

        private void Pyament_history_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*e.Cancel = true;
            Form1 menu = new Form1();
            menu.Show();
            this.Hide();*/
        }
    }
}