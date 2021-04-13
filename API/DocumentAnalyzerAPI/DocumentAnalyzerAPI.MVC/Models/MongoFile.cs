using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DocumentAnalyzerAPI.MVC.Models
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
        public Dictionary<string, int> Employees { get; set; }
    }
}
