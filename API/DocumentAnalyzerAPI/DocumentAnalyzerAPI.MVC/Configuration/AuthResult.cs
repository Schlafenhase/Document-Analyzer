using System.Collections.Generic;

namespace DocumentAnalyzerAPI.MVC.Configuration
{
    public class AuthResult
    {
        /// <summary>
        /// Atributes of AuthResult
        /// </summary>
        public string Token { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
