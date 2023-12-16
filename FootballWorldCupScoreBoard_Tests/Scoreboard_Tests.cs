using FootballWorldCupBoard.Models;
using FootballWorldCupBoard.Services;

namespace FootballWorldCupScoreBoard_Tests
{
    public class Scoreboard_Tests
    {
        private ScoreboardService scoreboardService;
        private GameService gameService;

        [SetUp]
        public void Setup()
        {
            scoreboardService = new ScoreboardService();
            gameService = new GameService();
        }

        [Test]
        public void Game_CreateAGame()
        {
            var game = gameService.CreateGame(0, "A", "B");

            Assert.Multiple(() =>
            {
                Assert.That(game.Id, Is.EqualTo(0));
                Assert.That(game.HomeScore, Is.EqualTo(0));
                Assert.That(game.AwayScore, Is.EqualTo(0));
                Assert.That(game.HomeTeam.Name, Is.EqualTo("A"));
                Assert.That(game.AwayTeam.Name, Is.EqualTo("B"));
            });

        }

        [Test]
        public void StartGame_ShouldAddGameToScoreboard()
        {
            var game = gameService.CreateGame(1, "HomeTeam", "AwayTeam");
            scoreboardService.AddGame(game);

            
            Assert.That(scoreboardService.GetTotalGames(), Is.EqualTo(1));
        }

        [Test]
        public void GetSummaryByTotalScore_ShouldReturnOrderedGames()
        {
            var game1 = gameService.CreateGame(1, "A", "B");
            var game2 = gameService.CreateGame(2, "C", "D");

            scoreboardService.AddGame(game1);
            scoreboardService.AddGame(game2);

            var summary = scoreboardService.GetSummaryByTotalScore();


            Assert.Multiple(() =>
            {
                Assert.That(summary, Has.Count.EqualTo(2));
                Assert.That(summary[0].HomeTeam.Name, Is.EqualTo("C"));
                Assert.That(summary[0].AwayTeam.Name, Is.EqualTo("D"));
            });
        }

        [Test]
        public void GetGame_GetCorrectGameById()
        {
            var game = gameService.CreateGame(0, "A", "B");

            scoreboardService.AddGame(game);
            
            var gameFromScoreboardService = scoreboardService.GetGame(game.Id);

            Assert.That(game, Is.EqualTo(gameFromScoreboardService));
        }


        [Test]
        public void UpdateScore_UpdateTheCorrectGame()
        {
            var game = gameService.CreateGame(0, "A", "B"); 
            var game2 = gameService.CreateGame(1, "C", "D");

            scoreboardService.AddGame(game);
            scoreboardService.AddGame(game2);

            scoreboardService.UpdateScore(game, 1, 0);

            game = scoreboardService.GetGame(game.Id);

            Assert.Multiple(() =>
            {
                Assert.That(game.HomeScore, Is.EqualTo(1));
                Assert.That(game.AwayScore, Is.EqualTo(0));
            });

        }
    }
}