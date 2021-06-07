using DAApi.Interfaces;
using DAApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Controllers
{
    [Authorize]
    [Route("Api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        /// <summary>
        /// Atribute that handle the services and configurations
        /// </summary>
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Constructor of EmployeeController
        /// </summary>
        /// <param name="employeeService">
        /// Employee service interface
        /// </param>
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Method that searches all the employees in the database
        /// </summary>
        /// <returns>
        /// List with all the database employees
        /// </returns>
        [Route("Employees")]
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            try
            {
                return _employeeService.GetEmployees();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
            
        }

        /// <summary>
        /// Method that adds a new employee to the database
        /// </summary>
        /// <param name="employee">
        /// Employee to be added
        /// </param>
        [HttpPost]
        public void AddEmployee([FromBody] Employee employee)
        {
            try
            {
                _employeeService.AddEmployee(employee);
                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }
        }
    }
}
