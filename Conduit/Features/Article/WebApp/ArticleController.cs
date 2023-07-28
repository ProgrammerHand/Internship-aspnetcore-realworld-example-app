using Conduit.Features.Article.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Conduit.Features.Article.Application.Queries;
using System.Drawing.Drawing2D;
using Conduit.Features.Article.Application.Dto;

namespace Conduit.Features.Article.WebApp
{

    [Route("api/articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleCreationHandler _createArticle;
        private readonly FeedArticlesHandler _feed;
        private readonly AddTagsHandler _addTags;
        private readonly DeleteTagsFromArticleHandler _deleteTags;

        public ArticleController(ArticleCreationHandler createServ, FeedArticlesHandler feedServ, AddTagsHandler addTagsServ, DeleteTagsFromArticleHandler deleteTags)
        {
            _createArticle = createServ;
            _feed = feedServ;
            _addTags = addTagsServ;
            _deleteTags = deleteTags;
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
        public async Task<IActionResult> UpdateArticleTags(TagsManipulationData data)
        {
            return Ok(await _addTags.AddTagsToArticle(data.names, data.title , Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }

        [HttpDelete("/tags/delete"), Authorize]
        public async Task<IActionResult> DeleteArticleTags(TagsManipulationData data)
        {
            return Ok(await _deleteTags.DeleteTagsFromArticle(data.names, data.title, Int32.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)));
        }
    }
}
