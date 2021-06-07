using Azure.Storage;
using Azure.Storage.Sas;
using DAApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Services
{
    public class AzureBlobAuthService : IAzureBlobAuthService
    {
        /// <summary>
        /// Constructor of AzureBlobAuthService
        /// </summary>
        public AzureBlobAuthService()
        {

        }

        /// <summary>
        /// Method that generates the SAS token for Azure Blob Storage authentication
        /// </summary>
        /// <param name="key">
        /// Azure account key
        /// </param>
        /// <param name="accountName">
        /// Azure account name
        /// </param>
        /// <returns>
        /// SAS key
        /// </returns>
        public string GenerateSAS(string key, string accountName)
        {
            var sharedKeyCredentials = new StorageSharedKeyCredential(accountName, key);
            var sasBuilder = new AccountSasBuilder()
            {
                StartsOn = DateTimeOffset.UtcNow - TimeSpan.FromSeconds(10),
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(5),
                Services = AccountSasServices.All,
                ResourceTypes = AccountSasResourceTypes.All,
                Protocol = SasProtocol.Https
            };
            sasBuilder.SetPermissions(AccountSasPermissions.All);

            return sasBuilder.ToSasQueryParameters(sharedKeyCredentials).ToString();
        }
    }
}
