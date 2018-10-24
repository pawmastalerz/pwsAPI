using AutoMapper;
using pwsAPI.Dtos;
using pwsAPI.Models;

namespace pwsAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Thought, ThoughtForUpdateDto>();
            CreateMap<ThoughtForUpdateDto, Thought>();
            
            CreateMap<Poster, PosterForUpdateDto>();
            CreateMap<PosterForUpdateDto, Poster>();
        }
    }
}