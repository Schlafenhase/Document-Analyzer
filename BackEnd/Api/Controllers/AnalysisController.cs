using DAApi.Configuration;
using DAApi.Interfaces;
using DAApi.Models;
using DAApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Controllers
{
    [Authorize]
    [Route("Api/[controller]")]
    [ApiController]
    public class AnalysisController : Controller
    {
        /// <summary>
        /// Atributes that handle the services and configurations
        /// </summary>
        private readonly PublisherService _publisherService;
        private readonly IFileService _fileService;
        private readonly IAnalysisService _analysisService;
        private readonly AzureBlobStorageConfig _azureBlobStorageConfig;
        private readonly MongoFileService _mongoFileService;
        private readonly RabbitMQConfig _rabbitMQConfig;

        /// <summary>
        /// Constructor of AnalysisController
        /// </summary>
        /// <param name="publisherService">
        /// Publisher service interface
        /// </param>
        /// <param name="fileService">
        /// File service interface
        /// </param>
        /// <param name="analysisService">
        /// Analysis interface
        /// </param>
        /// <param name="azureOptionsMonitor">
        /// Azure options
        /// </param>
        /// <param name="mongoFileService">
        /// Mongo service interface
        /// </param>
        /// <param name="rabbitOptionsMonitor">
        /// RabbitMQ options
        /// </param>
        public AnalysisController(PublisherService publisherService, IFileService fileService, IAnalysisService analysisService, IOptionsMonitor<AzureBlobStorageConfig> azureOptionsMonitor, MongoFileService mongoFileService, IOptionsMonitor<RabbitMQConfig> rabbitOptionsMonitor)
        {
            _publisherService = publisherService;
            _fileService = fileService;
            _analysisService = analysisService;
            _azureBlobStorageConfig = azureOptionsMonitor.CurrentValue;
            _mongoFileService = mongoFileService;
            _rabbitMQConfig = rabbitOptionsMonitor.CurrentValue;
        }

        /// <summary>
        /// Method that sends a file to analyze
        /// </summary>
        /// <param name="file">
        /// File to be analyzed
        /// </param>
        [HttpPost]
        public void AnalyzeFile([FromBody] File file)
        {
            try
            {
                _fileService.AddFile(file);
                string text = _analysisService.GetText(file.Name, file.Container, _azureBlobStorageConfig.ConnectionString);

                var files = _fileService.GetFiles();
                int fileId = -1;
                foreach (var sFile in files)
                {
                    if (sFile.Name == file.Name)
                    {
                        fileId = sFile.Id;
                        break;
                    }
                }

                _mongoFileService.InsertResults(fileId);

                QueueItem queueItem = new()
                {
                    Id = fileId,
                    Name = file.Name,
                    Text = text
                };

                _publisherService.PublishToQueue(queueItem, _rabbitMQConfig.SentimentQueue);
                _publisherService.PublishToQueue(queueItem, _rabbitMQConfig.NameQueue);
                _publisherService.PublishToQueue(queueItem, _rabbitMQConfig.SwearQueue);

                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }
        }
    }
}
