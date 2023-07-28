using Conduit.Features.User.Application.Commands;
using Conduit.Features.User.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Conduit.Features.User.WebApp
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RegistrationHandler _Registration;
        private readonly AuthenticationHandler _Authenticationn;
        private readonly GetAll _GetAll;
        private readonly UserUpdateHandler _Update;
        private readonly GetCurrent _GetCurrent;

        public UserController(RegistrationHandler regServ, AuthenticationHandler aunthServ, GetAll getAllServ, UserUpdateHandler updateServ, GetCurrent getCurrentServ)
        {
            _Registration = regServ;
            _Authenticationn = aunthServ;
            _GetAll = getAllServ;
            _Update = updateServ;
            _GetCurrent = getCurrentServ;

        }
        [HttpPost(""), AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UserRegistrationDataEnvelop data)
        {
            return Ok(await _Registration.Register(data.registrationData));
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser(UserAuthenticationDataEnvelop data)
        {
            var temp = await _Authenticationn.Authenticate(data.user);
            return Ok(temp);
        }

        [HttpGet("getAll"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _GetAll.GetAllUsers());
        }

        [HttpPut(""), Authorize]
        public async Task<IActionResult> UpdateUser(UserUpdateDataEnvelop data)
        {
            return Ok(await _Update.UpdateUser(data.updateData, Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value)));
        }

        [HttpGet(""), Authorize]
        public async Task<IActionResult> GetCurrent()
        {
            return Ok(await _GetCurrent.GetCurrentUser(Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value)));
        }
    }
}
