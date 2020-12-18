using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Minesweeper_WPF.Core.Abstractions;
using Minesweeper_WPF.Core.Core;
using Minesweeper_WPF.Models;
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
        DispatcherTimer timer;
        public int time = 0;
        private CellToImageConverter cellToImageConverter = new CellToImageConverter();
        public MainWindow()
        {
            InitializeComponent();
            gameConfiguration = new GameConfiguration();
            rows = gameConfiguration.Rows;
            cols = gameConfiguration.Columns;
            buttonsMatrix = new Button[rows, cols];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeButtonsMatrix();
            InitializeGrid();
            Game = new Game(gameConfiguration);
            Game.OnGameWin += OnGameWin;
            Game.OnGameOver += OnGameOver;
            timer = new DispatcherTimer();
            timer.Tick += timerTick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        private void OnGameWin()
        {
            MessageBox.Show("Win!");
        }
        private void OnGameOver()
        {
            MessageBox.Show("Game Over");
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
                    buttonsMatrix[i, j].Click += ButtonLeftClick;
                    buttonsMatrix[i, j].MouseRightButtonUp += ButtonRightClick;
                }
        }

        private void ButtonRightClick(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            if (button.Tag?.ToString() == "marked")
            {
                button.Content = "";
                Game.RemoveBombMark(ParseButtonCoordinates(button));
            }
            else
            {
                button.Tag = "marked";
                button.Content = cellToImageConverter.GetFlaggedImage();
                Game.MarkCellAsBomb(ParseButtonCoordinates(sender as Button));
            }      
        }

        void ButtonLeftClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            var point = ParseButtonCoordinates(button);
            if (button.Tag?.ToString()=="flaged")
            {
                Game.RemoveBombMark(point);
            }
            var cellsShouldBeOpened = Game.OpenCell(point);
            foreach (var cell in cellsShouldBeOpened)
            {
                buttonsMatrix[cell.RowIndex, cell.ColumnIndex].Content = cellToImageConverter.ConvertToImage(cell);
            }
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
        private Point ParseButtonCoordinates(Button button)
        {
            int i = int.Parse(button.Name.Substring(button.Name.IndexOf("row") + 3, button.Name.IndexOf("col") - (button.Name.IndexOf("row") + 3)));
            int j = int.Parse(button.Name.Substring(button.Name.IndexOf("col") + 3));
            return new Point(i, j);
        }
    }
}
