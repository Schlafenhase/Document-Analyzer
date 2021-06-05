using DAApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DAApi.Services
{
    public class KeyCloakService : IKeyCloakService
    {
        private static readonly HttpClient client = new HttpClient();

        public KeyCloakService()
        {

        }

        public async Task<string> GetToken(string user, string password, string clientId, string tokenURL)
        {
            try
            {
                var values = new Dictionary<string, string>
                {
                    { "username", user },
                    { "password", password },
                    { "client_id", clientId },
                    { "grant_type", "password" }
                };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(tokenURL, content);

                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return "Error";
            }
        }
    }
}
