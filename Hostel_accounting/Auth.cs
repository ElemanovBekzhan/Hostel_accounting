using System;
using System.Drawing;
using System.Windows.Forms;

namespace Hostel_accounting
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "kg312" & textBox2.Text == "12345678" )
            {
                textBox1.Text = "";
                textBox2.Text = "";
                Form1 menu = new Form1();
                menu.Show();
            }
            else
            {
                MessageBox.Show("Неверный пароль или логин");
            }
        }

        private void Auth_Load(object sender, EventArgs e)
        {
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            int x = (screenBounds.Width - this.ClientSize.Width) / 2;
            int y = (screenBounds.Height - this.ClientSize.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
        }
    }
}