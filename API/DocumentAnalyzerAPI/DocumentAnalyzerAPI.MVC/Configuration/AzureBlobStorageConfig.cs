using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentAnalyzerAPI.MVC.Configuration
{
    public class AzureBlobStorageConfig
    {
        public string ConectionString { get; set; }
        public string ContainerName { get; set; }
    }
}
