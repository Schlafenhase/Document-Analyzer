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
        private readonly IKeyCloakService _keyCloakService;
        private readonly KeyCloakConfig _keyCloakConfig;

        public KeyCloakAuthController(IKeyCloakService keyCloakService, IOptionsMonitor<KeyCloakConfig> azureOptionsMonitor)
        {
            _keyCloakService = keyCloakService;
            _keyCloakConfig = azureOptionsMonitor.CurrentValue;
        }

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
