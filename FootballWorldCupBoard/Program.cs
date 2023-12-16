using FootballWorldCupBoard.Models;
using FootballWorldCupBoard.Services;

namespace FootballWorldCupBoard
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ScoreboardService scoreboardService = new();
            GameService gameService = new();
            List<Game> games = new();
            
            games.AddRange(new[]
            {
                gameService.CreateGame(1, "Mexico", "Canada"),
                gameService.CreateGame(2, "Spain", "Brazil"),
                gameService.CreateGame(3, "Germany", "France"),
                gameService.CreateGame(4, "Uruguay", "Italy"),
                gameService.CreateGame(5, "Argentina", "Australia")
            });

            Console.WriteLine("Initial Games:");
            foreach (var game in games)
            {
                Console.WriteLine($"{game.HomeTeam.Name} {game.HomeScore} - {game.AwayScore} {game.AwayTeam.Name}");
            }

            Console.WriteLine();

            games.ForEach(game =>
            {
                scoreboardService.AddGame(game);
            });


            await scoreboardService.SimulateLiveUpdates(60);

            scoreboardService.DisplayFinalResults();

            Console.ReadLine();
        }
    }
}