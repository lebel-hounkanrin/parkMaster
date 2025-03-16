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
using parkMasterD.Services;

namespace parkMasterD
{
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserService uservice;
        private readonly ParcService _pservice;
        public LoginWindow(UserService service, ParcService pservice)
        {

            uservice = service;
            _pservice = pservice;
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTextbox.Text;
            string password = passwordTextbox.Password;

            try
            {
                // Appelez la méthode AuthenticateAsync pour récupérer le token
                string token = await uservice.AuthenticateUserAsync(email, password);

                // Stockez le token localement (par exemple, dans Settings)
                Properties.Settings.Default.UserToken = token;
                Properties.Settings.Default.IsUserLoggedIn = true;
                Properties.Settings.Default.Save();
                Application.Current.MainWindow = new MainWindow(uservice, _pservice);
                Application.Current.MainWindow.Show();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}