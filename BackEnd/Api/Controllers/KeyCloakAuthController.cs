using DAApi.Configuration;
using DAApi.Interfaces;
using DAApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DAApi.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class KeyCloakAuthController : Controller
    {
        /// <summary>
        /// Atribute that handle the services and configurations
        /// </summary>
        private readonly IKeyCloakService _keyCloakService;
        private readonly KeyCloakConfig _keyCloakConfig;

        /// <summary>
        /// Constructor of KeyCloakAuthController
        /// </summary>
        /// <param name="keyCloakService">
        /// KeyCloak service interface
        /// </param>
        /// <param name="azureOptionsMonitor">
        /// Azure options
        /// </param>
        public KeyCloakAuthController(IKeyCloakService keyCloakService, IOptionsMonitor<KeyCloakConfig> azureOptionsMonitor)
        {
            _keyCloakService = keyCloakService;
            _keyCloakConfig = azureOptionsMonitor.CurrentValue;
        }

        /// <summary>
        /// Method that retrieves the bearer token for authentication
        /// </summary>
        /// <param name="user">
        /// User that is trying to log in
        /// </param>
        /// <returns>
        /// String with the bearer token
        /// </returns>
        [HttpPost]
        public Task<string> GetToken([FromBody] User user)
        {
            try
            {
                return _keyCloakService.GetToken(user.Name, user.Password, _keyCloakConfig.ClientId, _keyCloakConfig.TokenURL);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
