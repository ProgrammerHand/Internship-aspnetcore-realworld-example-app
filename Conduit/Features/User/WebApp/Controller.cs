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
        private readonly GetAll _GetAllUsers;



        public Controller(Registration regServ, Authentication aunthServ, GetAll getAllUsers )
        {
            _Registration = regServ;
            _Authenticationn = aunthServ;
            _GetAllUsers = getAllUsers;

        }
        [HttpPost(""), Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterUser(RootUserRegistration data)
        {
            return Ok(await _Registration.Register(data.user));
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> LoginUser(RootUserAunthenication data)
        {
            return Ok(await _Authenticationn.Authenticate(data.user));
        }

        [HttpGet("getAll"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> getAll()
        {
            return Ok(await _GetAllUsers.GetAllUsers());
        }

    }
}
