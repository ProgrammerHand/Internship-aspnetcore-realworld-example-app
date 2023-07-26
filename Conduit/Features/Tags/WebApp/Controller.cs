using Conduit.Features.Tags.Application.Commands;
using Conduit.Features.Tags.Application.Dto;
using Conduit.Features.Tags.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Features.Tags.WebApp
{
    [Route("api/tags")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly Add _add;
        private readonly GetUnique _unique;

        public Controller(Add addServ, GetUnique uniqueServ)
        {
            _add = addServ;
            _unique = uniqueServ;
        }

        [HttpPut("add"), AllowAnonymous]
        public async Task<IActionResult> AddTags(AddTagsData data)
        {
            return Ok(await _add.AddTags(data.names, data.articleId));
        }

        [HttpGet(""), AllowAnonymous]
        public async Task<IActionResult> GetUniqueTags()
        {
            return Ok(await _unique.GetUniqueTags());
        }
    }
}
