using System.Data.SqlClient;

namespace Hostel_accounting
{
    public class DataBase
    {
        private SqlConnection con =
            new SqlConnection(@"Data Source=BEKZHAN\MURIM;Initial Catalog=dormitory;Integrated Security=True");
        //Server=BEKZHAN\MURIM;
        //Server=DESKTOP-LQOC5FQ;
        public void openConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();

            }
        }

        public void closeConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close(); 

            }
        }

        public SqlConnection getConnection()
        {
            return con;
        }
    }
}