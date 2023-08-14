using Backend.Interfaces;
using Backend.Models;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<PagedList<ReportGetAllResponse>> GetAllAsync(
            [FromQuery] string? searchCaption,
            [FromQuery] long? searchFundraisingId,
            [FromQuery] string? sortDateOrder,
            [FromQuery] int page,
            [FromQuery] int pageSize)
        {
            return await _reportService.GetAllAsync(searchCaption, searchFundraisingId, sortDateOrder, page, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<ReportGetOneResponse> GetAsync([FromRoute] long id)
        {
            return await _reportService.GetAsync(id);
        }

        [HttpPost]
        public async Task<long> AddAsync([FromBody] ReportAddRequest request, CancellationToken cancellationToken)
        {
            return await _reportService.AddAsync(request, cancellationToken);
        }

        [HttpPatch]
        public async Task UpdateAsync([FromBody] ReportUpdateRequest request, CancellationToken cancellationToken)
        {
            await _reportService.UpdateAsync(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromRoute] long id, CancellationToken cancellationToken)
        {
            await _reportService.DeleteAsync(id, cancellationToken);
        }
    }
}
