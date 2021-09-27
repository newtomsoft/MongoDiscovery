using MongoCRUD.Classes;
using MongoCRUD.ConnectionFactory;
using MongoDB.Bson;
using System;

namespace MongoCRUD
{
    public class Program
    {
        static readonly DbHandler dbHandler = new DbHandler();

        static void Main(string[] args)
        {
            #region GET
            var players = dbHandler.GetAllPlayers();
            foreach (Player currentPlayer in players)
            {
                Console.WriteLine(
                    $"ObjectID: {currentPlayer.Id}\n" +
                    $"RoomId: {currentPlayer.RoomId}\n" +
                    $"PlayerPos: [x: {currentPlayer.Position.X}, y {currentPlayer.Position.X}]\n");
            }
            #endregion

            #region CREATE
            var player = new Player
            {
                Position = new Position(2, 3),
                RoomId = "5"
            };
            dbHandler.InsertPlayer(player);
            #endregion

            #region UPDATE
            var playersToUpdate = dbHandler.GetAllPlayers();
            playersToUpdate[0].Position.X = 6;
            dbHandler.UpdatePlayerById(playersToUpdate[0].Id, playersToUpdate[0]);
            #endregion

            #region DELETE
            dbHandler.DeletePlayerById(new BsonDocument(
                "_id", new BsonObjectId(new ObjectId("611168c15206f112b0ee3ec9"))));
            #endregion
        }
    }
}