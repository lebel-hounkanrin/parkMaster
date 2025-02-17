using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parkMasterD.utils;
static class Helper{
    public static  void SaveToken(string token)
    {
        using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly())
        {
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("auth_token.txt", System.IO.FileMode.Create, isolatedStorage))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(token);
                }
            }
        }
    }

    public static string LoadToken()
    {
        using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly())
        {
            if (isolatedStorage.FileExists("auth_token.txt"))
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("auth_token.txt", System.IO.FileMode.Open, isolatedStorage))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            return null; // Aucun token trouvé
        }
    }

    public static bool IsUserConnected()
    {
        string token = LoadToken();

        if (token != null)
        {
            // Vérifiez si le token est valide (par exemple, en vérifiant sa date d'expiration ou en envoyant une requête à l'API)
            if (IsTokenValid(token))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsTokenValid(string token)
    {
        // Vous pouvez valider le token en analysant sa structure, ou appeler une API pour vérifier sa validité.
        // Par exemple, décoder le JWT et vérifier la date d'expiration
        try
        {
            var payload = DecodeJwt(token);
            var expiration = payload["exp"].ToString();
            var expirationDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiration)).DateTime;

            return expirationDate > DateTime.Now;
        }
        catch
        {
            return false; // Le token est invalide
        }
    }

    public static Dictionary<string, string> DecodeJwt(string token)
{
    var parts = token.Split('.');
    var payload = parts[1];

    // Décoder le payload du JWT (base64Url -> base64 -> json)
    var jsonBytes = Convert.FromBase64String(payload + "==");
    var json = Encoding.UTF8.GetString(jsonBytes);

     return new Dictionary<string, string>();
    //return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
}
    public static bool IsUserLoggedIn()
    {
        return Properties.Settings.Default.IsUserLoggedIn;
    }
}

