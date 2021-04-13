using DocumentAnalyzerAPI.A.ViewModels;
using DocumentAnalyzerAPI.D.Models;

namespace DocumentAnalyzerAPI.A.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Method that gets all the employees in the database
        /// </summary>
        /// <returns>
        /// List with all the employees
        /// </returns>
        EmployeeViewModel GetEmployees();

        /// <summary>
        /// Method that adds an employee to the database
        /// </summary>
        /// <param name="employee">
        /// Employeee to be added
        /// </param>
        void AddEmployee(Employee employee);
    }
}
