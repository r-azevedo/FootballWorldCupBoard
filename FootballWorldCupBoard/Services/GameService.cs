using FootballWorldCupBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballWorldCupBoard.Services
{
    public class GameService
    {
        public GameService() { }

        public Game CreateGame(int id, string homeTeam, string awayTeam)
        {
            Game game = new()
            {
                Id = id,
                HomeTeam = new Team() { Name = homeTeam },
                AwayTeam = new Team() { Name = awayTeam },
                HomeScore = 0,
                AwayScore = 0,
            };

            return game;
        }

    }
}
