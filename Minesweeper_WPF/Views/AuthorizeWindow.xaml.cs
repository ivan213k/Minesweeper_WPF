using Minesweeper_WPF.Models;
using System.Windows;
using System.Windows.Controls;

namespace Minesweeper_WPF.Views
{
    /// <summary>
    /// Interaction logic for AuthorizeWindow.xaml
    /// </summary>
    public partial class AuthorizeWindow : Window
    {
        private UserManager userManager = new UserManager();
        public AuthorizeWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (buttonLogin!=null)
            {
                buttonLogin.Content = "Вхід";
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            buttonLogin.Content = "Реєстрація";
        }

        private async void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Password.Password.Length<4)
            {
                MessageBox.Show("Пароль не може містити менше 4 символів!",
                        "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            EnableProgressBar();
            if (radioButtonAuthorization.IsChecked == true)
            {
                if (await userManager.Authorize(UserName.Text, Password.Password))
                {
                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не вдалось авторизуватись. Можливо nickName або пароль введено не вірно." +
                        "Спробуйте ще раз", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            if (radioButtonRegistration.IsChecked == true)
            {
                if (await userManager.Registrate(UserName.Text, Password.Password))
                {
                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не вдалось зареєструватись. Можливо цей nickName вже зайнятий" +
                        "Спробуйте ще раз", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            DisableProgressBar();
        }
        private void EnableProgressBar()
        {
            ProgressBar.Visibility = Visibility.Visible;
            ProgressBar.IsIndeterminate = true;
        }
        private void DisableProgressBar()
        {
            ProgressBar.Visibility = Visibility.Collapsed;
            ProgressBar.IsIndeterminate = false;
        }
    }
}
