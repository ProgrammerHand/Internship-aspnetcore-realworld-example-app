using Conduit.Features.User.Application;
using Conduit.Features.User.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Conduit.Features.User.UI
{
    [ApiController]
    [Route("users")]
    public class Controller : ControllerBase
    {
        private readonly Registration _Registration;
        private readonly Authentication _Authenticationn;

        public Controller(Registration regServ, Authentication aunthServ)
        {
            _Registration = regServ;
            _Authenticationn = aunthServ;

        }
        [HttpPost("")]
        public async Task<IActionResult> RegisterUser(UserRegistration data)
        {
            return Ok(await _Registration.Register(data));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] object User)
        {
            //var temp = JsonSerializer.Deserialize<UserAunthenicationRoot>(data);
            return Ok(_Authenticationn.Authenticate(User));
        }
    }
}
