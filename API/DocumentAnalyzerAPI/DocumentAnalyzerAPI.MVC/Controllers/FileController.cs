using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class FileController : Controller
    {
        private IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [Route("Files")]
        [HttpGet]
        public FileViewModel GetFiles()
        {
            return _fileService.GetFiles();
        }

        [HttpPost]
        public void AddFile([FromBody] File file)
        {
            _fileService.AddFile(file);
            return;
        }
    }
}
