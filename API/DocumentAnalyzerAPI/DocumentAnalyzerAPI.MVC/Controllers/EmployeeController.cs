using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentAnalyzerAPI.A.Interfaces;
using DocumentAnalyzerAPI.A.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentAnalyzerAPI.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize]
        public IActionResult Index()
        {
            EmployeeViewModel model = _employeeService.GetEmployees();
            return View(model);
        }
    }
}
