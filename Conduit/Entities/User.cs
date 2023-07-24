using Conduit.Features.User.Application.Dto;
using Microsoft.Extensions.Hosting;

namespace Conduit.Entities
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

        public ICollection<Article>? Articles { get; private set; } = new List<Article>();

        public static User CreateUser(string email, string username, byte[] passwordHash, byte[] passwordSalt) {
            var entity = new User(email, username, passwordHash, passwordSalt);
            entity.Role = SecretRolePromotion(entity.Username);
            return entity;
        }

        public void UpdateUser(UserUpdateData data, byte[] passwordHash, byte[] passwordSalt)
        {
            Email = data.email;
            Username = data.username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Bio = data.bio;
            Image = data.image;
        }

        private static string SecretRolePromotion(string username) {
            if (username.Contains("admin") || username.Contains("Admin"))
                return "Admin";
            else
                return "User";
        }
    }
}
