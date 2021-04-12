using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DocumentAnalyzerAPI.A.Interfaces;
using DocumentAnalyzerAPI.D.Models;
using DocumentAnalyzerAPI.MVC.Configuration;
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
        private readonly AzureBlobStorageConfig _azureBlobStorageConfig;

        public NLPController(INLPService nlpService, IOptionsMonitor<AzureBlobStorageConfig> azureOptionsMonitor)
        {
            _nlpService = nlpService;
            _azureBlobStorageConfig = azureOptionsMonitor.CurrentValue;
        }

        [HttpPost]
        public int SearchEmployees([FromBody] File file)
        {
            try
            {
                _nlpService.SearchEmployees(file, _azureBlobStorageConfig.ConectionString, _azureBlobStorageConfig.ContainerName);
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
