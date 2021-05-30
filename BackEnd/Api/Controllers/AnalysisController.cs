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
        private readonly IPublisherService _publisherService;
        private readonly PublisherConfig _publisherConfig;
        private readonly IFileService _fileService;
        private readonly IAnalysisService _analysisService;
        private readonly AzureBlobStorageConfig _azureBlobStorageConfig;
        private readonly MongoFileService _mongoFileService;

        public AnalysisController(IPublisherService publisherService, IOptionsMonitor<PublisherConfig> publisherOptionsMonitor, IFileService fileService, IAnalysisService analysisService, IOptionsMonitor<AzureBlobStorageConfig> azureOptionsMonitor, MongoFileService mongoFileService)
        {
            _publisherService = publisherService;
            _publisherConfig = publisherOptionsMonitor.CurrentValue;
            _fileService = fileService;
            _analysisService = analysisService;
            _azureBlobStorageConfig = azureOptionsMonitor.CurrentValue;
            _mongoFileService = mongoFileService;
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

                QueueItem queueItem = new QueueItem
                {
                    Id = fileId,
                    Name = file.Name,
                    Text = text
                };

                _publisherService.PublishToQueue(queueItem, "nameQueue", _publisherConfig.HostName);
                _publisherService.PublishToQueue(queueItem, "sentimentQueue", _publisherConfig.HostName);
                _publisherService.PublishToQueue(queueItem, "swearQueue", _publisherConfig.HostName);

                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error when trying to process the file");
                return;
            }
        }
    }
}
