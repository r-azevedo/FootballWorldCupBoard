using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballWorldCupBoard.Models
{
    public class Game
    {
        public int Id { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
