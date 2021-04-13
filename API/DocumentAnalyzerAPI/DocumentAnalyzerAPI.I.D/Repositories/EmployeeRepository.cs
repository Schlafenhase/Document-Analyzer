using DocumentAnalyzerAPI.D.Interfaces;
using DocumentAnalyzerAPI.D.Models;
using DocumentAnalyzerAPI.I.D.Context;
using System.Collections.Generic;

namespace DocumentAnalyzerAPI.I.D.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// Atribute that stores the microsoft sql database context
        /// </summary>
        public EntitiesDbContext _context;

        /// <summary>
        /// Constructor of EmployeeRespository
        /// </summary>
        /// <param name="context">
        /// Entities database context
        /// </param>
        public EmployeeRepository(EntitiesDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that gets all employees in the database
        /// </summary>
        /// <returns>
        /// List with all employees
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
