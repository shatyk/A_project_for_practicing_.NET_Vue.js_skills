
using AutoMapper;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Database.Models;

namespace Backend.MapperProfiles
{
    public class ReportMappingProfile : Profile
    {
        public ReportMappingProfile()
        {
            CreateMap<Report, ReportGetAllResponse>()
                .ForMember(rr => rr.ContentUaCaption, conf => conf
                    .MapFrom(r => r.Contents
                        .Select(c => c.Caption)
                            .FirstOrDefault()))
                .ForMember(rr => rr.FundraisingContentUaCaption, conf => conf
                    .MapFrom(r => r.Fundraising!.Contents
                        .Select(c => c.Caption)
                            .FirstOrDefault()));

            CreateMap<Report, ReportGetOneResponse>()
                .ForMember(rr => rr.TagsId, conf => conf
                    .MapFrom(r => r.ReportTags
                        .Select(rt => rt.TagId)));
            CreateMap<ReportContent, ReportContentResponse>();

            CreateMap<ReportAddRequest, Report>()
                .ForMember(r => r.ReportTags, conf => conf
                    .MapFrom(rr => CreateReportTags(rr.TagsId)));
            CreateMap<ReportContentAddRequest, ReportContent>();

            CreateMap<ReportUpdateRequest, Report>()
                .ForMember(r => r.ReportTags, conf => conf
                    .MapFrom(rr => CreateReportTags(rr.TagsId, rr.Id)));
            CreateMap<ReportContentUpdateRequest, ReportContent>();
        }

        private static IEnumerable<ReportTag> CreateReportTags(IEnumerable<int> tagsId, long reportId)
        {
            IList<ReportTag> reportTags = new List<ReportTag>();
            
            foreach (int id in tagsId)
            {
                reportTags.Add(new ReportTag()
                {
                    TagId = id,
                    ReportId = reportId
                });
            }

            return reportTags;
        }

        private static IEnumerable<ReportTag> CreateReportTags(IEnumerable<int> tagsId)
        {
            IList<ReportTag> reportTags = new List<ReportTag>();

            foreach (int id in tagsId)
            {
                reportTags.Add(new ReportTag()
                {
                    TagId = id
                });
            }

            return reportTags;
        }
    }
}
