using DANameAnalyzer.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DANameAnalyzer.Services
{
    public class MongoFileService
    {
        /// <summary>
        /// Atribute that stores all mongoFiles
        /// </summary>
        private readonly IMongoCollection<MongoFile> _mongoFiles;

        public MongoFileService(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            _mongoFiles = database.GetCollection<MongoFile>(collectionName);
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
        public MongoFile Get(int id) =>
            _mongoFiles.Find(file => file.FileId == id).FirstOrDefault();


        public void Update(int id, MongoFile fileIn) =>
            _mongoFiles.ReplaceOne(file => file.FileId == id, fileIn);
    }
}
