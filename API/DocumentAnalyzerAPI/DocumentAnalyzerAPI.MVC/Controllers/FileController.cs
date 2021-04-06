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
    public class FileController : Controller
    {
        private IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [Authorize]
        public IActionResult Index()
        {
            FileViewModel model = _fileService.GetFiles();
            return View(model);
        }
    }
}
