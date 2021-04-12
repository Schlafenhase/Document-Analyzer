using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentAnalyzerAPI.MVC.Models
{
    public class MongoFile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string[] Employees { get; set; }
    }
}
