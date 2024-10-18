using System;
using StackExchange.Redis;

namespace LeaderBoard
{
    internal class Program
    {
        private static readonly
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
        private static IDatabase db;
        static void Main(string[] args)
        {
             db = redis.GetDatabase();
            CreateLeaderBoard();
            DisplayLeaderBoard();

            Console.WriteLine("adding new record");
            db.SortedSetAdd("leaderboard1","player4",80);
            
            DisplayLeaderBoard();
        }
        private static void CreateLeaderBoard()
        {
            db.SortedSetAdd("leaderboard1", "player1", 100);
            db.SortedSetAdd("leaderboard1", "player2", 200);
            db.SortedSetAdd("leaderboard1", "player3",150);
        }
        private static void DisplayLeaderBoard()
        {
            var leaderboard = db.SortedSetRangeByRankWithScores("leaderboard1", 0, -1, Order.Descending);
            Console.WriteLine("Leaderboard1:");
            foreach (var entry in leaderboard)
            {
                Console.WriteLine($"{entry.Element}:{entry.Score}");
            }
        }
    }
}