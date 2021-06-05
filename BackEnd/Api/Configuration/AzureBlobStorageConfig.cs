using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Configuration
{
    public class AzureBlobStorageConfig
    {
        /// <summary>
        /// Atributes of AzureBlobStorageConfig
        /// </summary>
        public string ConnectionString { get; set; }
        public string AccountKey { get; set; }
        public string AccountName { get; set; }
        public string StorageUri { get; set; }
    }
}
