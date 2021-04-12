using DocumentAnalyzerAPI.MVC.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DocumentAnalyzerAPI.MVC.Services
{
    public class MongoFileService
    {
        private readonly IMongoCollection<MongoFile> _mongoFiles;

        public MongoFileService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _mongoFiles = database.GetCollection<MongoFile>(settings.CollectionName);
        }

        public void InsertResults(Dictionary<string, int> result, int fileId)
        {
            MongoFile mongoFile = new MongoFile();
            mongoFile.FileId = fileId;
            mongoFile.Employees = result;
            Create(mongoFile);
            return;
        }

        public List<MongoFile> Get() =>
            _mongoFiles.Find(file => true).ToList();

        public MongoFile Get(string id) =>
            _mongoFiles.Find<MongoFile>(file => file.FileId == int.Parse(id)).FirstOrDefault();

        public MongoFile Create(MongoFile file)
        {
            _mongoFiles.InsertOne(file);
            return file;
        }

        public void Update(string id, MongoFile fileIn) =>
            _mongoFiles.ReplaceOne(file => file.Id == id, fileIn);

        public void Remove(MongoFile fileOut) =>
            _mongoFiles.DeleteOne(file => file.Id == fileOut.Id);

        public void Remove(string id) =>
            _mongoFiles.DeleteOne(file => file.Id == id);
    }
}
