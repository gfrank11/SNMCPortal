using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SNMCDataManager;
namespace SNMCPortalTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetEmployeeByName()
        {
            var employee = GetEmployeeByName("Ken", "Parr");
            Assert.IsNotNull(employee);
        }
        [TestMethod]
        public void TestGetPipelineByEmployee()
        {
            PipelineContext pc = new SNMCDataManager.PipelineContext();
            var employee = GetEmployeeByName("Victor", "Ortega");
            IEnumerable<FlattenLoan> aList = pc.GetFlattenLoansByNMLSID(employee.NMLSId);
            Assert.IsTrue(aList.Count() > 0);
        }
        private SNMCDataManager.Employee GetEmployeeByName(string firstName, string lastName)
        {
            SNMCDataManager.EmployeeContext ec = new SNMCDataManager.EmployeeContext();
            return ec.GetEmployeeByName(firstName, lastName, true);
        }
    }
}
