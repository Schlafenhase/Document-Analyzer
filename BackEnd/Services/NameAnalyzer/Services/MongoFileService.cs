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

        /// <summary>
        /// Constructor of MongoFileService
        /// </summary>
        /// <param name="connectionString">
        /// String with the connection string
        /// </param>
        /// <param name="databaseName">
        /// String with the database name
        /// </param>
        /// <param name="collectionName">
        /// String with the collection name
        /// </param>
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

        /// <summary>
        /// Method that updates an item in the database
        /// </summary>
        /// <param name="id">
        /// String with the file id
        /// </param>
        /// <param name="fileIn">
        /// File that is going to replace
        /// </param>
        public void Update(int id, MongoFile fileIn) =>
            _mongoFiles.ReplaceOne(file => file.FileId == id, fileIn);
    }
}
