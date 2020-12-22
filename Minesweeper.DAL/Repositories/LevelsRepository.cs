using Minesweeper.DAL.EF;
using Minesweeper.DAL.Entities;
using Minesweeper.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.DAL.Repositories
{
    public class LevelsRepository : IRepository<Level>
    {
        minesweeperContext dbContext = new minesweeperContext();

        public async Task CreateAsync(Level item)
        {
            await dbContext.Levels.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var level = await dbContext.Levels.FindAsync(id);
            if (level != null)
            {
                dbContext.Levels.Remove(level);
                await dbContext.SaveChangesAsync();
            }
        }

        public IEnumerable<Level> Find(Func<Level, bool> predicate)
        {
            return dbContext.Levels.Where(predicate).ToList();
        }

        public IEnumerable<Level> GetAll()
        {
            return dbContext.Levels.ToList();
        }

        public async Task<Level> GetAsync(int id)
        {
            return await dbContext.Levels.FindAsync(id);
        }

        public async Task UpdateAsync(Level item)
        {
            if (item != null)
            {
                dbContext.Levels.Update(item);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
