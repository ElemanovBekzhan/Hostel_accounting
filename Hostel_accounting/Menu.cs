using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hostel_accounting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Students students = new Students();
            students.Show();
            /*this.Hide();*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Rooms rooms = new Rooms();
            rooms.Show();
            // this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pyament_history pyamentHistory = new Pyament_history();
            pyamentHistory.Show();
            // this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            int x = (screenBounds.Width - this.ClientSize.Width) / 2;
            int y = (screenBounds.Height - this.ClientSize.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            this.Hide();
        }

        private void оРазработчикеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Developers developers = new Developers();
            developers.Show();
        }
    }
}