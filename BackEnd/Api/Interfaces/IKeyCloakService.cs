using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Interfaces
{
    public interface IKeyCloakService
    {
        Task<string> GetToken(string user, string password, string clientId, string tokenURL);
    }
}
