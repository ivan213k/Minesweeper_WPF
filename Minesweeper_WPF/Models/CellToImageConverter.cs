using Minesweeper_WPF.Core;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Minesweeper_WPF.Models
{
    class CellToImageConverter
    {
        public Image ConvertToImage(Cell cell)
        {
            Image img = new Image();
            string imagePath = GetImagePath(cell);

            img.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            return img;
        } 

        public Image GetFlaggedImage()
        {
            Image img = new Image();
            string imagePath = @"\Resources\flaged.png";
            img.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            return img;
        }
        private string GetImagePath(Cell cell)
        {
            if (cell.IsEmpty)
            {
                return @"\Resources\zero.png";
            }
            if (cell.IsBomb)
            {
                return @"\Resources\bombed.png";
            }
            return $@"\Resources\num{cell.Number}.png";
        }
    }
}
