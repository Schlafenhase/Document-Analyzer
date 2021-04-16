namespace DocumentAnalyzerAPI.MVC.Configuration
{
    public class AzureBlobStorageConfig
    {
        /// <summary>
        /// Atributes of AzureBlobStorageConfig
        /// </summary>
        public string ConectionString { get; set; }
        public string AccountKey { get; set; }
        public string ContainerName { get; set; }
        public string AccountName { get; set; }
        public string StorageUri { get; set; }
    }
}
