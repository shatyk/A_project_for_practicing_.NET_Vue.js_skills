using AutoMapper;
using Backend.Models.Responses;
using Backend.Models.Requests;
using Database.Models;

namespace Backend.MapperProfiles
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            CreateMap<Tag, TagResponse>();
            CreateMap<CreateTagRequest, Tag>();
            CreateMap<UpdateTagRequest, Tag>();
        }
    }
}
