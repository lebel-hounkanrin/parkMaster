using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UserService _userService;
        public MainWindow(UserService userService)
        {
            _userService = userService;
            InitializeComponent();
            if (!IsUserLoggedIn())
            {
                var loginWindow = new LoginWindow(_userService);
                loginWindow.Show();
                //this.Close(); // Fermez MainWindow si l'utilisateur n'est pas connecté
            }
        }

        public static bool IsUserLoggedIn()
        {
            return Properties.Settings.Default.IsUserLoggedIn;
        }
    }
}