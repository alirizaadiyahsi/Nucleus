using Nucleus.Application.Dto;

namespace Nucleus.Application.Users.Dto
{
    public class UserDto : EntityDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}