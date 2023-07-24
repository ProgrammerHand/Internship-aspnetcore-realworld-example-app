using Conduit.Features.Article.Application.Commands;
using Conduit.Features.Article.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Conduit.Features.Article.Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.Article.WebApp
{
    
    [Route("api/articles")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly Create _Create;
        private readonly Feed _Feed;

        public Controller(Create createServ, Feed feedServ)
        {
            _Create = createServ;
            _Feed = feedServ;
        }

        [HttpPost(""), AllowAnonymous]
        public async Task<IActionResult> CreateArticle([FromBody]ArticleCreationEnvelop data)
        {
            var temp = data; 
            var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(await _Create.CreateArticle(data.article, Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }

        [HttpGet("feed"), AllowAnonymous]
        public async Task<IActionResult> FeedArticles(int limit, int offset)
        {
            return Ok(await _Feed.FeedArticles(Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value), limit, offset));
        }
    }
}
