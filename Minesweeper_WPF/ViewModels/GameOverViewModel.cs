using System.Windows.Media;

namespace Minesweeper_WPF.ViewModels
{
    class GameOverViewModel : BaseViewModel
    {
        private string displayText;
        private SolidColorBrush backgroundColor;

        public string GameOverText { get; set; } = "На жаль, ви програли. Наступний раз пощастить більше !";

        public string GameWinText { get; set; } = "На жаль, ви перемогли. Наступний раз пощастить менше !";

        public SolidColorBrush GameOverBackground { get; set; }
        public SolidColorBrush GameWinBackground { get; set; }

        public GameOverViewModel(bool isGameWin=false)
        {
            GameOverBackground = new SolidColorBrush(Color.FromRgb(222, 48, 51));
            GameWinBackground = new SolidColorBrush(Color.FromRgb(86, 222, 78));
            if (isGameWin)
            {
                DisplayText = GameWinText;
                BackgroundColor = GameWinBackground;
            }
            else
            {
                DisplayText = GameOverText;
                BackgroundColor = GameOverBackground;
            }
        }
        public string DisplayText
        {
            get => displayText;
            set
            {
                displayText = value;
                OnePropertyChanged();
            }
        }

        public SolidColorBrush BackgroundColor 
        { 
            get => backgroundColor;
            set 
            {
                backgroundColor = value;
                OnePropertyChanged();
            }
        }
    }
}
