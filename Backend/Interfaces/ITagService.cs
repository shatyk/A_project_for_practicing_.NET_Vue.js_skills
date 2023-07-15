using Common.Models;
using Backend.Models.Requests;
using Backend.Models.Responses;

namespace Backend.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagResponse>> GetAllAsync();
        Task<int> AddAsync(CreateTagRequest request, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateTagRequest request, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
