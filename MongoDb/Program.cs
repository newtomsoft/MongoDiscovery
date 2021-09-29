namespace MongoDb;
public class Program
{
    static readonly DbHandler dbHandler = new();

    static void Main(string[] args)
    {
        //GET
        var players = dbHandler.GetAllPlayers();
        foreach (Player currentPlayer in players)
        {
            Console.WriteLine(
                $"ObjectID: {currentPlayer.Id}\n" +
                $"RoomId: {currentPlayer.RoomId}\n" +
                $"PlayerPos: [x: {currentPlayer.Position.X}, y {currentPlayer.Position.X}]\n");
        }

        //CREATE
        var player = new Player("5", new Position(2, 3));
        dbHandler.InsertPlayer(player);

       //UPDATE
        var playersToUpdate = dbHandler.GetAllPlayers();
        playersToUpdate[0].Position.X = 6;
        dbHandler.UpdatePlayerById(playersToUpdate[0].Id, playersToUpdate[0]);

        //DELETE
        dbHandler.DeletePlayerById(new BsonDocument("_id", new BsonObjectId(new ObjectId("611168c15206f112b0ee3ec9"))));
    }
}
