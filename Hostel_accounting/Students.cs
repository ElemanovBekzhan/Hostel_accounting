using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;

namespace Hostel_accounting
{
    public partial class Students : Form
    {
        private DataBase dataBase = new DataBase();
        private DataTable Table, Table1 = null;
        private SqlDataAdapter adapter, adapter1 = null;
        
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

        private void studentsBindingNavigatorSaveItem_Click(object sender, System.EventArgs e)
        {
            this.Validate();
            this.studentsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dormitoryDataSet);
     
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Данные не введены");
            }
            else
            {
                try
                {
                    dataBase.openConnection();

                    string quary = "INSERT INTO Students (FIO, BirthDate, PhoneNumber, Email, StudentCardNumber,FacultiesId) VALUES('" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "' , '" + textBox5.Text + "' ,'" + textBox6.Text + "' , '" + comboBox1.SelectedValue + "')";
                    SqlCommand cmd = new SqlCommand(quary, dataBase.getConnection());
                    cmd.ExecuteNonQuery();
                    Table.Clear();
                    adapter.Fill(Table);
                    dataGridView1.DataSource = Table;
                    textBox2.Text = "";
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

                private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
                 {
           
                
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

                }

            private void textBox1_TextChanged(object sender, EventArgs e)
            {
                textBox1.Visible=false;
            }

            private void Students_Load(object sender, System.EventArgs e)
            {
                dataBase.openConnection();
                string query = "SELECT dbo.Students.StudentId, dbo.Students.FIO, dbo.Students.BirthDate, dbo.Students.PhoneNumber, dbo.Students.Email, dbo.Students.StudentCardNumber, dbo.Faculties.FacultiesName FROM dbo.Students INNER JOIN dbo.Faculties ON dbo.Students.FacultiesId = dbo.Faculties.FacultiesId";
                adapter = new SqlDataAdapter(query, dataBase.getConnection());
                Table = new DataTable();
                adapter.Fill(Table);
                dataGridView1.DataSource = Table;
                dataGridView1.Columns["StudentId"].Visible = false;
                dataGridView1.Columns["StudentId"].ReadOnly = true;
                textBox1.Visible = false;
                Load_combobox();
            }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Данные не выбраны");
                }
                else
                {
                    try
                    {
                        dataBase.openConnection();
                        string quary = "DELETE FROM Students WHERE StudentID ='" + textBox1.Text + "';";

                        SqlCommand cmd = new SqlCommand(quary, dataBase.getConnection());
                        cmd.ExecuteNonQuery();
                        Table.Clear();
                        adapter.Fill(Table);
                        dataGridView1.DataSource = Table;
                        textBox3.Text = "";

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

        private void button2_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();
            string quary = "UPDATE  Students  set FIO = '" + textBox2.Text + "' , BirthDate = '" + textBox3.Text + "', PhoneNumber = '" + textBox4.Text + "' , Email =  '" + textBox5.Text + "', StudentCardNumber = '" + textBox6.Text + "',FacultiesId = '" + comboBox1.SelectedValue + "' WHERE StudentID ='" + textBox1.Text + "';";
            SqlCommand cmd = new SqlCommand(quary, dataBase.getConnection());
            cmd.ExecuteNonQuery();
            Table.Clear();
            adapter.Fill(Table);
            dataGridView1.DataSource = Table;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Load_combobox()
        {
            string sql = "SELECT * FROM Faculties";
            using (SqlCommand cmd = new SqlCommand(sql, dataBase.getConnection()))
            {
                cmd.CommandType = CommandType.Text;
                Table1 = new DataTable();
                adapter1 = new SqlDataAdapter(cmd);
                adapter1.Fill(Table1);
                comboBox1.DataSource = Table1;
                comboBox1.DisplayMember = "FacultiesName";
                comboBox1.ValueMember = "FacultiesID";

            }
        }
            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}