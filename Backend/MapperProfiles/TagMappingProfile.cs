using AutoMapper;
using Backend.Models.Requests;
using Database.Models;

namespace Backend.MapperProfiles
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            CreateMap<Tag, Common.Models.Tag>();
            CreateMap<CreateTagRequest, Tag>();
            CreateMap<UpdateTagRequest, Tag>();
        }
    }
}
