using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DocumentAnalyzerAPI.A.Interfaces;
using DocumentAnalyzerAPI.MVC.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DocumentAnalyzerAPI.MVC.Controllers
{
    [Route("Api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AzureBlobAuthController : Controller
    {
        private IAzureBlobAuthService _azureBlobAuthService;
        private readonly AzureBlobStorageConfig _azureBlobStorageConfig;

        public AzureBlobAuthController(IAzureBlobAuthService azureBlobAuthService, IOptionsMonitor<AzureBlobStorageConfig> azureOptionsMonitor)
        {
            _azureBlobAuthService = azureBlobAuthService;
            _azureBlobStorageConfig = azureOptionsMonitor.CurrentValue;
        }

        [HttpGet]
        public string GetAzureBlobStorageSAS()
        {
            return _azureBlobAuthService.GenerateSAS(_azureBlobStorageConfig.AccountKey, _azureBlobStorageConfig.AccountName);
        }
    }
}
