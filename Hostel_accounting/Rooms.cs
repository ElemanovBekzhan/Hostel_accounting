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
            DataSet dataSet = new DataSet();
            Table = new DataTable();
            adapter.Fill(Table);
            adapter.Fill(dataSet);
            dataGridView1.DataSource = Table;
            dataGridView1.Columns["Roomid"].Visible = false;
            dataGridView1.Columns["Roomid"].ReadOnly = true;
            dataGridView1.Columns[1].HeaderText = "Номер комнаты";
            dataGridView1.Columns[2].HeaderText = "Заселено";
            dataGridView1.Columns[3].HeaderText = "Вместимость";
            dataGridView1.Columns[4].HeaderText = "Статус";
            dataGridView1.Columns[5].HeaderText = "Стоимость";

            
            dataBase.closeConnection();
        }
        

        private void load_combobox()
        {
            
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            if (dataGridView1.SelectedRows.Count == 1)
            {
                bool value = (bool)dataGridView1.SelectedRows[0].Cells["Occupied"].Value;
                checkBox1.Checked = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox2.Text == "" & textBox3.Text == "" & textBox5.Text == "")
            {
                MessageBox.Show("Данные не введены");
            }
            else
            {
                try
                {
                    dataBase.openConnection();
                    string quary = "INSERT INTO Rooms (RoomNumber, Capacity, Occupied, Rent) VALUES('" + textBox2.Text +
                                   "', '" + textBox3.Text + "', '" + checkBox1.Checked + "' , '" + textBox5.Text +"')";
                    SqlCommand cmd = new SqlCommand(quary, dataBase.getConnection());
                    cmd.ExecuteNonQuery();
                    Table.Clear();
                    adapter.Fill(Table);
                    dataGridView1.DataSource = Table;
                    textBox2.Text = "";
                    textBox3.Text = "";
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
            if (textBox2.Text == "" & textBox3.Text == "" & textBox5.Text == "")
            {
                MessageBox.Show("Данные не введены");
            }
            else
            {

                try
                {
                    
                    dataBase.openConnection();
                    bool value = checkBox1.Checked ? true : false;
                    string quary = "UPDATE  Rooms  SET RoomNumber = '" + textBox2.Text + "' ,Capacity = '" +
                                   textBox3.Text + "', Occupied = '" + "@value" + "' , Rent =  '" + textBox5.Text + "' WHERE RoomId ='" + textBox1.Text + "';";
                    SqlCommand cmd = new SqlCommand(quary, dataBase.getConnection());
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.ExecuteNonQuery();
                    Table.Clear();
                    adapter.Fill(Table);
                    dataGridView1.DataSource = Table;
                    textBox2.Text = "";
                    textBox3.Text = "";
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
            DialogResult result = MessageBox.Show("Удалить данные?", "Окно подтверждения", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Данные не введены");
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            /*if (dataGridView1.SelectedRows.Count == 1)
            {
                bool value = (bool)dataGridView1.SelectedRows[3].Cells["Occupied"].Value;
                checkBox1.Checked = value;
            }*/
        }
    }
}