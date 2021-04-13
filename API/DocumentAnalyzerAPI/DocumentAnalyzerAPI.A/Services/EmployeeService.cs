using DocumentAnalyzerAPI.A.Interfaces;
using DocumentAnalyzerAPI.A.ViewModels;
using DocumentAnalyzerAPI.D.Interfaces;
using DocumentAnalyzerAPI.D.Models;

namespace DocumentAnalyzerAPI.A.Services
{
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// Atribute that stores the employee repository
        /// </summary>
        public IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Constructor of EmployeeService
        /// </summary>
        /// <param name="employeeRepository">
        /// Employee repository
        /// </param>
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Method that gets all the employees in the database
        /// </summary>
        /// <returns>
        /// List with all the employees
        /// </returns>
        public EmployeeViewModel GetEmployees()
        {
            return new EmployeeViewModel()
            {
                Employees = _employeeRepository.GetEmployees()
            };
        }

        /// <summary>
        /// Method that adds an employee to the database
        /// </summary>
        /// <param name="employee">
        /// Employeee to be added
        /// </param>
        public void AddEmployee(Employee employee)
        {
            _employeeRepository.AddEmployee(employee);
            return;
        }
    }
}
