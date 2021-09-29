using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.Classes;
public class Player
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public BsonObjectId? Id { get; set; }

    [BsonElement("position")]
    public Position? Position { get; set; }

    [BsonElement("roomId")]
    public string? RoomId { get; set; }
}
