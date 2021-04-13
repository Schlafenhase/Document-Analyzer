using DocumentAnalyzerAPI.MVC.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DocumentAnalyzerAPI.MVC.Services
{
    public class MongoFileService
    {
        /// <summary>
        /// Atribute that stores all mongoFiles
        /// </summary>
        private readonly IMongoCollection<MongoFile> _mongoFiles;

        /// <summary>
        /// Constructor of MongoFileService
        /// </summary>
        /// <param name="settings">
        /// MongoDatabase settings
        /// </param>
        public MongoFileService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _mongoFiles = database.GetCollection<MongoFile>(settings.CollectionName);
        }

        /// <summary>
        /// Method that inserts the found entities of the file to the database
        /// </summary>
        /// <param name="result">
        /// Dictionary with the found results
        /// </param>
        /// <param name="fileId">
        /// Integer with the file id
        /// </param>
        public void InsertResults(Dictionary<string, int> result, int fileId)
        {
            MongoFile mongoFile = new MongoFile();
            mongoFile.FileId = fileId;
            mongoFile.Employees = result;
            Create(mongoFile);
            return;
        }

        /// <summary>
        /// Method that searches a mongo database entry with the file id
        /// </summary>
        /// <param name="id">
        /// String with the file id
        /// </param>
        /// <returns>
        /// Mongo entry found in the database
        /// </returns>
        public MongoFile Get(string id) =>
            _mongoFiles.Find<MongoFile>(file => file.FileId == int.Parse(id)).FirstOrDefault();

        /// <summary>
        /// Method that creates a new entry in the mongo database
        /// </summary>
        /// <param name="file">
        /// Mongo file to be inserted
        /// </param>
        /// <returns>
        /// Mongo file inserted
        /// </returns>
        public MongoFile Create(MongoFile file)
        {
            _mongoFiles.InsertOne(file);
            return file;
        }
    }
}
