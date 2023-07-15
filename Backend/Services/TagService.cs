using AutoMapper;
using Backend.Interfaces;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Common.Models;
using Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Backend.Services
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TagService(AppDbContext appDbContext, IMapper mapper) =>
            (_appDbContext, _mapper) = (appDbContext, mapper);

        public async Task<IEnumerable<TagResponse>> GetAllAsync()
        {
            IEnumerable<Database.Models.Tag> tags = _appDbContext.Tags.AsNoTracking().AsEnumerable();

            return await Task.FromResult(tags.Select(_mapper.Map<TagResponse>));
        }

        public async Task<int> AddAsync(CreateTagRequest request, CancellationToken cancellationToken)
        {
            Database.Models.Tag tag = _mapper.Map<Database.Models.Tag>(request);
            await _appDbContext.Tags.AddAsync(tag, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return tag.Id;
        }

        public async Task UpdateAsync(UpdateTagRequest request, CancellationToken cancellationToken)
        {
            Database.Models.Tag tag = _mapper.Map<Database.Models.Tag>(request);
            _appDbContext.Tags.Attach(tag);
            _appDbContext.Entry(tag).Property(t => t.Text).IsModified = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Database.Models.Tag? tag = await _appDbContext.Tags
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            
            if (tag is null)
            {
                throw new HttpRequestException("Cannot find tag, you got it wrong buddy!", null, HttpStatusCode.NotFound);
            }

            _appDbContext.Tags.Remove(tag);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }           
    }
}
