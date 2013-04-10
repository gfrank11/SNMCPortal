using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace SNMCDataManager
{
    public class PipelineContext
    {
        private FlattenLoanDataTable _flDataTable = new FlattenLoanDataTable();
        public IEnumerable<FlattenLoan> GetFlattenLoansByNMLSID(string nmlsid)
        {
            DataTable dt = _flDataTable.GetFlattenLoansByNMLSID(nmlsid);
            if (dt.Rows.Count == 0)
                return new List<FlattenLoan>();
            return DataTableToFlattenLoans(dt);
        }
        private List<FlattenLoan> DataTableToFlattenLoans(DataTable dt)
        {
            List<FlattenLoan> floanList = new List<FlattenLoan>();
            FlattenLoan fl;
            foreach (DataRow row in dt.Rows)
            {
                fl = new FlattenLoan()
                {
                    AdjustedNoteAmount = row["AdjustedNoteAmt"].ToString().Trim(),
                    BorrowerLastName = row["BorrLast"].ToString().Trim(),
                    ChannelType = row["ChannelType"].ToString().Trim(),
                    CurrentStatus = row["CurrentStatus"].ToString().Trim(),
                    DecisionStatus = row["DecisionStatus"].ToString().Trim(),
                    EstClosingDate = row["EstCloseDate"].ToString().Trim(),
                    LoanNumber = row["AppNum"].ToString().Trim(),
                    LoanProgramName = row["LoanProgramName"].ToString().Trim(),
                    LoanPurpose = row["Loan_Purpose"].ToString().Trim(),
                    LockExpireDate = row["LockExpireDate"].ToString().Trim(),
                    NoteRate = row["Note_Rate"].ToString().Trim(),
                    OriginatorName = row["OriginatorName"].ToString().Trim(),
                    OriginatorNMLSID = row["OriginatorNMLSID"].ToString().Trim(),
                    ReceiveDate = row["ReceivedDate"].ToString().Trim(),
                    RetailOriginator = row["Retail_Originator"].ToString().Trim(),
                    RetailOriginatorNMLSID = row["Retail_OriginatorNMLSID"].ToString().Trim(),
                    UnderwriterName = row["Underwriter_Name"].ToString().Trim()
                };
                floanList.Add(fl);
            }   
            return floanList;
        }
    }
}
