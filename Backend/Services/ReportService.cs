using AutoMapper;
using Backend.Constants;
using Backend.Interfaces;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Database;
using Microsoft.AspNetCore.Components;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly LanguageCacheService _languageCacheService;

        public ReportService(AppDbContext appDbContext, IMapper mapper, LanguageCacheService languageCacheService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _languageCacheService = languageCacheService;
        }

        public async Task<IEnumerable<ReportGetAllResponse>> GetAllAsync()
        {
            int uaLanguageId = await _languageCacheService.GetLanguageIdAsync(LanguageConstants.LanguageNameUA);
            IEnumerable<Report> reports = _appDbContext.Reports.AsNoTracking()
                .Include(r => r.Contents
                    .Where(c => c.LanguageId == uaLanguageId))
                .Include(r => r.Fundraising)
                .ThenInclude(f => f.Contents
                    .Where(c => c.LanguageId == uaLanguageId));

        }

        public async Task<ReportGetOneResponse> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<long> AddAsync(ReportAddRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ReportUpdateRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
