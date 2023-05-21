using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using OfficeOpenXml;

namespace Hostel_accounting
{
    public partial class Report : Form
    {
        private DataBase dataBase = new DataBase();


        public Report()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            GenerateReport(selectedDate);
        }

        private void GenerateReport(DateTime selectedDate)
        {
            // �������� ����������� � ���� ������
            // �������� �����������
            try
            {
                 dataBase.openConnection();

            // �������� ������� SQL ��� ������� ���������, ����������� � ��������� ����
            string query = "SELECT dbo.Students.FIO, dbo.Sex.Sex, dbo.Students.BirthDate, dbo.Students.PhoneNumber, dbo.Students.Email, dbo.Students.StudentCardNumber, dbo.Faculties.FacultiesName, dbo.Rooms.RoomNumber," + 
                           " dbo.Students.Passport, dbo.Students.Inventory, dbo.Students.Data_of_receipt"+
                           " FROM dbo.Sex INNER JOIN" +
                           " dbo.Students ON dbo.Sex.ID = dbo.Students.Sex INNER JOIN" +
                           " dbo.Rooms ON dbo.Students.Room_residence = dbo.Rooms.RoomId INNER JOIN"+
                           " dbo.Faculties ON dbo.Students.FacultiesId = dbo.Faculties.FacultiesId"+
                           " WHERE YEAR(Data_of_receipt) = @Year";
            SqlCommand command = new SqlCommand(query, dataBase.getConnection());
            command.Parameters.AddWithValue("@Year", selectedDate.Year);

            // �������� ������ ��� ������ �����������
            SqlDataReader reader = command.ExecuteReader();

            // �������� DataTable ��� �������� ����������� �������
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            // �������� ������ ������
            reader.Close();

            // �������� ����� � ������� Excel � �������������� ���������� EPPlus
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("�����");
                
                string[] newHeaders = { "���", "���", "��� ��������", "����� ��������", "�����", "������������ �����", "���������", "����� �������", "ID ���������", "���������", "��� �����������" };
                // ��������� ��������� ��������
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    worksheet.Cells[1, col + 1].Value = newHeaders[col];
                }

                // ��������� ������ ���������
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        // �������� �������� ������
                        object value = dataTable.Rows[row][col];

                        // ���� �������� �������� �����, �������� ��� ������ �� "yyyy"
                        if (value is DateTime dateValue)
                        {
                            worksheet.Cells[row + 2, col + 1].Value = dateValue.ToString("yyyy");
                        }
                        // ���� �������� �������� ���������� ��������� False, �������� ��� �� "������"
                        // ���� �������� �������� ���������� ��������� False, �������� ��� �� "�� ������"
                        else if (value is bool && value.ToString() == "False")
                        {
                            worksheet.Cells[row + 2, col + 1].Value = "�� �����";
                        }
                        // ���� �������� �������� ���������� ��������� True, �������� ��� �� "������"
                        else if (value is bool && value.ToString() == "True")
                        {
                            worksheet.Cells[row + 2, col + 1].Value = "�����";
                        }
                        else
                        {
                            worksheet.Cells[row + 2, col + 1].Value = value?.ToString();
                        }
                    }
                }
                worksheet.Cells.AutoFitColumns();
                // ��������� ����� � ����� Excel
                FileInfo excelFile = new FileInfo("C:\\Users\\user\\Desktop\\Report.xlsx");
                excelPackage.SaveAs(excelFile);
            }

            MessageBox.Show("����� �������� �� ������� ����", "�����", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        private void Report_FormClosing(object sender, FormClosingEventArgs e)
        {
            Students students = new Students();
            students.Show();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            int x = (screenBounds.Width - this.ClientSize.Width) / 2;
            int y = (screenBounds.Height - this.ClientSize.Height) / 2;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
        }
    }
}