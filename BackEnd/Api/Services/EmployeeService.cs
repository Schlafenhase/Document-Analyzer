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
        public DocumentAnalyzerDBContext _context;

        public EmployeeService(DocumentAnalyzerDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees;
        }

        public void AddEmployee(Employee employee)
        {
            _context.Add(employee);
            _context.SaveChanges();
            return;
        }
    }
}
