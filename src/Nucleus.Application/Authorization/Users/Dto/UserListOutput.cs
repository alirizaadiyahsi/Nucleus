using Nucleus.Application.Dto;
using Nucleus.Domain.Entities.Authorization;

namespace Nucleus.Application.Authorization.Users.Dto;

public class UserListOutput : CreationAuditedEntityDto
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public string ProfileImageUrl { get; set; }

    public User CreatorUser { get; set; }
}