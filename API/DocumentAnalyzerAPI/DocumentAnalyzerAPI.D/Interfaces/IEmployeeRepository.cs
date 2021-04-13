using DocumentAnalyzerAPI.D.Models;
using System.Collections.Generic;

namespace DocumentAnalyzerAPI.D.Interfaces
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Method that gets all employees in the database
        /// </summary>
        /// <returns>
        /// List with all employees
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
