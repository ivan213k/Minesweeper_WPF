using Minesweeper_WPF.Core.Abstractions;
using Minesweeper_WPF.Core.Core;
using Minesweeper_WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_WPF.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        private IGameConfiguration selectedConfiguration;

        public List<IGameConfiguration> GameConfigurations { get; set; }

        public IGameConfiguration SelectedConfiguration 
        { 
            get => selectedConfiguration; 
            set 
            { 
               selectedConfiguration = value; ApplyCommand.OneExecuteChaneged(); 
            } 
        }

        public int Rows { get; set; }

        public int Cols { get; set; }

        public int BombsCount { get; set; }

        public bool IsSpecialConfigurationSelected { get; set; }
        public SettingsViewModel()
        {
            GameConfigurations = new List<IGameConfiguration>();
            GameConfigurations = CreateDefaultConfigurations();
            ApplyCommand = new Command(Apply,CanApply);
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
            window.Close();
               
        }
        private bool CanApply(object parametr = null)
        {
            if (SelectedConfiguration != null)
            {
                return true;
            }
            return false;
        }
    }
}
