using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Hostel_accounting
{
    public partial class Rooms : Form
    {
        
        private DataBase dataBase = new DataBase();
        private DataTable Table, Table1 = null;
        private SqlDataAdapter adapter, adapter1 = null;
        public Rooms()
        {
            InitializeComponent();
        }

        private void Rooms_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*e.Cancel = true;
            Form1 menu = new Form1();
            menu.Show();
            this.Hide();*/
        }

        // private void Rooms_Load(object sender, EventArgs e)
        // {
        //     Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
        //     int x = (screenBounds.Width - this.ClientSize.Width) / 2;
        //     int y = (screenBounds.Height - this.ClientSize.Height) / 2;
        //     this.StartPosition = FormStartPosition.Manual;
        //     this.Location = new Point(x, y);
        //     
        //     
        //     dataBase.openConnection();
        //     string query = "SELECT * FROM Rooms";
        //     adapter = new SqlDataAdapter( query, dataBase.getConnection());
        //     Table = new DataTable();
        //     adapter.Fill(Table);
        //     dataGridView1.DataSource = Table;
        //     dataGridView1.Columns["Roomid"].Visible = false;
        //     dataGridView1.Columns["Roomid"].ReadOnly = true;
        // }
    }
}