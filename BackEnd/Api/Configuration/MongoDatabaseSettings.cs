using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Configuration
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
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
