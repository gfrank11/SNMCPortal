using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SNMCDataManager;

namespace SNMCPortal.Caching
{
    static public class InMemoryCache
    {
        static public T Get<T>(string cacheID, Func<T> getItemCallback) where T : class
        {
            T item = HttpRuntime.Cache.Get(cacheID) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(cacheID, item);
            }
            return item;
        }
        static public Employee  GetEmployee(string fname, string lName, bool forceGet)
        {
            var employee = Get<Employee>("Employee", () =>
            {
                var db = new SNMCDataManager.EmployeeContext();  //no need to employ using here. The manager takes care of it.
                return db.GetEmployeeByName(fname,lName, forceGet);
            });
            return employee;
        }
    }
}