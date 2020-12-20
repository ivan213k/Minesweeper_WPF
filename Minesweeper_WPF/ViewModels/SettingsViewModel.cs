using Minesweeper_WPF.Core.Abstractions;
using Minesweeper_WPF.Core.Core;
using Minesweeper_WPF.Views;
using System.Collections.Generic;

namespace Minesweeper_WPF.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        private IGameConfiguration selectedConfiguration;
        private bool isSpecialConfigurationSelected;
        private int rows;
        private int cols;
        private int bombsCount;

        public List<IGameConfiguration> GameConfigurations { get; set; }

        public IGameConfiguration SelectedConfiguration
        {
            get => selectedConfiguration;
            set
            {
                selectedConfiguration = value; ApplyCommand.OneExecuteChaneged();
            }
        }

        public int Rows { get => rows; set { rows = value; ApplyCommand.OneExecuteChaneged(); } }

        public int Cols { get => cols; set { cols = value; ApplyCommand.OneExecuteChaneged(); } }

        public int BombsCount { get => bombsCount; set { bombsCount = value; ApplyCommand.OneExecuteChaneged(); } }

        public bool IsSpecialConfigurationSelected
        {
            get => isSpecialConfigurationSelected;
            set { isSpecialConfigurationSelected = value; ApplyCommand.OneExecuteChaneged(); }
        }
        public SettingsViewModel()
        {
            GameConfigurations = new List<IGameConfiguration>();
            GameConfigurations = CreateDefaultConfigurations();
            ApplyCommand = new Command(Apply, CanApply);
        }

        private List<IGameConfiguration> CreateDefaultConfigurations()
        {
            List<IGameConfiguration> configurations = new List<IGameConfiguration>();
            configurations.Add(new GameConfiguration());
            configurations.Add(new GameConfiguration(16, 16, 40));
            configurations.Add(new GameConfiguration(16, 30, 99));
            return configurations;
        }

        public Command ApplyCommand { get; set; }

        private void Apply(object parametr)
        {
            SettingsWindow window = parametr as SettingsWindow;
            window.DialogResult = true;
            if (IsSpecialConfigurationSelected)
            {
                SelectedConfiguration = new GameConfiguration(Rows, Cols, BombsCount);
            }
            window.Close();

        }
        private bool CanApply(object parametr = null)
        {
            if (IsSpecialConfigurationSelected)
            {
                return IsSpecialConfigurationCorrect();
            }
            if (SelectedConfiguration != null)
            {
                return true;
            }
            return false;
        }
        private bool IsSpecialConfigurationCorrect()
        {
            if (Rows < 2 || Rows > 30)
            {
                return false;
            }
            if (Cols < 2 || Cols > 60)
            {
                return false;
            }
            if (BombsCount >= (Cols * Rows) || BombsCount<1)
            {
                return false;
            }
            return true;
        }
    }
}
