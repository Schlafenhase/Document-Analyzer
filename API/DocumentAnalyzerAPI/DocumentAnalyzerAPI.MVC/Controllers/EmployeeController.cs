using DocumentAnalyzerAPI.A.Interfaces;
using DocumentAnalyzerAPI.A.ViewModels;
using DocumentAnalyzerAPI.D.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentAnalyzerAPI.MVC.Controllers
{
    [Route("Api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController : Controller
    {
        /// <summary>
        /// Atribute that stores the employee service
        /// </summary>
        private IEmployeeService _employeeService;

        /// <summary>
        /// Constructor of EmployeeController
        /// </summary>
        /// <param name="employeeService">
        /// Employee service
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
        public EmployeeViewModel GetEmployees()
        {
            return _employeeService.GetEmployees();
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
            _employeeService.AddEmployee(employee);
            return;
        }
    }
}
