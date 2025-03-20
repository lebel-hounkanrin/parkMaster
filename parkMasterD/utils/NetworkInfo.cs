using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace parkMasterD.utils
{
    public class NetworkInfo
    {
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string Hostname { get; set; }
        public string Network { get; set; }
        public string ConnectionType { get; set; }

        public void GetNetworkInfo()
        {
            // Récupérer l'adresse IP
            IpAddress = GetIpAddress();

            // Récupérer l'adresse MAC
            MacAddress = GetMacAddress();

            // Récupérer le nom d'hôte
            Hostname = GetHostname();

            // Récupérer le nom du réseau
            Network = GetNetwork();

            // Récupérer le type de connexion
            ConnectionType = GetConnectionType();
        }

        private string GetIpAddress()
        {
            string ipAddress = string.Empty;

            // Récupérer l'adresse IP de la première interface réseau active
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) // IPv4
                        {
                            ipAddress = ip.Address.ToString();
                            return ipAddress;
                        }
                    }
                }
            }

            return ipAddress;
        }

        private string GetMacAddress()
        {
            string macAddress = string.Empty;

            // Récupérer l'adresse MAC de la première interface réseau active
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up)
                {
                    macAddress = netInterface.GetPhysicalAddress().ToString();
                    return macAddress;
                }
            }

            return macAddress;
        }

        private string GetHostname()
        {
            return Dns.GetHostName(); // Récupère le nom d'hôte de la machine
        }

        private string GetNetwork()
        {
            string network = "Non connecté";

            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up)
                {
                    network = netInterface.Name;
                    break;
                }
            }

            return network;
        }

        private string GetConnectionType()
        {
            string connectionType = "Non spécifié";

            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up)
                {
                    switch (netInterface.NetworkInterfaceType)
                    {
                        case NetworkInterfaceType.Wireless80211:
                            connectionType = "Wi-Fi";
                            break;
                        case NetworkInterfaceType.Ethernet:
                            connectionType = "Ethernet";
                            break;
                        default:
                            connectionType = "Autre";
                            break;
                    }
                    break;
                }
            }

            return connectionType;
        }

        public void ShowNetworkInfoInMessageBox()
        {
            // Appeler GetNetworkInfo() pour récupérer les informations
            GetNetworkInfo();

            // Construire le message
            string message = $"Adresse IP: {IpAddress}\n" +
                             $"Adresse MAC: {MacAddress}\n" +
                             $"Nom d'hôte: {Hostname}\n" +
                             $"Réseau: {Network}\n" +
                             $"Type de connexion: {ConnectionType}";

            // Afficher les informations dans une MessageBox
            MessageBox.Show(message, "Informations Réseau", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
