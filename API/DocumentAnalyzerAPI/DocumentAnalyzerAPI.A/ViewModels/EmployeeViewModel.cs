using DocumentAnalyzerAPI.D.Models;
using System.Collections.Generic;

namespace DocumentAnalyzerAPI.A.ViewModels
{
    public class EmployeeViewModel
    {
        /// <summary>
        /// Atribute of EmployeeViewModel
        /// </summary>
        public IEnumerable<Employee> Employees { get; set; }
    }
}
