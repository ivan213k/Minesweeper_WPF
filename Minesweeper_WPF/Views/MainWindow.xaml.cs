using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Minesweeper.DAL.Entities;
using Minesweeper_WPF.Core;
using Minesweeper_WPF.Core.Abstractions;
using Minesweeper_WPF.Core.Core;
using Minesweeper_WPF.Models;
using Minesweeper_WPF.ViewModels;
using Minesweeper_WPF.Views;
using Point = Minesweeper_WPF.Core.Core.Point;

namespace Minesweeper_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int rows;
        int cols;
        IGameConfiguration gameConfiguration;
        IGame Game;

        Button[,] buttonsMatrix;
        DispatcherTimer timer = new DispatcherTimer();
        public int time = 0;
        private CellToImageConverter cellToImageConverter = new CellToImageConverter();

        int unmarkedBombsCount;
        UserManager userManager = new UserManager();
        StatisticManager statisticManager = new StatisticManager();
        Level currentLevel;
        public MainWindow()
        {
            InitializeComponent();
            gameConfiguration = new GameConfiguration();
            rows = gameConfiguration.Rows;
            cols = gameConfiguration.Columns;
            buttonsMatrix = new Button[rows, cols];
            timer.Tick += timerTick;
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            currentLevel = statisticManager.GetLevels().First();
            if (await userManager.IsAuthorized())
            {
                NewGame();
            }
            else
            {
                AuthorizeWindow authorizeWindow = new AuthorizeWindow();
                if (authorizeWindow.ShowDialog()==true)
                {
                    NewGame();
                }
                else
                {
                    Close();
                }
            }
        }
        private async void OnGameWin()
        {
            timer.Stop();
            if (currentLevel!=null)
            {
                await statisticManager.AddGame(new TimeSpan(0, 0, time), true, currentLevel.Id);
            }
            GameOverWindow gameOverWindow = new GameOverWindow();
            gameOverWindow.DataContext = new GameOverViewModel(isGameWin:true);
            if (gameOverWindow.ShowDialog() == true)
            {
                this.Close();
            }
            else
            {
                NewGame();
            }
        }
        private async void OnGameOver(Cell bombedCell)
        {
            timer.Stop();
            buttonsMatrix[bombedCell.RowIndex, bombedCell.ColumnIndex].Content = cellToImageConverter.ConvertToImage(bombedCell);
            if (currentLevel != null)
            {
                await statisticManager.AddGame(new TimeSpan(0, 0, time), false, currentLevel.Id);
            }
            GameOverWindow gameOverWindow = new GameOverWindow();
            gameOverWindow.DataContext = new GameOverViewModel();
            if (gameOverWindow.ShowDialog() == true)
            {
                this.Close();
            }
            else
            {
                NewGame();
            }
        }
        private void InitializeGrid()
        {
            foreach (var rowDefinition in CreateRowDefinitions(rows))
            {
                GridField.RowDefinitions.Add(rowDefinition);
            }
            foreach (var colDefinition in CreateColsDefinitions(cols))
            {
                GridField.ColumnDefinitions.Add(colDefinition);
            }
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {

                    Grid.SetRow(buttonsMatrix[i, j], i);
                    Grid.SetColumn(buttonsMatrix[i, j], j);
                    GridField.Children.Add(buttonsMatrix[i, j]);
                }
        }
        private void InitializeButtonsMatrix()
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    buttonsMatrix[i, j] = new Button();
                    buttonsMatrix[i, j].Name = "Button" + "row" + i + "col" + j;
                    buttonsMatrix[i, j].Background = new SolidColorBrush(Color.FromRgb(141, 163, 153));
                    buttonsMatrix[i, j].Click += ButtonLeftClick;
                    buttonsMatrix[i, j].MouseRightButtonUp += ButtonRightClick;
                }
        }

        private void ButtonRightClick(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            if (button.Tag?.ToString() == "marked")
            {
                IncrementUnmarkedBombsCount();
                button.Tag = "";
                button.Content = "";
                Game.RemoveBombMark(ParseButtonCoordinates(button));
            }
            else
            {
                DecrementUnmarkedBombsCount();
                button.Tag = "marked";
                button.Content = cellToImageConverter.GetFlaggedImage();
                Game.MarkCellAsBomb(ParseButtonCoordinates(sender as Button));
            }
        }

        void ButtonLeftClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            var point = ParseButtonCoordinates(button);
            if (button.Tag?.ToString()=="marked")
            {
                IncrementUnmarkedBombsCount();
                button.Tag = "";
                Game.RemoveBombMark(point);
            }
            var cellsShouldBeOpened = Game.OpenCell(point);
            foreach (var cell in cellsShouldBeOpened)
            {
                buttonsMatrix[cell.RowIndex, cell.ColumnIndex].Content = cellToImageConverter.ConvertToImage(cell);
            }
            CountBomb.Text = unmarkedBombsCount.ToString();
        }
        private ColumnDefinition[] CreateColsDefinitions(int count)
        {
            ColumnDefinition[] colDefinitions = new ColumnDefinition[count];
            for (int i = 0; i < count; i++)
            {
                colDefinitions[i] = new ColumnDefinition();
            }
            return colDefinitions;
        }
        private RowDefinition[] CreateRowDefinitions(int count)
        {
            RowDefinition[] rowDefinitions = new RowDefinition[count];
            for (int i = 0; i < count; i++)
            {
                rowDefinitions[i] = new RowDefinition();
            }
            return rowDefinitions;
        }

        private void menuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void timerTick(object sender, EventArgs e)
        {
            Timer.Text = time++.ToString();
        }
        private void IncrementUnmarkedBombsCount()
        {
            unmarkedBombsCount++;
            CountBomb.Text = unmarkedBombsCount.ToString();
        }
        private void DecrementUnmarkedBombsCount()
        {
            unmarkedBombsCount--;
            CountBomb.Text = unmarkedBombsCount.ToString();
        }
        private Point ParseButtonCoordinates(Button button)
        {
            int i = int.Parse(button.Name.Substring(button.Name.IndexOf("row") + 3, button.Name.IndexOf("col") - (button.Name.IndexOf("row") + 3)));
            int j = int.Parse(button.Name.Substring(button.Name.IndexOf("col") + 3));
            return new Point(i, j);
        }
        private void NewGame()
        {
            time = 0;
            CountBomb.Text = gameConfiguration.BombsCount.ToString();
            unmarkedBombsCount = gameConfiguration.BombsCount;
            Game = new Minesweeper_WPF.Core.Core.Game(gameConfiguration);
            Game.OnGameWin += OnGameWin;
            Game.OnGameOver += OnGameOver;
            rows = gameConfiguration.Rows;
            cols = gameConfiguration.Columns;
            buttonsMatrix = new Button[rows, cols];
            InitializeButtonsMatrix();
            GridField = new Grid();
            this.Parent.Child = GridField;
            InitializeGrid();
            timer.Start();
        }

        private void menuItemNewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void menuItemGameRules_Click(object sender, RoutedEventArgs e)
        {
            GameRulesWindow gameRulesWindow = new GameRulesWindow();
            gameRulesWindow.Show();
        }

        private void menuItemAboutGame_Click(object sender, RoutedEventArgs e)
        {
            AboutGameWindow aboutGameWindow = new AboutGameWindow();
            aboutGameWindow.ShowDialog();
        }

        private void menuItemSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            if (settingsWindow.ShowDialog()==true)
            {
                var vm = settingsWindow.DataContext as SettingsViewModel;
                gameConfiguration = vm.SelectedConfiguration;
                currentLevel = statisticManager.GetLevels().Where(r => r.SizeHeight == gameConfiguration.Rows && r.SizeWidth == gameConfiguration.Columns).FirstOrDefault();
                NewGame();
            }        
        }

        private void menuItemStatistic_Click(object sender, RoutedEventArgs e)
        {
            StatisticWindow statisticWindow = new StatisticWindow();
            statisticWindow.ShowDialog();
        }
    }
}
