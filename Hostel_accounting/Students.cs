using System.Windows.Forms;

namespace Hostel_accounting
{
    public partial class Students : Form
    {
        public Students()
        {
            InitializeComponent();
        }

        private void Students_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*e.Cancel = true;
            Form1 menu = new Form1();
            menu.Show();
            this.Hide();*/
        }
    }
}