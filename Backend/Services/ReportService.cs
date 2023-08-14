using AutoMapper;
using Backend.Constants;
using Backend.Interfaces;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Database;
using Microsoft.AspNetCore.Components;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Backend.Models;

namespace Backend.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILanguageCacheService _languageCacheService;

        public ReportService(AppDbContext appDbContext, IMapper mapper, ILanguageCacheService languageCacheService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _languageCacheService = languageCacheService;
        }

        public async Task<PagedList<ReportGetAllResponse>> GetAllAsync(
            string? searchCaptionTerm,
            long? searchFundraisingIdTerm,
            string? sortDateOrder,
            int page,
            int pageSize)
        {
            int uaLanguageId = await _languageCacheService.GetLanguageIdAsync(LanguageConstants.LanguageNameUA);

            IQueryable<Report> reports = _appDbContext.Reports.AsNoTracking()
                .Include(r => r.Contents
                    .Where(c => c.LanguageId == uaLanguageId))
                .Include(r => r.Fundraising!)
                .ThenInclude(f => f.Contents
                    .Where(c => c.LanguageId == uaLanguageId));

            if (searchFundraisingIdTerm is not null)
            {
                reports = reports.Where(f => f.FundraisingId == searchFundraisingIdTerm);
            }
            
            if (!string.IsNullOrWhiteSpace(searchCaptionTerm))
            {
                reports = reports.Where(f => f.Contents.FirstOrDefault()!
                    .Caption.ToLower().Contains(searchCaptionTerm.ToLower()));
            }

            if (sortDateOrder?.ToLower() == "desc")
            {
                reports = reports.OrderByDescending(f => f.CreatedAt);
            }
            else
            {
                reports = reports.OrderBy(f => f.CreatedAt);
            }

            PagedList<ReportGetAllResponse> pagedListResult = await PagedList<ReportGetAllResponse>
                .CreateAsync(reports.Select(f => _mapper.Map<ReportGetAllResponse>(f)), page, pageSize);

            return await Task.FromResult(pagedListResult);
        }

        public async Task<ReportGetOneResponse> GetAsync(long id)
        {
            Report? report = _appDbContext.Reports.AsNoTracking()
                .Where(r => r.Id == id)
                .Include(r => r.Contents)
                .Include(r => r.Fundraising!)
                .Include(r => r.ReportTags)
                .FirstOrDefault();

            return await Task.FromResult(_mapper.Map<ReportGetOneResponse>(report));
        }

        public async Task<long> AddAsync(ReportAddRequest request, CancellationToken cancellationToken)
        {
            Report report = _mapper.Map<Report>(request);
            report.CreatedAt = DateTime.UtcNow;
            int languagesCount = await _languageCacheService.GetLanguagesCountAsync();
            if (report.Contents.Count() != languagesCount)
            {
                throw new HttpRequestException($"Are you DURBECEL? We have {languagesCount} languages, send me Contents for each language!", null, HttpStatusCode.BadRequest);
            }
            await _appDbContext.AddAsync(report, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);           
            return report.Id;
        }

        public async Task UpdateAsync(ReportUpdateRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<ReportTag> reportTagsExisting = _appDbContext.ReportTags
                .Where(r => r.ReportId == request.Id);

            Report report = _mapper.Map<Report>(request);
            int languagesCount = await _languageCacheService.GetLanguagesCountAsync();
            if (report.Contents.Count() != languagesCount)
            {
                throw new HttpRequestException($"Are you DURBECEL? We have {languagesCount} languages, send me Contents for each language!", null, HttpStatusCode.BadRequest);
            }
     
            foreach (ReportTag reportTagExisting in reportTagsExisting)
            {
                if (!report.ReportTags.Contains(reportTagExisting))
                {
                    _appDbContext.ReportTags.Remove(reportTagExisting);
                }
            }
            foreach (ReportTag reportTagRequest in report.ReportTags)
            {
                if (!reportTagsExisting.Contains(reportTagRequest))
                {
                    await _appDbContext.ReportTags.AddAsync(reportTagRequest, cancellationToken);
                }
            }

            _appDbContext.Reports.Attach(report);

            _appDbContext.Entry(report).Property(r => r.VisabilityStatus).IsModified = true;
            _appDbContext.Entry(report).Property(r => r.FundraisingId).IsModified = true;
            foreach (ReportContent content in report.Contents)
            {
                _appDbContext.Entry(content).Property(c => c.Caption).IsModified = true;
                _appDbContext.Entry(content).Property(c => c.Text).IsModified = true;
            }

            await _appDbContext.SaveChangesAsync(cancellationToken);            
        }

        public async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            Report? report = await _appDbContext.Reports.AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

            if (report is null)
            {
                throw new HttpRequestException("Can't find report! How do you think I should delete a non-existent object? Сompletely crazy man.", null, HttpStatusCode.BadRequest);
            }

            _appDbContext.Reports.Remove(report);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
