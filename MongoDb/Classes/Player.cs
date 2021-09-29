using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.Classes;
public class Player
{
    public Player(string roomId, Position position)
    {
        Id = new BsonObjectId(new ObjectId(""));
        Position = position;
        RoomId = roomId;
    }

    public Player(BsonObjectId id, string roomId, Position position)
    {
        Id = id;
        RoomId = roomId;
        Position = position;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public BsonObjectId Id { get; set; }

    [BsonElement("position")]
    public Position Position { get; set; }

    [BsonElement("roomId")]
    public string RoomId { get; set; }
}
