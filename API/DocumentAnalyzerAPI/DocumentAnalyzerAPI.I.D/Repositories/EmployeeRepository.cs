using DocumentAnalyzerAPI.D.Interfaces;
using DocumentAnalyzerAPI.D.Models;
using DocumentAnalyzerAPI.I.D.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.I.D.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EntitiesDbContext _context;
        public EmployeeRepository(EntitiesDbContext context)
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
