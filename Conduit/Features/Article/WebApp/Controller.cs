using Conduit.Features.Article.Application.Commands;
using Conduit.Features.Article.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Conduit.Features.Article.WebApp
{
    [ApiController]
    [Route("api/articles")]
    public class Controller : ControllerBase
    {
        private readonly Create _Create;

        public Controller(Create createServ)
        {
            _Create = createServ;
        }

        [HttpPost(""), AllowAnonymous]
        public async Task<IActionResult> CreateArticle(ArticleCreation data)
        {
            return Ok(await _Create.CreateArticle(data, Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value)));
        }
    }
}
