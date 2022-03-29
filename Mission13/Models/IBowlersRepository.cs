using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Models
{
    public interface IBowlersRepository
    {
        IQueryable<Bowler> Bowlers { get;}
        IQueryable<Team> Teams { get; }
        void SaveBowler(Bowler b);
        void CreateBowler(Bowler b);
        void DeleteBowler(Bowler b);
    }
}
