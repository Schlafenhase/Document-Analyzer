using DANameAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DANameAnalyzer.Services
{
    public class EmployeeService
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
    }
}
