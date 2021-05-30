using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Interfaces
{
    public interface IAzureBlobAuthService
    {
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
        string GenerateSAS(string key, string accountName);
    }
}
