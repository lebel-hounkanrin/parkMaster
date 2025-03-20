using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;


namespace parkMasterD.utils
{
   

    public class SystemInfo
    {
        public string Name { get; set; }
        public string DeviceType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }

        public static SystemInfo GetSystemInfo()
        {
            var systemInfo = new SystemInfo();

            // Récupérer le nom de l'ordinateur
            systemInfo.Name = GetWMIValue("SELECT Name FROM Win32_ComputerSystem");

            // Récupérer le type de périphérique (ex. Desktop, Laptop, etc.)
            systemInfo.DeviceType = GetWMIValue("SELECT PCSystemType FROM Win32_ComputerSystem");

            // Récupérer la marque de l'ordinateur (ex. Dell, HP, etc.)
            systemInfo.Brand = GetWMIValue("SELECT Manufacturer FROM Win32_ComputerSystem");

            // Récupérer le modèle de l'ordinateur
            systemInfo.Model = GetWMIValue("SELECT Model FROM Win32_ComputerSystem");

            // Récupérer le numéro de série de l'ordinateur
            systemInfo.SerialNumber = GetWMIValue("SELECT SerialNumber FROM Win32_BIOS");

            return systemInfo;
        }

        private static string GetWMIValue(string query)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                ManagementObjectCollection queryCollection = searcher.Get();

                foreach (ManagementObject m in queryCollection)
                {
                    foreach (PropertyData property in m.Properties)
                    {
                        if (property.Name == "Name")
                        {
                            return property.Value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion des erreurs
                return $"Erreur : {ex.Message}";
            }
            return string.Empty;
        }
    }

}
