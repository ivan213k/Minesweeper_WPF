using System.IO;
using System.Threading.Tasks;

namespace Minesweeper_WPF.Models
{
    class UserManager
    {
        private string fileName = "currentUserId.txt";
        public bool IsAuthorized()
        {
            if (!File.Exists(fileName))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(File.ReadAllText(fileName)))
            {
                return false;
            }
            return true;
        }
        public async Task<bool> Authorize(string nickName, string password)
        {
            var fileStream = File.Open(fileName, FileMode.OpenOrCreate);
            using (StreamWriter fs = new StreamWriter(fileStream))
            {
                await fs.WriteLineAsync(nickName);
            }
            return true;
        }
        public async Task<bool> Registrate(string nickName, string password)
        {
            var fileStream = File.Open(fileName, FileMode.OpenOrCreate);
            using (StreamWriter fs = new StreamWriter(fileStream))
            {
                await fs.WriteLineAsync(nickName);
            }
            return true;
        }
    }
}
