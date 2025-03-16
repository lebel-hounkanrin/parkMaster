using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace parkMasterD.Services
{
    public class Parc
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ParcService
    {
        private static readonly HttpClient http = new HttpClient();

        string apiUrl = Properties.Settings.Default.ApiUrl;
        public async Task<List<String>> GetExistingParcAsync()
        {
            var response = await http.GetAsync($"{apiUrl}/parc");
            if (response.IsSuccessStatusCode)
            {
                var parcsStream = await response.Content.ReadAsStreamAsync();
                var parcs = await JsonSerializer.DeserializeAsync<List<Parc>>(parcsStream);
                List<String> parcNames = parcs.Select(parc => parc.Name).ToList();
                return parcNames;
            }
            else
            {
                throw new Exception("Erreur lors de la récupération des parcs");
            }
        }
    }
}
