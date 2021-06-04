using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DANameAnalyzer.Models
{
    public class MongoFile
    {
        /// <summary>
        /// Atributes of MongoFile
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int FileId { get; set; }
        public Dictionary<string, int> NA { get; set; }
        public int SAPercentage { get; set; }
        public string SAMessage { get; set; }
        public int SWACount { get; set; }
    }
}
