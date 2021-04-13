using DocumentAnalyzerAPI.MVC.Models;
using DocumentAnalyzerAPI.MVC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentAnalyzerAPI.MVC.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MongoController : Controller
    {
        /// <summary>
        /// Atribute that stores the mongo service
        /// </summary>
        private readonly MongoFileService _mongoFileService;

        /// <summary>
        /// Constructor of the MongoController
        /// </summary>
        /// <param name="fileService">
        /// Monfo service
        /// </param>
        public MongoController(MongoFileService fileService)
        {
            _mongoFileService = fileService;
        }

        /// <summary>
        /// Method that searches a mongo database entry with the file id
        /// </summary>
        /// <param name="id">
        /// Id of the file to search
        /// </param>
        /// <returns>
        /// Mongo database entry found
        /// </returns>
        [Route("{id}")]
        [HttpGet]
        public ActionResult<MongoFile> Get(string id)
        {
            var file = _mongoFileService.Get(id);

            if (file == null)
            {
                return NotFound();
            }

            return file;
        }
    }
}