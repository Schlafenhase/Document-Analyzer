namespace DocumentAnalyzerAPI.MVC.Models
{
    public class MongoDatabaseSettings: IMongoDatabaseSettings
    {
        /// <summary>
        /// Atributes of MongoDatabaseSettings
        /// </summary>
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public class IMongoDatabaseSettings
    {
        /// <summary>
        /// Atributes of IMongoDatabaseSettings
        /// </summary>
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
