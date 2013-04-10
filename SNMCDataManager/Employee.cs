using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SNMCDataManager
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName {get; set; }
        public string NickName { get; set; }
        public string AltName { get; set; }
        public string JobTitle { get; set; }
        public string DepartmentName { get; set; }
        public string State { get; set; }
        public string SupervisorId { get; set; }
        public string NMLSId { get; set; }
        public bool Inactive { get; set; }
        //qry.Append(		"EMPLOYID, ");
        //    qry.Append(		"LASTNAME, ");
        //    qry.Append(		"FRSTNAME, ");
        //    qry.Append(		"NICKNAME, ");
        //    qry.Append(		"ALTERNATENAME, ");
        //    qry.Append(		"JOBTITLE, ");
        //    qry.Append(		"DEPRTMNT AS Department, ");
        //    qry.Append(		"SUTASTAT AS State, ");
        //    qry.Append(		"SUPERVISORCODE_I, ");
        //    qry.Append(		"USERDEF1 AS NMLS_Num, ");
        //    qry.Append(		"INACTIVE ");
    }
}
