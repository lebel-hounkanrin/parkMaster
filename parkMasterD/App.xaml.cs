using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using parkMasterD.Services;

namespace parkMasterD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider? ServiceProvider { get; private set; }

        private void ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<UserService>();

            serviceCollection.AddSingleton<LoginWindow>();
            serviceCollection.AddSingleton<MainWindow>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ConfigureServices();
            MainWindow mainWindow = ServiceProvider.GetService<MainWindow>()!;
            mainWindow.Show();

        }
    }

}
