using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace parkMasterD.Services;
public class UserService{
    public async Task<string> AuthenticateUserAsync(string email, string password)
    {
        const string apiUrl = "https://localhost:50866";
        using (var client = new HttpClient())
        {
            var loginData = new
            {
                email = email,
                password = password
            };

            //var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");
            var response = await client.PostAsJsonAsync($"{apiUrl}/user/authenticate", loginData);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                return token;  // Vous pouvez également le désérialiser si nécessaire
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception("Identifiants incorrects");
            }
            else
            {
                // Gérer les erreurs (mauvais identifiants, serveur non disponible, etc.)
                throw new Exception("Échec de la connexion");
            }
        }
    }

    public async Task<HttpResponseMessage> GetUserDataAsync(string token)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://votreapi.com/user/data");

            return response;
        }
    }

}


