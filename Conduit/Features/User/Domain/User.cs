using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Duende.IdentityServer.Models.IdentityResources;
using System.Data;

namespace Conduit.Features.User.Domain
{
    public class User  //:IdentityUser
    {
        private User(string email, string username, byte[] passwordHash, byte[] passwordSalt)
        {
            this.Email = email;
            this.PasswordHash = passwordHash;
            this.PasswordSalt = passwordSalt;
            this.Username = username;
        }

        public User()
        {
        }

        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }

        public byte[] PasswordHash { get; private set; }

        public byte[] PasswordSalt { get; private set; }

        public string Username { get; private set; }

        public string Bio { get; private set; } = string.Empty; 

        public string Image { get; private set; } = string.Empty;

        public static async Task<User> CreateUser(string email, string username, byte[] passwordHash, byte[] passwordSalt) {
            var entity = new User(email, username, passwordHash, passwordSalt);
            entity.Role = await SecretRolePromotion(entity.Username);
            return entity;
        }

        private static async Task<string> SecretRolePromotion(string username) {
            if (username.Contains("admin") || username.Contains("Admin"))
                return "Admin";
            else
                return "User";
        }
    }
}
