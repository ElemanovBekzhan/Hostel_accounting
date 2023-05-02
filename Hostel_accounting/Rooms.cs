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
        


        private void Rooms_Load_1(object sender, EventArgs e)
        {
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            int x = (screenBounds.Width - this.ClientSize.Width) / 2;
            int y = (screenBounds.Height - this.ClientSize.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);

            textBox1.Visible = false;
            textBox1.ReadOnly = true;
            dataBase.openConnection();
            string query = "SELECT * FROM Rooms";
            adapter = new SqlDataAdapter( query, dataBase.getConnection());
            Table = new DataTable();
            adapter.Fill(Table);
            dataGridView1.DataSource = Table;
            dataGridView1.Columns["Roomid"].Visible = false;
            dataGridView1.Columns["Roomid"].ReadOnly = true;
            dataGridView1.Columns[1].HeaderText = "Номер комнаты";
            dataGridView1.Columns[2].HeaderText = "Вместимость";
            dataGridView1.Columns[3].HeaderText = "Статус";
            dataGridView1.Columns[4].HeaderText = "Стоимость";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" & textBox3.Text == "" & textBox4.Text == "" & textBox5.Text == "")
            {
                MessageBox.Show("?????? ?? ???????");
            }
            else
            {
                try
                {
                    dataBase.openConnection();

                    string quary = "INSERT INTO Rooms (RoomNumber, Capacity, Occupied, Rent) VALUES('" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "' , '" + textBox5.Text +"')";
                    SqlCommand cmd = new SqlCommand(quary, dataBase.getConnection());
                    cmd.ExecuteNonQuery();
                    Table.Clear();
                    adapter.Fill(Table);
                    dataGridView1.DataSource = Table;
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" & textBox3.Text == "" & textBox4.Text == "" & textBox5.Text == "")
            {
                MessageBox.Show("?????? ?? ???????");
            }
            else
            {

                try
                {

                    dataBase.openConnection();
                    string quary = "UPDATE  Rooms  SET RoomNumber = '" + textBox2.Text + "' ,Capacity = '" + textBox3.Text +
                                   "', Occupied = '" + textBox4.Text + "' , Rent =  '" + textBox5.Text + "' WHERE RoomId ='" + textBox1.Text + "';";
                    SqlCommand cmd = new SqlCommand(quary, dataBase.getConnection());
                    cmd.ExecuteNonQuery();
                    Table.Clear();
                    adapter.Fill(Table);
                    dataGridView1.DataSource = Table;
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("?? ????????????? ?????? ????????", "????????", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("?????? ?? ???????");
                }
                else
                {
                    try
                    {
                        dataBase.openConnection();
                        string quary = "DELETE FROM Rooms WHERE RoomId ='" + textBox1.Text + "';";

                        SqlCommand cmd = new SqlCommand(quary, dataBase.getConnection());
                        cmd.ExecuteNonQuery();
                        Table.Clear();
                        adapter.Fill(Table);
                        dataGridView1.DataSource = Table;
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
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
            else
            {

            }
        }
    }
}