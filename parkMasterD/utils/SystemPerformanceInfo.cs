using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace parkMasterD.utils
{
    public class SystemPerformanceInfo
    {
        public long FreeStorage { get; set; }
        public long FreeRamSize { get; set; }
        public long DiskRead { get; set; }
        public long DiskWrite { get; set; }
        public long NetSend { get; set; }
        public long NetReceive { get; set; }

        // Méthode pour récupérer ces informations
        public void GetSystemPerformanceInfo()
        {

            // Obtenir l'espace de stockage libre
            FreeStorage = GetFreeStorage();

            // Obtenir la mémoire RAM libre
            FreeRamSize = GetFreeRam();

            // Obtenir les statistiques de lecture et d'écriture du disque
            (DiskRead, DiskWrite) = GetDiskIOStats();

            // Obtenir les statistiques réseau (envoi et réception)
            (NetSend, NetReceive) = GetNetworkStats();

        }

        private long GetFreeStorage()
        {
            long freeSpace = 0;
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                if (drive.IsReady && drive.DriveType == DriveType.Fixed) // Vérifie les disques durs fixes
                {
                    freeSpace = drive.AvailableFreeSpace;
                    break; // On prend le premier disque fixe
                }
            }

            return freeSpace;
        }

        private long GetFreeRam()
        {
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            return (long)(ramCounter.NextValue() * 1024 * 1024); // Convertir en octets
        }

        private (long diskRead, long diskWrite) GetDiskIOStats()
        {
            PerformanceCounter diskReadCounter = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");
            PerformanceCounter diskWriteCounter = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");

            long diskRead = (long)(diskReadCounter.NextValue());
            long diskWrite = (long)(diskWriteCounter.NextValue());

            return (diskRead, diskWrite);
        }

        private (long netSend, long netReceive) GetNetworkStats()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            long netSend = 0;
            long netReceive = 0;

            foreach (NetworkInterface netInterface in networkInterfaces)
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties properties = netInterface.GetIPProperties();
                    IPv4InterfaceProperties ipv4Properties = properties.GetIPv4Properties();

                    // Compte le nombre de bytes envoyés et reçus
                    netSend += netInterface.GetIPv4Statistics().BytesSent;
                    netReceive += netInterface.GetIPv4Statistics().BytesReceived;
                }
            }

            return (netSend, netReceive);
        }

        public void ShowSystemPerformanceInfo()
        {
            GetSystemPerformanceInfo();

            string message = $"Espace libre disque: {FreeStorage / (1024 * 1024 * 1024)} Go\n" +
                             $"RAM libre: {FreeRamSize / (1024 * 1024)} Mo\n" + // En Mo
                             $"Lecture disque: {DiskRead / (1024 * 1024)} Mo/s\n" + // En Mo
                             $"Écriture disque: {DiskWrite / (1024 * 1024)} Mo/s\n" + // En Mo
                             $"Envoi réseau: {NetSend / (1024 * 1024)} Mo\n" + // En Mo
                             $"Réception réseau: {NetReceive / (1024 * 1024)} Mo\n"  // En Mo
                             ;

            
            MessageBox.Show(message, "Informations Performance Système", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
