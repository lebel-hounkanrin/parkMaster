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
    /// Logique d'interaction pour CreateDevice.xaml
    /// </summary>
    public partial class CreateDevice : Window
    {
        private readonly ParcService parcService;
        public CreateDevice(ParcService parcService)
        {
            parcService = parcService;
            InitializeComponent();
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            if (parcService != null)
            {
                parcService.GetExistingParcAsync();
            }
            else
            {
                // Handle the null case, possibly by initializing parcService or logging an error
                // parcService = new ParcService(); // Example initialization
            }
        }
        private void okButton_Click(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.IsDeviceCreated = true;
            Properties.Settings.Default.Save();
            DeviceData deviceData = new DeviceData();
            deviceData.Show();
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e) { }
    }
}
