using System.Data.SqlClient;

namespace Hostel_accounting
{
    public class DataBase
    {
        private SqlConnection con =
            new SqlConnection(@"Data Source=BEKZHAN\MURIM;Initial Catalog=University_dorms;Integrated Security=True");
        //Server=BEKZHAN\MURIM;Database=Sklad
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