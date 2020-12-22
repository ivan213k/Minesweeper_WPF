using Minesweeper.DAL.EF;
using Minesweeper.DAL.Entities;
using Minesweeper.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper.DAL.Repositories
{
    public class GamesRepository : IRepository<Game>
    {
        minesweeperContext dbContext = new minesweeperContext();

        public async Task CreateAsync(Game item)
        {
            await dbContext.Games.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var game = await dbContext.Games.FindAsync(id);
            if (game != null)
            {
                dbContext.Games.Remove(game);
                await dbContext.SaveChangesAsync();
            }
        }

        public IEnumerable<Game> Find(Func<Game, bool> predicate)
        {
            return dbContext.Games.Where(predicate).ToList();
        }

        public IEnumerable<Game> GetAll()
        {
            return dbContext.Games.ToList();
        }

        public async Task<Game> GetAsync(int id)
        {
            return await dbContext.Games.FindAsync(id);
        }

        public async Task UpdateAsync(Game item)
        {
            if (item != null)
            {
                dbContext.Games.Update(item);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
