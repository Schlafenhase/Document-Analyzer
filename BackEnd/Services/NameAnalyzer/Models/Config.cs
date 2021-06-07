using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DANameAnalyzer.Models
{
    public class Config
    {
        /// <summary>
        /// Atributes of Config
        /// </summary>
        public string HostName { get; set; }
        public string QueueName { get; set; }
        public string SqlConnection { get; set; }
        public string MongoCollectionName { get; set; }
        public string MongoConnectionString { get; set; }
        public string MongoDatabaseName { get; set; }
    }
}
