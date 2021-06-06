using DAApi.Configuration;
using DAApi.Interfaces;
using DAApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAApi.Services
{
    public class MongoFileService
    {
        /// <summary>
        /// Atribute that stores all mongoFiles
        /// </summary>
        private readonly IMongoCollection<MongoNameAnalysisFile> _mongoNameAnalysisFiles;
        private readonly IMongoCollection<MongoSentimentAnalysisFile> _mongoSentimentAnalysisFiles;
        private readonly IMongoCollection<MongoSwearAnalysisFile> _mongoSwearAnalysisFiles;

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

            _mongoNameAnalysisFiles = database.GetCollection<MongoNameAnalysisFile>(settings.NameAnalyzerCollectionName);
            _mongoSentimentAnalysisFiles = database.GetCollection<MongoSentimentAnalysisFile>(settings.SentimentAnalyzerCollectionName);
            _mongoSwearAnalysisFiles = database.GetCollection<MongoSwearAnalysisFile>(settings.SwearAnalyzerCollectionName);
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
        public void InsertResults(int fileId)
        {
            MongoNameAnalysisFile mongoNameAnalysisFile = new()
            {
                FileId = fileId,
                AnalysisDone = -1,
                Result = null
            };

            CreateNameAnalysis(mongoNameAnalysisFile);

            MongoSwearAnalysisFile mongoSwearAnalysisFile = new()
            {
                FileId = fileId,
                Result = -1
            };

            CreateSwearAnalysis(mongoSwearAnalysisFile);

            MongoSentimentAnalysisFile mongoSentimentAnalysisFile = new()
            {
                FileId = fileId,
                ResultPercentage = -101,
                ResultMessage = "null"
            };

            CreateSentimentAnalysis(mongoSentimentAnalysisFile);

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
        public string Get(string id)
        {
            var mongoNameAnalysisFile = _mongoNameAnalysisFiles.Find(file => file.FileId == int.Parse(id)).FirstOrDefault();
            var mongoSwearAnalysisFile = _mongoSwearAnalysisFiles.Find(file => file.FileId == int.Parse(id)).FirstOrDefault();
            var mongoSentimentAnalysisFile = _mongoSentimentAnalysisFiles.Find(file => file.FileId == int.Parse(id)).FirstOrDefault();

            BsonDocument NameAnalysisResult = new()
            {
                { "AnalysisDone", mongoNameAnalysisFile.AnalysisDone },
                { "Result", mongoNameAnalysisFile.Result.ToBsonDocument() }
            };

            BsonDocument SwearAnalysisResult = new()
            {
                { "Result", mongoSwearAnalysisFile.Result }
            };

            BsonDocument SentimentAnalysisResult = new()
            {
                { "ResultPercentage", mongoSentimentAnalysisFile.ResultPercentage },
                { "ResultMessage", mongoSentimentAnalysisFile.ResultMessage }
            };

            BsonDocument result = new()
            {
                { "NameAnalysis", NameAnalysisResult },
                { "SwearAnalysis", SwearAnalysisResult },
                { "SentimentAnalysis", SentimentAnalysisResult }
            };

            return result.ToJson();
        }

        /// <summary>
        /// Method that creates a new entry in the mongo database
        /// </summary>
        /// <param name="file">
        /// Mongo file to be inserted
        /// </param>
        /// <returns>
        /// Mongo file inserted
        /// </returns>
        private void CreateNameAnalysis(MongoNameAnalysisFile file)
        {
            _mongoNameAnalysisFiles.InsertOne(file);
            return;
        }

        private void CreateSwearAnalysis(MongoSwearAnalysisFile file)
        {
            _mongoSwearAnalysisFiles.InsertOne(file);
            return;
        }

        private void CreateSentimentAnalysis(MongoSentimentAnalysisFile file)
        {
            _mongoSentimentAnalysisFiles.InsertOne(file);
            return;
        }
    }
}
