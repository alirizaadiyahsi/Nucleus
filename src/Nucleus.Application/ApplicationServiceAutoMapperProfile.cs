using AutoMapper;
using Nucleus.Application.Users.Dto;
using Nucleus.Core.Users;

namespace Nucleus.Application
{
    public class ApplicationServiceAutoMapperProfile : Profile
    {
        public ApplicationServiceAutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(u => u.Password, opt => opt.Ignore());
        }
    }
}