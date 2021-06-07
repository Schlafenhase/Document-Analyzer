using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Models
{
    public class MongoSentimentAnalysisFile
    {
        /// <summary>
        /// Atributes of MongoSentimentAnalysisFile
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int FileId { get; set; }
        public int ResultPercentage { get; set; }
        public string ResultMessage { get; set; }
    }
}
