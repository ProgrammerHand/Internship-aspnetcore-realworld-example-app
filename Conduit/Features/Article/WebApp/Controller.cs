using Conduit.Features.Article.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Conduit.Features.Article.Application.Queries;
using System.Drawing.Drawing2D;

namespace Conduit.Features.Article.WebApp
{

    [Route("api/articles")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly ArticleCreationHandler _createArticle;
        private readonly Feed _feed;
        private readonly AddTags _addTags;

        public Controller(ArticleCreationHandler createServ, Feed feedServ, AddTags addTagsServ)
        {
            _createArticle = createServ;
            _feed = feedServ;
            _addTags = addTagsServ;
        }

        [HttpPost(""), AllowAnonymous]
        public async Task<IActionResult> CreateArticle([FromBody]ArticleCreationEnvelop data)
        {
            return Ok(await _createArticle.CreateArticle(data.article, Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }

        [HttpGet("feed"), AllowAnonymous]
        public async Task<IActionResult> FeedArticles(int limit, int offset)
        {
            return Ok(await _feed.FeedArticles(Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value), limit, offset));
        }

        [HttpPost("/tags/add"), Authorize]
        public async Task<IActionResult> UpdateArticleTags(AddTagsData data)
        {
            return Ok(await _addTags.AddTagsToArticle(data.names, data.title , Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }
    }
}
