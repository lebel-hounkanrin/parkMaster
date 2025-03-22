using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using parkMasterD.utils;

namespace parkMasterD.Services
{
    public class DeviceService
    {
        private static readonly HttpClient http = new HttpClient();
        string apiUrl = Properties.Settings.Default.ApiUrl;

        public async Task<bool> createTechnicalSpec()
        {
            try
            {
                var technicalSpecs = new TechnicalSpecs();
                var data = technicalSpecs.GetSystemInfo();
                var deviceData = new
                {
                    DeviceId = Properties.Settings.Default.DeviceId,
                    OperatingSystem = data.OperatingSystem,
                    Processor = data.Processor,
                    TotalRamSize = data.TotalRamSize.ToString(),
                    TotalStorage = data.TotalStorage.ToString(),
                    GraphicsCard = data.GraphicsCard,
                    FreeRamSize = data.FreeRamSize.ToString(),
                    FreeStorage = data.FreeStorage.ToString()
                };
                var token = Properties.Settings.Default.UserToken;
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{apiUrl}/DeviceTechnicalSpecs");
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                requestMessage.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(deviceData), Encoding.UTF8, "application/json");
                var response = await http.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode)
                {
                    Properties.Settings.Default.IsDeviceCreated = true;
                    Properties.Settings.Default.Save();
                    return true;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Properties.Settings.Default.IsUserLoggedIn = false;
                    Properties.Settings.Default.UserToken = String.Empty;
                    Properties.Settings.Default.Username = String.Empty;
                    Properties.Settings.Default.Save();
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'envoi des informations relatives aux détails techniques", ex);
            }
        }

        public async Task<bool> createNetworkInfo()
        {
            try
            {
                var networkInfo = new NetworkInfo();
                var data = networkInfo.GetNetworkInfo();
                var deviceData = new
                {
                    DeviceId = Properties.Settings.Default.DeviceId,
                    IpAddress = data.IpAddress,
                    MacAddress = data.MacAddress,
                    Hostname = data.Hostname,
                    Network = data.Network,
                    ConnectionType = data.ConnectionType
                };
                var token = Properties.Settings.Default.UserToken;
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{apiUrl}/DeviceNetworkInfo");
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                requestMessage.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(deviceData), Encoding.UTF8, "application/json");
                var response = await http.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode)
                {
                    //Properties.Settings.Default.IsNetworkInfoCreated = true;
                    //Properties.Settings.Default.Save();
                    //return true;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Properties.Settings.Default.IsUserLoggedIn = false;
                    Properties.Settings.Default.UserToken = String.Empty;
                    Properties.Settings.Default.Username = String.Empty;
                    Properties.Settings.Default.Save();
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'envoi des informations relatives au réseau", ex);
            }
        }

        public async Task<bool> createVariableInfo()
        {
            try
            {
                var variableInfo = new SystemPerformanceInfo();
                var data = variableInfo.GetSystemPerformanceInfo();
                var deviceData = new
                {
                    DeviceId = Properties.Settings.Default.DeviceId,
                    FreeStorage = data.FreeStorage.ToString(),
                    FreeRamSize = data.FreeRamSize.ToString(),
                    //CpuUsage = data.CpuUsage,
                    //RamUsage = data.RamUsage,
                    DiskRead = data.DiskRead.ToString(),
                    DiskWrite = data.DiskWrite.ToString(),
                    NetSend = data.NetSend.ToString(),
                    NetReceive = data.NetReceive.ToString()
                };
                var token = Properties.Settings.Default.UserToken;
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{apiUrl}/DeviceVariable");
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                requestMessage.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(deviceData), Encoding.UTF8, "application/json");
                var response = await http.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode)
                {
                    //Properties.Settings.Default.IsVariableInfoCreated = true;
                    //Properties.Settings.Default.Save();
                    //return true;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Properties.Settings.Default.IsUserLoggedIn = false;
                    Properties.Settings.Default.UserToken = String.Empty;
                    Properties.Settings.Default.Username = String.Empty;
                    Properties.Settings.Default.Save();
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'envoi des informations relatives aux variables", ex);
            }
        }
    }
}
