using Conduit.Features.User.Application;
using Conduit.Features.User.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Features.User.UI
{
    [ApiController]
    [Route("users")]
    public class Controller : ControllerBase
    {
        private readonly Registration _Registration;
        private readonly Authentication _Authenticationn;
        private readonly GetAll _GetAll;
        private readonly Update _Update;

        public Controller(Registration regServ, Authentication aunthServ, GetAll getAllServ, Update updateServ )
        {
            _Registration = regServ;
            _Authenticationn = aunthServ;
            _GetAll = getAllServ;
            _Update = updateServ;

        }
        [HttpPost(""), AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UserRegistrationDataEnvelop data)
        {
            return Ok(await _Registration.Register(data.registrationData));
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser(UserAuthenticationDataEnvelop data)
        {
            return Ok(await _Authenticationn.Authenticate(data.authenticationData));
        }

        [HttpGet("getAll"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _GetAll.GetAllUsers());
        }

        [HttpPut(""), Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateUser(UserUpdateDataEnvelop data)
        {
            return Ok(await _Update.UpdateUser(data.updateData));
        }

        [HttpGet(""), Authorize(Roles = "User")]
        public async Task<IActionResult> GetCurrent()
        {
            this.HttpContext.User.Claims.Single(x=> x.Type.Equals("sub"));
            return Ok();
        }
    }
}
