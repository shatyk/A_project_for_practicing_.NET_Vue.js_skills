using Common.Models;
using Backend.Models.Requests;

namespace Backend.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<int> AddAsync(CreateTagRequest request, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateTagRequest request, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
