from pymongo import MongoClient

class MongoFileService:
    def __init__(self, connectionString, databaseName, collectionName):
        self.client = MongoClient(connectionString)
        self.database = self.client[databaseName]
        self.mongoFiles = self.database[collectionName]

    # Method that finds one file in the mongo database
    # Returns the mongo file found
    def get(self, id):
        return self.mongoFiles.find_one({"FileId": id})

    # Method that updates a file in the mongo database
    def update(self, id, fileIn):
        self.mongoFiles.replace_one({"FileId": id}, fileIn)

