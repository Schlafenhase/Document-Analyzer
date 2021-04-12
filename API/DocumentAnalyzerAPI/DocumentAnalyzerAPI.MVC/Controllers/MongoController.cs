using DocumentAnalyzerAPI.MVC.Models;
using DocumentAnalyzerAPI.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DocumentAnalyzerAPI.MVC.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MongoController : Controller
    {
        private readonly MongoFileService _mongoFileService;

        public MongoController(MongoFileService fileService)
        {
            _mongoFileService = fileService;
        }

        [HttpGet]
        public ActionResult<List<MongoFile>> Get() =>
            _mongoFileService.Get();

        [HttpGet("{id:length(24)}", Name = "GetFile")]
        public ActionResult<MongoFile> Get(string id)
        {
            var file = _mongoFileService.Get(id);

            if (file == null)
            {
                return NotFound();
            }

            return file;
        }

        [HttpPost]
        public ActionResult<MongoFile> Create(MongoFile file)
        {
            _mongoFileService.Create(file);

            return CreatedAtRoute("GetFile", new { id = file.Id.ToString() }, file);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, MongoFile fileIn)
        {
            var file = _mongoFileService.Get(id);

            if (file == null)
            {
                return NotFound();
            }

            _mongoFileService.Update(id, fileIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var file = _mongoFileService.Get(id);

            if (file == null)
            {
                return NotFound();
            }

            _mongoFileService.Remove(file.Id);

            return NoContent();
        }
    }
}