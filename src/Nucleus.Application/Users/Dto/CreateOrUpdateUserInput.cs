using System;
using System.Collections.Generic;

namespace Nucleus.Application.Users.Dto
{
    public class CreateOrUpdateUserInput
    {
        public UserDto User { get; set; } = new UserDto();

        public List<Guid> GrantedRoleIds { get; set; } = new List<Guid>();
    }
}
