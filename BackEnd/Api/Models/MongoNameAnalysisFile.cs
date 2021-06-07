using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Models
{
    public class MongoNameAnalysisFile
    {
        /// <summary>
        /// Atributes of MongoNameAnalysisFile
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int FileId { get; set; }
        public int AnalysisDone { get; set; }
        public Dictionary<string, int> Result { get; set; }
    }
}
