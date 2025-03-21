using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace parkMasterD.utils
{
    public class TechnicalSpecsData
    {
        public string OperatingSystem { get; set; }
        public string Processor { get; set; }
        public long TotalRamSize { get; set; }
        public string TotalStorage { get; set; }
        public string GraphicsCard { get; set; }
        public long FreeRamSize { get; set; }
        public long FreeStorage { get; set; }
    }
    public class TechnicalSpecs
    {
        public string OperatingSystem { get; set; }
        public string Processor { get; set; }
        public long TotalRamSize { get; set; }
        public string TotalStorage { get; set; }
        public string GraphicsCard { get; set; }
        public long FreeRamSize { get; set; }
        public long FreeStorage { get; set; }

        private void _GetSystemInfo()
        {
            OperatingSystem = GetOperatingSystem();

            Processor = GetProcessor();

            TotalRamSize = GetTotalRam();

            TotalStorage = GetTotalStorage();

            GraphicsCard = GetGraphicsCard();

            FreeRamSize = GetFreeRam();

            FreeStorage = GetFreeStorage();
        }

        private string GetOperatingSystem()
        {
            string os = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                os = queryObj["Caption"].ToString();
            }
            return os;
        }

        private string GetProcessor()
        {
            string processor = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                processor = queryObj["Name"].ToString();
            }
            return processor;
        }

        private long GetTotalRam()
        {
            long totalRam = 0;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                totalRam = Convert.ToInt64(queryObj["TotalPhysicalMemory"]);
            }
            return totalRam;
        }

        private string GetTotalStorage()
        {
            string totalStorage = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DriveType=3");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                totalStorage = (Convert.ToInt64(queryObj["Size"]) / (1024 * 1024 * 1024)).ToString() + " GB";
            }
            return totalStorage;
        }

        private string GetGraphicsCard()
        {
            string graphicsCard = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                graphicsCard = queryObj["Caption"].ToString();
            }
            return graphicsCard;
        }

        private long GetFreeRam()
        {
            long freeRam = 0;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                freeRam = Convert.ToInt64(queryObj["FreePhysicalMemory"]);
            }
            return freeRam;
        }

        private long GetFreeStorage()
        {
            long freeStorage = 0;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DriveType=3");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                freeStorage = Convert.ToInt64(queryObj["FreeSpace"]);
            }
            return freeStorage;
        }

        public void ShowSystemInfoInMessageBox()
        {
        
            _GetSystemInfo();

            // Construire le message
            string message = $"Système d'exploitation: {OperatingSystem}\n" +
                             $"Processeur: {Processor}\n" +
                             $"RAM totale: {TotalRamSize / (1024 * 1024 * 1024)} Go\n" +
                             $"RAM libre: {FreeRamSize / 1024} Mo\n" + // Affichage en Mo
                             $"Stockage total: {TotalStorage}\n" +
                             $"Espace libre: {FreeStorage / (1024 * 1024 * 1024)} Go\n" + // Affichage en Go
                             $"Carte graphique: {GraphicsCard}";

       
            MessageBox.Show(message, "Informations Système", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public TechnicalSpecsData GetSystemInfo()
        {
            _GetSystemInfo();
            return new TechnicalSpecsData
            {
                OperatingSystem = OperatingSystem,
                Processor = Processor,
                TotalRamSize = TotalRamSize,
                TotalStorage = TotalStorage,
                GraphicsCard = GraphicsCard,
                FreeRamSize = FreeRamSize,
                FreeStorage = FreeStorage
            };
        }

    }
}
