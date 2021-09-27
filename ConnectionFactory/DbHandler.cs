using MongoCRUD.Classes;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MongoCRUD.ConnectionFactory
{
    public class DbHandler
    {
        private readonly IMongoDatabase _db;

        public DbHandler()
        {
            var client = new MongoClient(MongoClientSettings.FromConnectionString("mongodb://localhost:27017"));
            _db = client.GetDatabase("Database0");
        }

        public List<Player> GetAllPlayers() => GetPlayers(new BsonDocument());
        public List<Player> GetPlayers(BsonDocument filter)
        {
            var playersCollection = _db.GetCollection<BsonDocument>("Players");
            var bsonPlayers = playersCollection.Find(new BsonDocument(filter)).ToList();
            var players = new List<Player>();
            bsonPlayers.ForEach(bsonPlayer => players.Add(new Player
            {
                Id = bsonPlayer["_id"].AsObjectId,
                RoomId = bsonPlayer["roomId"].AsString,
                Position = new Position(bsonPlayer["position"]["x"].AsInt32, bsonPlayer["position"]["y"].AsInt32)
            }));
            return players;
        }
        public void InsertPlayer(Player newPlayer)
        {
            var playersCollection = _db.GetCollection<BsonDocument>("Players");
            BsonDocument newPlayerBson = new BsonDocument
            {
                { "_id", new BsonObjectId(ObjectId.GenerateNewId()) },
                { "roomId", newPlayer.RoomId},
                { "position", new BsonDocument
                    {
                        { "x", newPlayer.Position.X },
                        { "y", newPlayer.Position.Y }
                    }
                },
            };
            playersCollection.InsertOne(newPlayerBson);
        }
        public void UpdatePlayerById(BsonObjectId filter, Player player)
        {
            var playersColletion = _db.GetCollection<BsonDocument>("Players");
            BsonDocument playerBson = new BsonDocument
            {
                { "roomId", player.RoomId},
                { "position", new BsonDocument
                    {
                        { "x", player.Position.X },
                        { "y", player.Position.Y }
                    }
                },
            };
            playersColletion.ReplaceOne(new BsonDocument("_id", filter), playerBson);
        }
        public void DeletePlayerById(BsonDocument filter)
        {
            var playersColletion = _db.GetCollection<BsonDocument>("Players");
            playersColletion.DeleteMany(filter);
        }
    }
}