using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int rows = 9;
        int cols = 9;

        Button[,] buttonsMatrix;
        public MainWindow()
        {
            InitializeComponent();
            buttonsMatrix = new Button[rows, cols];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeButtonsMatrix();
            InitializeGrid();
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
    }
}
