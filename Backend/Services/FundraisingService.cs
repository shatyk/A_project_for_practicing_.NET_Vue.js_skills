using AutoMapper;
using Backend.Constants;
using Backend.Interfaces;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Backend.Services
{
    public class FundraisingService : IFundraisingService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILanguageCacheService _languageCacheService;

        public FundraisingService(AppDbContext appDbContext, IMapper mapper, ILanguageCacheService languageCacheService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _languageCacheService = languageCacheService;
        }

        public async Task<IEnumerable<FundraisingGetAllResponse>> GetAllAsync()
        {
            int uaLanguageId = await _languageCacheService.GetLanguageIdAsync(LanguageConstants.LanguageNameUA);
            IEnumerable<Fundraising> fundraisings = _appDbContext.Fundraisings.AsNoTracking()
                .Include(f => f.Contents
                    .Where(c => c.LanguageId == uaLanguageId));

            return await Task.FromResult(fundraisings.Select(_mapper.Map<FundraisingGetAllResponse>));
        }

        public async Task<IEnumerable<FundraisingGetAllCapturesResponse>> GetAllCapturesAsync()
        {
            int uaLanguageId = await _languageCacheService.GetLanguageIdAsync(LanguageConstants.LanguageNameUA);
            IEnumerable<Fundraising> fundraisings = _appDbContext.Fundraisings.AsNoTracking()
                .Include(f => f.Contents
                    .Where(c => c.LanguageId == uaLanguageId));

            return await Task.FromResult(fundraisings.Select(_mapper.Map<FundraisingGetAllCapturesResponse>));
        }

        public async Task<FundraisingGetOneResponse> GetAsync(long id)
        {
            Fundraising? fundraising = _appDbContext.Fundraisings.AsNoTracking()
                .Include(f => f.Contents)
                .FirstOrDefault(f => f.Id == id);

            return await Task.FromResult(_mapper.Map<FundraisingGetOneResponse>(fundraising));
        }

        public async Task<long> AddAsync(FundraisingAddRequest request, CancellationToken cancellationToken)
        {
            Fundraising fundraising = _mapper.Map<Fundraising>(request);
            fundraising.CreatedAt = DateTime.UtcNow;
            int languagesCount = await _languageCacheService.GetLanguagesCountAsync();
            if (fundraising.Contents.Count() != languagesCount)
            {
                throw new HttpRequestException($"Are you DURBECEL? We have {languagesCount} languages, send me Contents for each language!", null, HttpStatusCode.BadRequest);
            }
            await _appDbContext.Fundraisings.AddAsync(fundraising, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return fundraising.Id;
        }

        public async Task UpdateAsync(FundraisingUpdateRequest request, CancellationToken cancellationToken)
        {
            Fundraising fundraising = _mapper.Map<Fundraising>(request);
            int languagesCount = await _languageCacheService.GetLanguagesCountAsync();
            if (fundraising.Contents.Count() != languagesCount)
            {
                throw new HttpRequestException($"Are you DURBECEL? We have {languagesCount} languages, send me Contents for each language!", null, HttpStatusCode.BadRequest);
            }
            _appDbContext.Fundraisings.Attach(fundraising);
            _appDbContext.Entry(fundraising).Property(f => f.ActivityStatus).IsModified = true;
            _appDbContext.Entry(fundraising).Property(f => f.VisabilityStatus).IsModified = true;
            foreach (FundraisingContent content in fundraising.Contents)
            {
                _appDbContext.Entry(content).Property(c => c.Capture).IsModified = true;
                _appDbContext.Entry(content).Property(c => c.Text).IsModified = true;
            }
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            Fundraising? fundraising = await _appDbContext.Fundraisings.AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

            if (fundraising is null)
            {
                throw new HttpRequestException("Can't find fundraising! How do you think I should delete a non-existent object? Сompletely crazy man.", null, HttpStatusCode.BadRequest);
            }

            _appDbContext.Fundraisings.Remove(fundraising);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
