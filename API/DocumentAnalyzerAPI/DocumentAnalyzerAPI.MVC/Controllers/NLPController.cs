using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentAnalyzerAPI.A.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentAnalyzerAPI.MVC.Controllers
{
    [Route("Api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NLPController : Controller
    {
        private INLPService _nlpService;

        public NLPController(INLPService nlpService)
        {
            _nlpService = nlpService;
        }

        [HttpPost]
        public void SearchEmployees()
        {
            _nlpService.SearchEmployees();
            return;
        }
    }
}
