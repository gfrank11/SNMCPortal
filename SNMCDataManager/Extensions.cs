using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SNMCDataManager
{
    public static class Extensions
    {
        public static DataTable ExecuteSqlToTable(this StringBuilder query, string connectionString)
        {
            string queryString = query.ToString();
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand sql = new SqlCommand(queryString, cn) { CommandType = CommandType.Text };
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(sql))
            {
                da.Fill(dt);
            }
            return dt;
        }
    }
}
