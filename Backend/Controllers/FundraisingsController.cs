using Backend.Constants;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Database.Models;
using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Backend.Interfaces;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/fundraisings")]
    [ApiController]
    public class FundraisingsController : ControllerBase
    {
        private readonly IFundraisingService _fundraisingService;

        public FundraisingsController(IFundraisingService fundraisingService)
        {
            _fundraisingService = fundraisingService;
        }

        [HttpGet]
        public async Task<PagedList<FundraisingGetAllResponse>> GetAllAsync(
            [FromQuery] string? searchCaptionTerm,
            [FromQuery] string? sortDateOrder,
            [FromQuery] int page,
            [FromQuery] int pageSize)
        {
            return await _fundraisingService.GetAllAsync(searchCaptionTerm, sortDateOrder, page, pageSize);
        }

        [HttpGet("captions")]
        public async Task<IEnumerable<FundraisingGetAllCaptionsResponse>> GetAllCaptionsAsync()
        {
            return await _fundraisingService.GetAllCaptionssAsync();
        }

        [HttpGet("{id}")]
        public async Task<FundraisingGetOneResponse> GetAsync([FromRoute] long id)
        {
            return await _fundraisingService.GetAsync(id);
        }

        [HttpPost]
        public async Task<long> AddAsync([FromBody] FundraisingAddRequest request, CancellationToken cancellationToken)
        {
            return await _fundraisingService.AddAsync(request, cancellationToken);
        }

        [HttpPatch]
        public async Task UpdateAsync([FromBody] FundraisingUpdateRequest request, CancellationToken cancellationToken)
        {
            await _fundraisingService.UpdateAsync(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromRoute] long id, CancellationToken cancellationToken)
        {
            await _fundraisingService.DeleteAsync(id, cancellationToken);
        }
    }
}
