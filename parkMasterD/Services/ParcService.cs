using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace parkMasterD.Services
{
    public class Parc
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Salle> Salles { get; set; }
    }
    public class Salle
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ParcService
    {
        private static readonly HttpClient http = new HttpClient();

        string apiUrl = Properties.Settings.Default.ApiUrl;
        public async Task<List<Parc>> GetExistingParcAsync()
        {
            try
            {

                var token = Properties.Settings.Default.UserToken;
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{apiUrl}/parc");
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await http.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    var parcsStream = await response.Content.ReadAsStringAsync();
                    //var parcs = await JsonSerializer.DeserializeAsync<List<Parc>>(parcsStream);
                    var parcs = JsonConvert.DeserializeObject<List<Parc>>(parcsStream);
                    //List<String> parcNames = parcs.Select(parc => parc.Name).ToList();
                    return parcs;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Properties.Settings.Default.IsUserLoggedIn = false;
                    Properties.Settings.Default.UserToken = String.Empty;
                    Properties.Settings.Default.Username = String.Empty;
                    Properties.Settings.Default.Save();
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des parcs", ex);
            }
        }
    }
}
