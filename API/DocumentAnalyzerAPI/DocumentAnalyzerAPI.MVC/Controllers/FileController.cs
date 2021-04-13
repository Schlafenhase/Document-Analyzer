using DocumentAnalyzerAPI.A.Interfaces;
using DocumentAnalyzerAPI.A.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentAnalyzerAPI.MVC.Controllers
{
    [Route("Api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FileController : Controller
    {
        /// <summary>
        /// Atribute that stores the file service
        /// </summary>
        private IFileService _fileService;

        /// <summary>
        /// Constructor of FileController
        /// </summary>
        /// <param name="fileService">
        /// File service
        /// </param>
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Method that searches all the files in the database
        /// </summary>
        /// <returns>
        /// List with all the database files
        /// </returns>
        [Route("Files")]
        [HttpGet]
        public FileViewModel GetFiles()
        {
            return _fileService.GetFiles();
        }
    }
}
