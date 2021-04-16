using DocumentAnalyzerAPI.A.Interfaces;
using DocumentAnalyzerAPI.MVC.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DocumentAnalyzerAPI.MVC.Controllers
{
    [Route("Api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AzureBlobAuthController : Controller
    {
        /// <summary>
        /// Atributes that store the azure blob storage authentication service and configuration
        /// </summary>
        private IAzureBlobAuthService _azureBlobAuthService;
        private readonly AzureBlobStorageConfig _azureBlobStorageConfig;

        /// <summary>
        /// Construction of AzureBlobAuthController
        /// </summary>
        /// <param name="azureBlobAuthService">
        /// Azure Blob Storage Authentication service
        /// </param>
        /// <param name="azureOptionsMonitor">
        /// Azure Blob Storage Authentication configuration
        /// </param>
        public AzureBlobAuthController(IAzureBlobAuthService azureBlobAuthService, IOptionsMonitor<AzureBlobStorageConfig> azureOptionsMonitor)
        {
            _azureBlobAuthService = azureBlobAuthService;
            _azureBlobStorageConfig = azureOptionsMonitor.CurrentValue;
        }

        /// <summary>
        /// Method that generates the SAS token for Azure Blob Storage
        /// </summary>
        /// <returns>
        /// SAS token
        /// </returns>
        [HttpGet]
        public Dictionary<string, string> GetAzureBlobStorageSAS()
        {
            string SASToken = _azureBlobAuthService.GenerateSAS(_azureBlobStorageConfig.AccountKey, _azureBlobStorageConfig.AccountName);
            Dictionary<string, string> result = new Dictionary<string, string>();

            result.Add("storageUri", _azureBlobStorageConfig.StorageUri);
            result.Add("storageAccessToken", SASToken);

            return result;
        }
    }
}
