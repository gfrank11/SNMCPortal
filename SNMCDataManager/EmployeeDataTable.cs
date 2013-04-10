using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace SNMCDataManager
{
	internal class EmployeeDataTable
	{
        private const string SN52CommissionsConnection = "SN52Commissions";
		public DataTable GetEmployeeByName(string fname, string lname, bool forceGet)
		{
			StringBuilder qry = new StringBuilder();

			qry.Append("SELECT ");
			qry.Append(		"EMPLOYID, ");
			qry.Append(		"LASTNAME, ");
			qry.Append(		"FRSTNAME, ");
			qry.Append(		"NICKNAME, ");
			qry.Append(		"ALTERNATENAME, ");
			qry.Append(		"JOBTITLE, ");
			qry.Append(		"DEPRTMNT as Department, ");
			qry.Append(		"SUTASTAT as State, ");
			qry.Append(		"SUPERVISORCODE_I, ");
			qry.Append(		"USERDEF1 AS NMLS_Num, ");
			qry.Append(		"INACTIVE ");
			qry.Append("FROM UPR00100 ");
			qry.Append("WHERE ");
			qry.AppendFormat("LASTNAME LIKE '{0}%' AND ", lname);
			qry.AppendFormat("FRSTNAME LIKE '{0}%'", fname);

			if (!forceGet)
				qry.Append(	" AND INACTIVE = 0");
            return qry.ExecuteSqlToTable(ConfigurationManager.ConnectionStrings[SN52CommissionsConnection].ConnectionString);
		}

		public DataTable GetEmployeeById(string hrId)
		{
			StringBuilder qry = new StringBuilder();

			qry.Append("SELECT ");
			qry.Append(		"EMPLOYID, ");
			qry.Append(		"LASTNAME, ");
			qry.Append(		"FRSTNAME, ");
			qry.Append(		"NICKNAME, ");
			qry.Append(		"ALTERNATENAME, ");
			qry.Append(		"JOBTITLE, ");
			qry.Append(		"DEPRTMNT as Department, ");
			qry.Append(		"SUTASTAT as State, ");
			qry.Append(		"SUPERVISORCODE_I, ");
			qry.Append(		"USERDEF1 AS NMLS_Num, ");
			qry.Append(		"INACTIVE ");
			qry.Append("FROM UPR00100 ");
			qry.AppendFormat("WHERE EMPLOYID = '{0}'", hrId);

            return qry.ExecuteSqlToTable(ConfigurationManager.ConnectionStrings[SN52CommissionsConnection].ConnectionString);
		}

		public DataTable GetEmployeeByNickname(string fname, string lname, bool forceGet)
		{
			StringBuilder qry = new StringBuilder();

			qry.Append("SELECT ");
			qry.Append(		"EMPLOYID, ");
			qry.Append(		"LASTNAME, ");
			qry.Append(		"FRSTNAME, ");
			qry.Append(		"NICKNAME, ");
			qry.Append(		"ALTERNATENAME, ");
			qry.Append(		"JOBTITLE, ");
			qry.Append(		"DEPRTMNT as Department, ");
			qry.Append(		"SUTASTAT as State, ");
			qry.Append(		"SUPERVISORCODE_I, ");
			qry.Append(		"USERDEF1 AS NMLS_Num, ");
			qry.Append(		"INACTIVE ");
			qry.Append("FROM UPR00100 ");
			qry.Append("WHERE ");
			qry.AppendFormat("LASTNAME LIKE '{0}%' AND ", lname);
			qry.AppendFormat("NICKNAME LIKE '{0}%'", fname);

			if (!forceGet)
				qry.Append(" AND INACTIVE = 0");

            return qry.ExecuteSqlToTable(ConfigurationManager.ConnectionStrings[SN52CommissionsConnection].ConnectionString);
		}

		public DataTable GetEmployeeByAlternateName(string fname, string lname, bool forceGet)
		{
			StringBuilder qry = new StringBuilder();

			qry.Append("SELECT ");
			qry.Append(		"EMPLOYID, ");
			qry.Append(		"LASTNAME, ");
			qry.Append(		"FRSTNAME, ");
			qry.Append(		"NICKNAME, ");
			qry.Append(		"ALTERNATENAME, ");
			qry.Append(		"JOBTITLE, ");
			qry.Append(		"DEPRTMNT AS Department, ");
			qry.Append(		"SUTASTAT AS State, ");
			qry.Append(		"SUPERVISORCODE_I, ");
			qry.Append(		"USERDEF1 AS NMLS_Num, ");
			qry.Append(		"INACTIVE ");
			qry.Append("FROM UPR00100 ");
			qry.Append("WHERE ");
			qry.AppendFormat("LASTNAME LIKE '{0}%' AND ", lname);
			qry.AppendFormat("ALTERNATENAME LIKE '{0}%'", fname);

			if (!forceGet)
				qry.Append(" AND INACTIVE = 0");

            return qry.ExecuteSqlToTable(ConfigurationManager.ConnectionStrings[SN52CommissionsConnection].ConnectionString);
		}

		public DataTable GetManagerBySupervisorCode(string supervisorCode)
		{
			StringBuilder qry = new StringBuilder();

			qry.Append("SELECT ");
			qry.Append(		"EMPLOYID, ");
			qry.Append(		"LASTNAME, ");
			qry.Append(		"FRSTNAME, ");
			qry.Append(		"NICKNAME, ");
			qry.Append(		"JOBTITLE ");
			qry.Append("FROM UPR00100 ");
			qry.Append("WHERE ");
			qry.AppendFormat("JOBTITLE = '{0}' AND ", supervisorCode);
			qry.Append(		"INACTIVE = 0");

            return qry.ExecuteSqlToTable(ConfigurationManager.ConnectionStrings[SN52CommissionsConnection].ConnectionString);
		}

		public DataTable GetManagerById(string hrId)
		{
			StringBuilder qry = new StringBuilder();

			qry.Append("SELECT ");
			qry.Append(		"LASTNAME, ");
			qry.Append(		"FRSTNAME, ");
			qry.Append(		"NICKNAME, ");
			qry.Append(		"ALTERNATENAME, ");
			qry.Append(		"JOBTITLE, ");
			qry.Append(		"DEPRTMNT AS Department, ");
			qry.Append(		"SUTASTAT AS State, ");
			qry.Append("WHERE ");
			qry.AppendFormat("EMPLOYID = '{0}'", hrId);

            return qry.ExecuteSqlToTable(ConfigurationManager.ConnectionStrings[SN52CommissionsConnection].ConnectionString);
		}
		

		private static DataSet ExecuteSqlToSet(string query)
		{
			SqlConnection  cn  = new SqlConnection(ConfigurationManager.
                                                    ConnectionStrings[SN52CommissionsConnection].
																				ConnectionString);
			SqlCommand     sql = new SqlCommand(query, cn) { CommandType = CommandType.Text };
			DataSet        ds  = new DataSet();
			SqlDataAdapter da  = new SqlDataAdapter(sql);

			da.Fill(ds);

			return ds;
		}
	}
}
