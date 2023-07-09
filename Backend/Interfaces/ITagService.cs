using Common.Models;
using Backend.Models.Requests;

namespace Backend.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Guid> AddAsync(CreateTagRequest request, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateTagRequest request, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
