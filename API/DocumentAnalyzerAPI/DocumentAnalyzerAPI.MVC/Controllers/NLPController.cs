using System.Diagnostics;
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
        /// <summary>
        /// Atributes that store the nlp, file and mongo services and the azure blob storage configuration
        /// </summary>
        private INLPService _nlpService;
        private IFileService _fileService;
        private readonly AzureBlobStorageConfig _azureBlobStorageConfig;
        private readonly MongoFileService _mongoFileService;

        /// <summary>
        /// Constructor of NLPController
        /// </summary>
        /// <param name="nlpService">
        /// NLP service
        /// </param>
        /// <param name="fileService">
        /// File service
        /// </param>
        /// <param name="azureOptionsMonitor">
        /// Azure Blob Storage configuration
        /// </param>
        /// <param name="mongoFileService">
        /// Mongo service
        /// </param>
        public NLPController(INLPService nlpService, IFileService fileService, IOptionsMonitor<AzureBlobStorageConfig> azureOptionsMonitor, MongoFileService mongoFileService)
        {
            _nlpService = nlpService;
            _fileService = fileService;
            _azureBlobStorageConfig = azureOptionsMonitor.CurrentValue;
            _mongoFileService = mongoFileService;
        }

        /// <summary>
        /// Method that searches the employees in a file through a nlp library
        /// </summary>
        /// <param name="file">
        /// File to be handled
        /// </param>
        /// <returns>
        /// Integer indicating if everything went well or not
        /// </returns>
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
