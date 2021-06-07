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
        /// <summary>
        /// Atribute that saves the Http client
        /// </summary>
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Constructor of the key cloak service
        /// </summary>
        public KeyCloakService()
        {

        }

        /// <summary>
        /// Method that gets the bearer token from key cloak
        /// </summary>
        /// <param name="user">
        /// String that is trying to log in
        /// </param>
        /// <param name="password">
        /// String with the password of the user
        /// </param>
        /// <param name="clientId">
        /// String with the client id
        /// </param>
        /// <param name="tokenURL">
        /// String with the url
        /// </param>
        /// <returns>
        /// String with the token
        /// </returns>
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
