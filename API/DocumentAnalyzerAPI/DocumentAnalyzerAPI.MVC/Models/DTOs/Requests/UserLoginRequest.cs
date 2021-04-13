using System.ComponentModel.DataAnnotations;

namespace DocumentAnalyzerAPI.MVC.Models.DTOs.Requests
{
    public class UserLoginRequest
    {
        /// <summary>
        /// Atributes of UserLoginRequest
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
