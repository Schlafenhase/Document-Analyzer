using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DocumentAnalyzerAPI.A.Interfaces;
using DocumentAnalyzerAPI.D.Models;
using DocumentAnalyzerAPI.MVC.Configuration;
using DocumentAnalyzerAPI.MVC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DocumentAnalyzerAPI.MVC.Controllers
{
    [Route("Api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NLPController : Controller
    {
        private INLPService _nlpService;
        private IFileService _fileService;
        private readonly AzureBlobStorageConfig _azureBlobStorageConfig;
        private readonly MongoFileService _mongoFileService;

        public NLPController(INLPService nlpService, IFileService fileService, IOptionsMonitor<AzureBlobStorageConfig> azureOptionsMonitor, MongoFileService mongoFileService)
        {
            _nlpService = nlpService;
            _fileService = fileService;
            _azureBlobStorageConfig = azureOptionsMonitor.CurrentValue;
            _mongoFileService = mongoFileService;
        }

        [HttpPost]
        public int SearchEmployees([FromBody] File file)
        {
            try
            {
                var result = _nlpService.SearchEmployees(file, _azureBlobStorageConfig.ConectionString, _azureBlobStorageConfig.ContainerName);
                var files = _fileService.GetFiles().Files;
                int fileId = -1;
                foreach (var sFile in files)
                {
                    if (sFile.Name == file.Name)
                    {
                        fileId = sFile.Id;
                        break;
                    }
                }
                _mongoFileService.InsertResults(result, fileId);
                return 1;
            }
            catch
            {
                Debug.WriteLine("Error when trying to analyze the file.");
                return -1;
            }
        }
    }
}
