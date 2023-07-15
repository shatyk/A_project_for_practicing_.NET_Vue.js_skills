using Backend.Interfaces;
using Backend.Models.Requests;
using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Backend.Models.Responses;

namespace Backend.Controllers
{
    //[Authorize]
    [Route("api/tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService) => _tagService = tagService;

        [HttpGet]
        public async Task<IEnumerable<TagResponse>> GetAllAsync()
        {
            return await _tagService.GetAllAsync();
        }

        [HttpPost]
        public async Task<int> AddAsync([FromBody] CreateTagRequest request, CancellationToken cancellationToken)
        {
            return await _tagService.AddAsync(request, cancellationToken);
        }

        [HttpPatch]
        public async Task UpdateAsync([FromBody] UpdateTagRequest request, CancellationToken cancellationToken)
        {
            await _tagService.UpdateAsync(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _tagService.DeleteAsync(id, cancellationToken);
        }
    }
}
