using DocumentAnalyzerAPI.A.Interfaces;
using DocumentAnalyzerAPI.A.ViewModels;
using DocumentAnalyzerAPI.D.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.A.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeViewModel GetEmployees()
        {
            return new EmployeeViewModel()
            {
                Employees = _employeeRepository.GetEmployees()
            };
        }
    }
}
