
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
                .ForMember(rr => rr.ContentUaCapture, conf => conf
                    .MapFrom(r => r.Contents
                        .Select(c => c.Capture)
                            .FirstOrDefault()))
                .ForMember(rr => rr.FundraisingContentUaCapture, conf => conf
                    .MapFrom(r => r.Fundraising!.Contents
                        .Select(c => c.Capture)
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
        }

        private static IEnumerable<ReportTag> CreateReportTags(IEnumerable<int> ids)
        {
            IList<ReportTag> reportTags = new List<ReportTag>();
            
            foreach (int id in ids)
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
