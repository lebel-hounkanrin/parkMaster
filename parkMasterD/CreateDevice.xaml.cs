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
        public CreateDevice()
        {
            parcService = new ParcService();
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            parcNamesComboBox.ItemsSource = await parcService.GetExistingParcAsync();
        }
        private void parcNamesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        
            var selectedParc = parcNamesComboBox.SelectedItem as Parc;

            if (selectedParc != null)
            {
                sallesComboBox.ItemsSource = selectedParc.Salles;
            }
            else
            {
                sallesComboBox.ItemsSource = null;
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
