using FootballWorldCupBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballWorldCupBoard.Services
{
    public class ScoreboardService
    {
        public List<Game> games = new ();
        
        public ScoreboardService() { }

        public void StartGame(Game game)
        {
            games.Add(game);
        }

        public void UpdateScore(Game game, int homeScore, int awayScore)
        {
            var gameToUpdate = games.FirstOrDefault(g => g.Id == game.Id);
            
            if (gameToUpdate != null)
            {
                gameToUpdate.HomeScore = homeScore;
                gameToUpdate.AwayScore = awayScore;
            }
        }

        public int GetTotalGames()
        {
            return games.Count;
        }
        
        public Game GetGame(int id)
        {
            var game = games.FirstOrDefault(x => x.Id == id);
            
            if (game != null)
                return game;

            return null;
        }

        public List<Game> GetSummaryByTotalScore()
        {
            return games.OrderByDescending(g => g.HomeScore + g.AwayScore).ThenByDescending(g => g.Id).ToList();
        }

        public async Task SimulateLiveUpdates(int totalSeconds)
        {
            Random random = new Random();

            for (int seconds = 1; seconds <= totalSeconds;)
            {
                // Pick a random game to update
                var gameToUpdate = GetGame(random.Next(games.Count+1));

                if (gameToUpdate is null)
                    continue;

                var totalGoals = gameToUpdate.HomeScore + gameToUpdate.AwayScore;

                if (totalGoals > 10) //Ignore absurd results
                    continue;

                // Decide whether to update home score or away score
                bool updateHomeScore = random.Next(2) == 0;

                if (updateHomeScore)
                {
                    UpdateScore(gameToUpdate, gameToUpdate.HomeScore + 1, gameToUpdate.AwayScore);
                }
                else
                {
                    UpdateScore(gameToUpdate, gameToUpdate.HomeScore, gameToUpdate.AwayScore + 1);
                }

                var getUpdatedGame = GetGame(gameToUpdate.Id);
                Console.WriteLine($"Update {seconds}' : {getUpdatedGame.HomeTeam.Name} {getUpdatedGame.HomeScore} - {getUpdatedGame.AwayScore} {getUpdatedGame.AwayTeam.Name}");


                //Simulate delay
                int delaySeconds = random.Next(0, 7);
                await Task.Delay(delaySeconds * 1000);

                
                seconds += delaySeconds;
            }
        }

        public void DisplayFinalResults()
        {
            Console.WriteLine("\nGame Summary:");
            int rank = 1;

            List<Game> games = GetSummaryByTotalScore();

            foreach (Game game in games)
            {
                Console.WriteLine($"{rank}. {game.HomeTeam.Name} {game.HomeScore} - {game.AwayScore} {game.AwayTeam.Name}");
                rank++;
            }
        }
    }
}
