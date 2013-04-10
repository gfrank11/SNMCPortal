using System;
using System.Data;
namespace SNMCDataManager
{
    public class EmployeeContext 
    {
        private EmployeeDataTable _empDataTable = new EmployeeDataTable();
        public EmployeeContext()
        {

        }
        public Employee GetEmployeeByName(string fname, string lname, bool forceGet)
        {
            DataTable dt = _empDataTable.GetEmployeeByName(fname, lname, forceGet);
            if (dt.Rows.Count == 0)
                dt = _empDataTable.GetEmployeeByNickname(fname, lname, forceGet);

            if (dt.Rows.Count == 0)
                dt = _empDataTable.GetEmployeeByAlternateName(fname, lname, forceGet);

            if (dt.Rows.Count == 0)
                return null;
            return DataTableToEmployee(dt);
        }
        private Employee DataTableToEmployee(DataTable dt)
        {
            Employee e = new Employee()
            {
                AltName = dt.Rows[0]["ALTERNATENAME"].ToString().Trim(),
                DepartmentName = dt.Rows[0]["Department"].ToString().Trim(),
                EmployeeId = dt.Rows[0]["EMPLOYID"].ToString().Trim(),
                FirstName = dt.Rows[0]["FRSTNAME"].ToString().Trim(),
                Inactive = dt.Rows[0]["inactive"].ToString() == "1",
                JobTitle = dt.Rows[0]["JOBTITLE"].ToString().Trim(),
                LastName = dt.Rows[0]["LASTNAME"].ToString().Trim(),
                NickName = dt.Rows[0]["NICKNAME"].ToString().Trim(),
                NMLSId = dt.Rows[0]["NMLS_Num"].ToString().Trim(),
                State = dt.Rows[0]["State"].ToString().Trim(),
                SupervisorId = dt.Rows[0]["SUPERVISORCODE_I"].ToString().Trim()
            };
            return e;
        }
    }
}
