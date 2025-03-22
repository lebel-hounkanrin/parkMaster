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
using System.Windows.Shapes;
using parkMasterD.Services;
using parkMasterD.utils;

namespace parkMasterD
{
    /// <summary>
    /// Logique d'interaction pour DeviceData.xaml
    /// </summary>
    public partial class DeviceData : Window
    {
        public DeviceData()
        {
            InitializeComponent();
            TechnicalSpecs technicalSpecs = new TechnicalSpecs();
            //NetworkInfo networkInfo = new NetworkInfo();
            //SystemPerformanceInfo systemPerformanceInfo = new SystemPerformanceInfo();
            technicalSpecs.ShowSystemInfoInMessageBox();
            //networkInfo.ShowNetworkInfoInMessageBox();
            //systemPerformanceInfo.ShowSystemPerformanceInfo();

            DeviceService deviceService = new DeviceService();
            deviceService.createVariableInfo();
        }
    }
}
