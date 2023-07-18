using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Conduit.Features.User.Domain
{
    public class User  //:IdentityUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Username { get; set; }

        public string Bio { get; set; } = string.Empty; 

        public string Image { get; set; } = string.Empty;
    }
}
