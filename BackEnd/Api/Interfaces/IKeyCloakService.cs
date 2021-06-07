using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Interfaces
{
    public interface IKeyCloakService
    {
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
        Task<string> GetToken(string user, string password, string clientId, string tokenURL);
    }
}
