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
            // Создайте подключение к базе данных
            // Откройте подключение
            try
            {
                 dataBase.openConnection();

            // Создайте команду SQL для выборки студентов, поступивших в указанном году
            string query = "SELECT dbo.Students.FIO, dbo.Sex.Sex, dbo.Students.BirthDate, dbo.Students.PhoneNumber, dbo.Students.Email, dbo.Students.StudentCardNumber, dbo.Faculties.FacultiesName, dbo.Rooms.RoomNumber," + 
                           " dbo.Students.Passport, dbo.Students.Inventory, dbo.Students.Data_of_receipt"+
                           " FROM dbo.Sex INNER JOIN" +
                           " dbo.Students ON dbo.Sex.ID = dbo.Students.Sex INNER JOIN" +
                           " dbo.Rooms ON dbo.Students.Room_residence = dbo.Rooms.RoomId INNER JOIN"+
                           " dbo.Faculties ON dbo.Students.FacultiesId = dbo.Faculties.FacultiesId"+
                           " WHERE YEAR(Data_of_receipt) = @Year";
            SqlCommand command = new SqlCommand(query, dataBase.getConnection());
            command.Parameters.AddWithValue("@Year", selectedDate.Year);

            // Создайте объект для чтения результатов
            SqlDataReader reader = command.ExecuteReader();

            // Создайте DataTable для хранения результатов запроса
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            // Закройте объект чтения
            reader.Close();

            // Создайте отчет в формате Excel с использованием библиотеки EPPlus
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Отчет");
                
                string[] newHeaders = { "ФИО", "Пол", "Год рождения", "Номер телефона", "Почта", "Студенческий номер", "Факультет", "Номер комнаты", "ID Пасспорта", "Инвентарь", "Год поступления" };
                // Заполните заголовки столбцов
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    worksheet.Cells[1, col + 1].Value = newHeaders[col];
                }

                // Заполните данные студентов
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        // Получите значение ячейки
                        object value = dataTable.Rows[row][col];

                        // Если значение является датой, измените его формат на "yyyy"
                        if (value is DateTime dateValue)
                        {
                            worksheet.Cells[row + 2, col + 1].Value = dateValue.ToString("yyyy");
                        }
                        // Если значение является логическим значением False, замените его на "Выдано"
                        // Если значение является логическим значением False, замените его на "Не выдано"
                        else if (value is bool && value.ToString() == "False")
                        {
                            worksheet.Cells[row + 2, col + 1].Value = "Не выдан";
                        }
                        // Если значение является логическим значением True, замените его на "Выдано"
                        else if (value is bool && value.ToString() == "True")
                        {
                            worksheet.Cells[row + 2, col + 1].Value = "Выдан";
                        }
                        else
                        {
                            worksheet.Cells[row + 2, col + 1].Value = value?.ToString();
                        }
                    }
                }
                worksheet.Cells.AutoFitColumns();
                // Сохраните отчет в файле Excel
                FileInfo excelFile = new FileInfo("C:\\Users\\user\\Desktop\\Report.xlsx");
                excelPackage.SaveAs(excelFile);
            }

            MessageBox.Show("Отчет сохранен на рабочий стол", "Успех", MessageBoxButtons.OK,
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