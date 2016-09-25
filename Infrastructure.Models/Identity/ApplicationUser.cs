
using Infrastructure.Models.Attributes;

namespace Infrastructure.Models.Identity
{
    [Table(Name = "User")]
    public class ApplicationUser : Entity
    {
        public string Email { get; set; }
        public long FailedLoginAttemptsCount { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public long RoleId { get; set; }
        public string UserName { get; set; }
    }
}
