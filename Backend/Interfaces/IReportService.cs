using Backend.Models;
using Backend.Models.Requests;
using Backend.Models.Responses;

namespace Backend.Interfaces
{
    public interface IReportService
    {
        Task<PagedList<ReportGetAllResponse>> GetAllAsync(
            string? searchCaptionTerm,
            long? searchFundraisingIdTerm,
            string? sortDateOrder,
            int page,
            int pageSize);
        Task<ReportGetOneResponse> GetAsync(long id);
        Task<long> AddAsync(ReportAddRequest request, CancellationToken cancellationToken);
        Task UpdateAsync(ReportUpdateRequest request, CancellationToken cancellationToken);
        Task DeleteAsync(long id, CancellationToken cancellationToken);
    }
}
