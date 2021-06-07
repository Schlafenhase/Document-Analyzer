using DANameAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DANameAnalyzer.Services
{
    public class EmployeeService
    {
        /// <summary>
        /// Atribute that stores the database context
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
        /// Method that gets all the employees of the database
        /// </summary>
        /// <returns>
        /// List with all the database employees
        /// </returns>
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees;
        }
    }
}
