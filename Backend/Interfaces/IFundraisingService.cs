using Backend.Models.Requests;
using Backend.Models.Responses;

namespace Backend.Interfaces
{
    public interface IFundraisingService
    {
        Task<IEnumerable<FundraisingGetAllResponse>> GetAllAsync();
        Task<FundraisingGetOneResponse> GetAsync(long id);
        Task<long> AddAsync(FundraisingAddRequest request, CancellationToken cancellationToken);
        Task UpdateAsync(FundraisingUpdateRequest request, CancellationToken cancellationToken);
        Task DeleteAsync(long id, CancellationToken cancellationToken);
    }
}
