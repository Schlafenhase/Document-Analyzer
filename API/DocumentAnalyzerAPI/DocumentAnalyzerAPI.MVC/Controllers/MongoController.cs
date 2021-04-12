using DocumentAnalyzerAPI.MVC.Models;
using DocumentAnalyzerAPI.MVC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DocumentAnalyzerAPI.MVC.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MongoController : Controller
    {
        private readonly MongoFileService _mongoFileService;

        public MongoController(MongoFileService fileService)
        {
            _mongoFileService = fileService;
        }

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