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
using Newtonsoft.Json;
using parkMasterD.Services;

namespace parkMasterD
{
    public class UserData
    {
        public string Token { get; set; }
        public string UserName { get; set; }
    }
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserService uservice;
        public LoginWindow(UserService service)
        {

            uservice = service;
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTextbox.Text;
            string password = passwordTextbox.Password;

            try
            {
                // Appelez la méthode AuthenticateAsync pour récupérer le token
                string response = await uservice.AuthenticateUserAsync(email, password);
                var data = JsonConvert.DeserializeObject<UserData>(response);

                // Stockez le token localement (par exemple, dans Settings)
                Properties.Settings.Default.UserToken = data.Token;
                Properties.Settings.Default.Username = data.UserName;
                Properties.Settings.Default.IsUserLoggedIn = true;
                Properties.Settings.Default.Save();
                Application.Current.MainWindow = new MainWindow(uservice);
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