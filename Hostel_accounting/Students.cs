using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;
using System.Drawing;
using System.Linq;

namespace Hostel_accounting
{
    public partial class Students : Form
    {
        private DataBase dataBase = new DataBase();
        private DataTable Table, Table1, Table2 = null;
        private SqlDataAdapter adapter, adapter1, adapter2 = null;
        
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
            if (textBox2.Text == "" || textBox4.Text == "" || textBox5.Text == "" || maskedTextBox3.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Данные не введены");
            }
            else
            {
                try 
                {
                    dataBase.openConnection();
                    string quary =
                            "INSERT INTO Students (FIO, Sex, BirthDate, PhoneNumber, Email, StudentCardNumber,FacultiesId, Passport, Inventory, Data_of_receipt) VALUES('" +
                            textBox2.Text + "', '" + comboBox2.SelectedValue + "','" + dateTimePicker1.Value + "', '" + textBox4.Text + "' , '" +
                            textBox5.Text + "' ,'" + maskedTextBox3.Text + "' ,'" + comboBox1.SelectedValue + "' ,'" + textBox6.Text + "' ,'" + checkBox1.Checked + "' ,'" + dateTimePicker2.Value +"')";
                        SqlCommand cmd1 = new SqlCommand(quary, dataBase.getConnection());
                        cmd1.ExecuteNonQuery();
                        Table.Clear();
                        adapter.Fill(Table);
                        dataGridView1.DataSource = Table;
                        textBox2.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        maskedTextBox3.Text = "";
                        textBox6.Text = "";
                }
                catch (SqlException ex)
                {
                    // Перехват сообщения об ошибке
                    if (ex.Number == 50000 && ex.Class == 16 && ex.State == 1)
                    {
                        string errorMessage = ex.Message;
                        MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Обработка других ошибок
                        /*catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                            throw;
                    
                        }*/
                    }
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
            comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            maskedTextBox3.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            checkBox1.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Visible = false;
        }

        private void Students_Load(object sender, System.EventArgs e)
        {
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            int x = (screenBounds.Width - this.ClientSize.Width) / 2;
            int y = (screenBounds.Height - this.ClientSize.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
            
            
            
            dataBase.openConnection();
            string query = "SELECT dbo.Students.StudentId, dbo.Students.FIO, dbo.Sex.Sex, dbo.Students.BirthDate, dbo.Students.PhoneNumber, dbo.Students.Email, dbo.Students.StudentCardNumber, dbo.Faculties.FacultiesName, dbo.Rooms.RoomNumber," + 
            " dbo.Students.Passport, dbo.Students.Inventory, dbo.Students.Data_of_receipt"+
            " FROM dbo.Sex INNER JOIN" +
            " dbo.Students ON dbo.Sex.ID = dbo.Students.Sex INNER JOIN" +
            " dbo.Rooms ON dbo.Students.Room_residence = dbo.Rooms.RoomId INNER JOIN"+
            " dbo.Faculties ON dbo.Students.FacultiesId = dbo.Faculties.FacultiesId";
            adapter = new SqlDataAdapter(query, dataBase.getConnection());
            Table = new DataTable();
            adapter.Fill(Table);
            dataGridView1.DataSource = Table;
            dataGridView1.Columns["StudentId"].Visible = false;
            dataGridView1.Columns["StudentId"].ReadOnly = true;
            textBox1.Visible = false;
            Load_combobox();
            Load_combobox2();
            dataGridView1.Columns["StudentId"].ReadOnly = true;
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Пол";
            dataGridView1.Columns[3].HeaderText = "Дата рождения";
            dataGridView1.Columns[4].HeaderText = "Телефон";
            dataGridView1.Columns[5].HeaderText = "Почта";
            dataGridView1.Columns[6].HeaderText = "Студенческий номер";
            dataGridView1.Columns[7].HeaderText = "Факультет";
            dataGridView1.Columns[8].HeaderText = "Комната №";
            dataGridView1.Columns[9].HeaderText = "ID Пасспорта";
            dataGridView1.Columns[10].HeaderText = "Инвентарь";
            dataGridView1.Columns[11].HeaderText = "Дата поступления";
        }

        private void Load_combobox2()
        {
            string sql = "SELECT * FROM Sex";
            using (SqlCommand cmd = new SqlCommand(sql, dataBase.getConnection()))
            {
                cmd.CommandType = CommandType.Text;
                Table2 = new DataTable();
                adapter2 = new SqlDataAdapter(cmd);
                adapter2.Fill(Table2);
                comboBox2.DataSource = Table2;
                comboBox2.DisplayMember = "Sex";
                comboBox2.ValueMember = "ID";
            }
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
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" && textBox4.Text == "" && textBox5.Text == "" && maskedTextBox3.Text == "")
            {
                MessageBox.Show("Данные не введены");
            }
            else
            {
                try
                {
                    dataBase.openConnection();
                    string quary = "UPDATE  Students  set FIO = '" + textBox2.Text + "' , Sex = '" + comboBox2.SelectedValue +  "' , BirthDate = '" + dateTimePicker1.Value + "', PhoneNumber = '" + textBox4.Text + "' , Email =  '" + textBox5.Text + "', StudentCardNumber = '" + maskedTextBox3.Text + "',FacultiesId = '" + comboBox1.SelectedValue + "' , Passport = '" + textBox6.Text + "' , Data_of_receipt = '" + dateTimePicker2.Value +  "' WHERE StudentID ='" + textBox1.Text + "';";
                    SqlCommand cmd = new SqlCommand(quary, dataBase.getConnection());
                    cmd.ExecuteNonQuery();
                    Table.Clear();
                    adapter.Fill(Table);
                    dataGridView1.DataSource = Table;
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
            //dataBase.openConnection();
            //string quary = "UPDATE  Students  set FIO = '" + textBox2.Text + "' , BirthDate = '" + maskedTextBox2.Text + "', PhoneNumber = '" + maskedTextBox1.Text + "' , Email =  '" + textBox5.Text + "', StudentCardNumber = '" + maskedTextBox3.Text + "',FacultiesId = '" + comboBox1.SelectedValue + "' WHERE StudentID ='" + textBox1.Text + "';";
            //SqlCommand cmd = new SqlCommand(quary, dataBase.getConnection());
            //cmd.ExecuteNonQuery();
            //Table.Clear();
            //adapter.Fill(Table);
            //dataGridView1.DataSource = Table;
        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlParameter sqlParameter = new SqlParameter("@name", textBox3.Text);
            string _query = "SearchByName";
            using (SqlCommand cmd = new SqlCommand(_query, dataBase.getConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(sqlParameter);
                Table = new DataTable();
                
                adapter = new SqlDataAdapter(cmd);
                adapter.Fill(Table);
                dataGridView1.DataSource = Table;
                dataGridView1.Columns["StudentId"].Visible = false;
                dataGridView1.Columns["StudentId"].ReadOnly = true;
            }
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешить ввод только цифр и клавиш управления текстом (backspace, delete, arrow keys)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Ограничить количество символов в TextBox до 10
            if (textBox1.TextLength >= 10 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            report.Show();
            this.Hide();
        }
    }   
  

}