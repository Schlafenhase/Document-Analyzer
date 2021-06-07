using DAApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Method that gets all employees in the database
        /// </summary>
        /// <returns>
        /// List with all the employees
        /// </returns>
        IEnumerable<Employee> GetEmployees();

        /// <summary>
        /// Method that adds an employee to the database
        /// </summary>
        /// <param name="employee">
        /// Employee to be added
        /// </param>
        void AddEmployee(Employee employee);
    }
}
