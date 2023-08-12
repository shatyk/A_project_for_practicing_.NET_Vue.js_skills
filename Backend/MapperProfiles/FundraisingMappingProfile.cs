using AutoMapper;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Database.Models;

namespace Backend.MapperProfiles
{
    public class FundraisingMappingProfile : Profile
    {
        public FundraisingMappingProfile()
        {
            CreateMap<Fundraising, FundraisingGetAllResponse>()
                .ForMember(fr => fr.ContentUaCapture, conf => conf
                    .MapFrom(f => f.Contents
                        .Select(c => c.Capture)
                            .FirstOrDefault()));

            CreateMap<Fundraising, FundraisingGetAllCapturesResponse>()
                .ForMember(fr => fr.ContentUaCapture, conf => conf
                    .MapFrom(f => f.Contents
                        .Select(c => c.Capture)
                            .FirstOrDefault()));

            CreateMap<Fundraising, FundraisingGetOneResponse>();
            CreateMap<FundraisingContent, FundraisingContentResponse>();

            CreateMap<FundraisingAddRequest, Fundraising>();
            CreateMap<FundraisingContentAddRequest, FundraisingContent>();

            CreateMap<FundraisingUpdateRequest, Fundraising>();
            CreateMap<FundraisingContentUpdateRequest, FundraisingContent>();
        }
    }
}

