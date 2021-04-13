using System.ComponentModel.DataAnnotations;

namespace DocumentAnalyzerAPI.MVC.Models.DTOs.Requests
{
    public class UserRegistrationDto
    {
        /// <summary>
        /// Atributes of UserRegistrationDto
        /// </summary>
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
