using Minesweeper.DAL.EF;
using Minesweeper.DAL.Entities;
using Minesweeper.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper.DAL.Repositories
{
    public class PlayersRepository : IRepository<Player>
    {
        minesweeperContext dbContext = new minesweeperContext();

        public async Task CreateAsync(Player item)
        {
            int id;
            if (dbContext.Players.Count()==0)
            {
                id = 1;
            }
            else
            {
                id = dbContext.Players.Select(r => r.Id).Max() + 1;
            }
            item.Id = id;
            item.IdInfo = 0;
            await dbContext.Players.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var player = await dbContext.Players.FindAsync(id);
            if (player != null)
            {
                dbContext.Players.Remove(player);
                await dbContext.SaveChangesAsync();
            }
        }

        public IEnumerable<Player> Find(Func<Player, bool> predicate)
        {
            return dbContext.Players.Where(predicate).ToList();
        }

        public IEnumerable<Player> GetAll()
        {
            return dbContext.Players.ToList();
        }

        public async Task<Player> GetAsync(int id)
        {
            return await dbContext.Players.FindAsync(id);
        }

        public async Task UpdateAsync(Player item)
        {
            if (item != null)
            {
                dbContext.Players.Update(item);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
