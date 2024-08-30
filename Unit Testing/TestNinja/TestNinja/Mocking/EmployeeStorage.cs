using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    internal class EmployeeStorage : IEmployeeStorage
    {
        private EmployeeContext database;

        public EmployeeStorage()
        {
            this.database = new EmployeeContext();
        }

        public void DeleteEmployee(int id)
        {
            Employee employee = database.Employees.Find(id);

            if (employee == null)
            {
                return;
            }

            database.Employees.Remove(employee);
            database.SaveChanges();
        }
    }
}
