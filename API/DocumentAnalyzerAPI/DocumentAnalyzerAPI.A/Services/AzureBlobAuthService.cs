using Azure.Storage;
using Azure.Storage.Sas;
using DocumentAnalyzerAPI.A.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentAnalyzerAPI.A.Services
{
    public class AzureBlobAuthService : IAzureBlobAuthService
    {
        public string GenerateSAS(string key, string accountName)
        {
            var sharedKeyCredentials = new StorageSharedKeyCredential(accountName, key);
            var sasBuilder = new AccountSasBuilder()
            {
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(5),
                Services = AccountSasServices.Blobs,
                ResourceTypes = AccountSasResourceTypes.All,
                Protocol = SasProtocol.Https
            };
            sasBuilder.SetPermissions(AccountSasPermissions.All);

            return sasBuilder.ToSasQueryParameters(sharedKeyCredentials).ToString();
        }
    }
}
