using System.ComponentModel.DataAnnotations;

namespace SharedService.Models.User
{
    public class LoginBindingModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
