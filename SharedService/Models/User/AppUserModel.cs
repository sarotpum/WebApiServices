using Microsoft.AspNetCore.Identity;

namespace SharedService.Models.User
{
    public class AppUserModel : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
