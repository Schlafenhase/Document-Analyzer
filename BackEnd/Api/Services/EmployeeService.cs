using DAApi.Interfaces;
using DAApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// Atribute with the database context
        /// </summary>
        public DocumentAnalyzerDBContext _context;

        /// <summary>
        /// Constructor of EmployeeService
        /// </summary>
        /// <param name="context">
        /// Database context
        /// </param>
        public EmployeeService(DocumentAnalyzerDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that gets all employees in the database
        /// </summary>
        /// <returns>
        /// List with all the employees
        /// </returns>
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees;
        }

        /// <summary>
        /// Method that adds an employee to the database
        /// </summary>
        /// <param name="employee">
        /// Employee to be added
        /// </param>
        public void AddEmployee(Employee employee)
        {
            _context.Add(employee);
            _context.SaveChanges();
            return;
        }
    }
}
