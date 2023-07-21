using Conduit.Infrastructure.Security.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Conduit.Infrastructure.Security
{
    public class JWTtoken : IJWTtoken
    {
        private readonly IConfiguration _configuration;

        public JWTtoken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(string username, string role, string id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username), 
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Authentication:JwtKey").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
            var tmp = token.Claims;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public string getUsername(string token) 
        //{
        //    var validationParameters = GetValidationParameters();
        //    var readToken = new JwtSecurityTokenHandler().ValidateToken(token);
        //    return readToken.Claims;
        //}
    }
}
