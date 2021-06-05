using DAApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();

        void AddEmployee(Employee employee);
    }
}
