using DocumentAnalyzerAPI.A.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.A.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeViewModel GetEmployees();
    }
}
