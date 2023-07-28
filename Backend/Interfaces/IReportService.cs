using Backend.Models.Requests;
using Backend.Models.Responses;

namespace Backend.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<ReportGetAllResponse>> GetAllAsync();
        Task<ReportGetOneResponse> GetAsync(long id);
        Task<long> AddAsync(ReportAddRequest request, CancellationToken cancellationToken);
        Task UpdateAsync(ReportUpdateRequest request, CancellationToken cancellationToken);
        Task DeleteAsync(long id, CancellationToken cancellationToken);
    }
}
