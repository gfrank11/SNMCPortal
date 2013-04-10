using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SNMCDataManager
{
    internal class FlattenLoanDataTable
    {
        private const string SteveReportConnection = "SteveReport";
        public DataTable GetFlattenLoansByNMLSID(string nmlsid)
        {
            StringBuilder qry = new StringBuilder();

            qry.Append("SELECT ");
            qry.Append("[AppNum], ");
            qry.Append("[OriginatorName], ");
            qry.Append("[OriginatorNMLSID], ");
            qry.Append("[Retail_Originator], ");
            qry.Append("[Retail_OriginatorNMLSID], ");
            qry.Append("[LoanProgramName], ");
            qry.Append("[BorrLast], ");
            qry.Append("[AdjustedNoteAmt], ");
            qry.Append("[Note_Rate], ");
            qry.Append("[Loan_Purpose], ");
            qry.Append("[CurrentStatus], ");
            qry.Append("[DecisionStatus], ");
            qry.Append("CONVERT(VARCHAR(10),[Rec],101) AS ReceivedDate, ");
            qry.Append("CONVERT(VARCHAR(10),[LockExpireDate],101) AS LockExpireDate, ");
            qry.Append("[EstCloseDate], ");
            qry.Append("[Underwriter_Name], ");
            qry.Append("[ChannelType] ");
            qry.Append("FROM [SteveReport].[dbo].[FlattenedLoans] ");
            qry.Append("WHERE ");
            qry.AppendFormat("([OriginatorNMLSID] = '{0}' OR ", nmlsid);
            qry.AppendFormat("[Retail_OriginatorNMLSID] = '{0}') AND ", nmlsid);
            qry.Append("[FundedDate] IS NULL AND ");
            qry.Append("[LoanFolder] IN ('My Pipeline','Pre-qualifications','Prospects')");
            return qry.ExecuteSqlToTable(ConfigurationManager.ConnectionStrings[SteveReportConnection].ConnectionString);
        }
    }
}
