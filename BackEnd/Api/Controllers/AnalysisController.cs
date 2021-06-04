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
        private readonly PublisherService _publisherService;
        private readonly IFileService _fileService;
        private readonly IAnalysisService _analysisService;
        private readonly AzureBlobStorageConfig _azureBlobStorageConfig;
        private readonly MongoFileService _mongoFileService;
        private readonly RabbitMQConfig _rabbitMQConfig;

        public AnalysisController(PublisherService publisherService, IFileService fileService, IAnalysisService analysisService, IOptionsMonitor<AzureBlobStorageConfig> azureOptionsMonitor, MongoFileService mongoFileService, IOptionsMonitor<RabbitMQConfig> rabbitOptionsMonitor)
        {
            _publisherService = publisherService;
            _fileService = fileService;
            _analysisService = analysisService;
            _azureBlobStorageConfig = azureOptionsMonitor.CurrentValue;
            _mongoFileService = mongoFileService;
            _rabbitMQConfig = rabbitOptionsMonitor.CurrentValue;
        }

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

                _publisherService.PublishToQueue(queueItem, _rabbitMQConfig.NameQueue);
                _publisherService.PublishToQueue(queueItem, _rabbitMQConfig.SwearQueue);
                _publisherService.PublishToQueue(queueItem, _rabbitMQConfig.SentimentQueue);
                

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
