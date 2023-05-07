using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Hostel_accounting
{
    public partial class Pyament_history : Form
    {
        private DataBase dataBase = new DataBase();
        private DataTable Table, Table1 = null;
        private SqlDataAdapter adapter, adapter1 = null;
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
            
            
            dataBase.openConnection();
            string query = "SELECT dbo.Payments.PaymentId, dbo.Students.FIO, dbo.Rooms.RoomNumber, dbo.Payments.PaymentAmount, dbo.Payments.PaymentDate, dbo.Payments.Amount FROM dbo.Students INNER JOIN dbo.Rooms ON dbo.Students.Room_residence = dbo.Rooms.RoomId INNER JOIN dbo.Payments ON dbo.Students.StudentId = dbo.Payments.StudentId AND dbo.Rooms.RoomId = dbo.Payments.RoomsId";
            adapter = new SqlDataAdapter(query, dataBase.getConnection());
            Table = new DataTable();
            adapter.Fill(Table);
            dataGridView1.DataSource = Table;
            dataGridView1.Columns["PaymentId"].Visible = false;
            dataGridView1.Columns["PaymentId"].ReadOnly = true;
            textBox1.Visible = false;
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Номер комнаты";
            dataGridView1.Columns[3].HeaderText = "Стоимость комнаты";
            dataGridView1.Columns[4].HeaderText = "Дата оплаты";
            dataGridView1.Columns[5].HeaderText = "Сумма оплаты";
            Load_ComboBox1();
            Load_ComboBox2();
        }

        private void Load_ComboBox1()
        {
            string sql = "SELECT * FROM Students";
            using (SqlCommand cmd = new SqlCommand(sql, dataBase.getConnection()))
            {
                cmd.CommandType = CommandType.Text;
                Table1 = new DataTable();
                adapter1 = new SqlDataAdapter(cmd);
                adapter1.Fill(Table1);
                comboBox1.DataSource = Table1;
                comboBox1.DisplayMember = "FIO";
                comboBox1.ValueMember = "StudentId";

            }
        }
        private void Load_ComboBox2()
        {
            string sql = "SELECT * FROM Rooms";
            using (SqlCommand cmd = new SqlCommand(sql, dataBase.getConnection()))
            {
                cmd.CommandType = CommandType.Text;
                Table1 = new DataTable();
                adapter1 = new SqlDataAdapter(cmd);
                adapter1.Fill(Table1);
                comboBox2.DataSource = Table1;
                comboBox2.DisplayMember = "RoomNumber";
                comboBox2.ValueMember = "RoomId";

            }
        }

        private void Pyament_history_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*e.Cancel = true;
            Form1 menu = new Form1();
            menu.Show();
            this.Hide();*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("Данные не введены", "Ошибка" ,MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    dataBase.openConnection();
                    string quary =
                            "INSERT INTO Payments (StudentId,RoomsId, PaymentAmount ,PaymentDate, Amount) VALUES(@FIO, @RoomNumber,(SELECT Rent FROM Rooms WHERE @RoomNumber = RoomNumber), @PaymentDate, @Amount)";
                        SqlCommand cmd1 = new SqlCommand(quary, dataBase.getConnection());
                        cmd1.Parameters.AddWithValue("@FIO", comboBox1.SelectedValue);
                        cmd1.Parameters.AddWithValue("@RoomNumber", comboBox2.SelectedValue);
                        cmd1.Parameters.AddWithValue("@PaymentDate", DateTime.Today);
                        cmd1.Parameters.AddWithValue("@Amount", textBox5.Text);
                        cmd1.ExecuteNonQuery();
                        Table.Clear();
                        adapter.Fill(Table);
                        dataGridView1.DataSource = Table;
                        textBox5.Text = "";
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
                finally
                {
                    dataBase.closeConnection();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*string StudentName = comboBox1.SelectedIndex.ToString();
            string query = "SELECT RoomNumber FROM Rooms WHERE FIO = @name";
            SqlCommand cmd = new SqlCommand(query, dataBase.getConnection());
            cmd.Parameters.AddWithValue("@name", StudentName);
            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                int RoomNumber = Convert.ToInt32(result);
                comboBox2.Items.Clear();
                comboBox2.Items.Add(RoomNumber.ToString());
            }*/
        }
    }
}