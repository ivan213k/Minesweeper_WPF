using Minesweeper.DAL.Entities;
using Minesweeper.DAL.Interfaces;
using Minesweeper.DAL.Repositories;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper_WPF.Models
{
    class UserManager
    {
        private string fileName = "currentUserId.txt";
        IRepository<Player> repository = new PlayersRepository();
        public async Task<bool> IsAuthorized()
        {
            if (!File.Exists(fileName))
            {
                return false;
            }
            string text = File.ReadAllText(fileName);
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }
            if (int.TryParse(text, out int id))
            {
                if (await repository.GetAsync(id) != null)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> Authorize(string nickName, string password)
        {

            var player = repository.Find(r => r.Nickname == nickName && r.Password == password).SingleOrDefault();
            if (player != null)
            {
                await WriteIdInTextFile(player.Id);
                return true;
            }
            return false;

        }
        public async Task<bool> Registrate(string nickName, string password)
        {
            if (repository.GetAll().Any(r => r.Nickname == nickName))
            {
                return false;
            }
            var newPlayer = new Player()
            {
                Nickname = nickName,
                Password = password
            };
            await repository.CreateAsync(newPlayer);
            var id = repository.Find(r => r.Nickname == nickName).Single().Id;
            await WriteIdInTextFile(id);
            return true;
        }
        private async Task WriteIdInTextFile(int id)
        {
            File.WriteAllText(fileName, string.Empty);
            var fileStream = File.Open(fileName, FileMode.OpenOrCreate);
            using (StreamWriter fs = new StreamWriter(fileStream))
            {
                await fs.WriteLineAsync(id.ToString());
            }
        }
        public int GetCurrentUserId()
        {
            string text = File.ReadAllText(fileName);
            return int.Parse(text);
        }
    }
}
