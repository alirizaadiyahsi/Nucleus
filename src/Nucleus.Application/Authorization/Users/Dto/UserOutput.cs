using Nucleus.Application.Authorization.Roles.Dto;
using Nucleus.Application.Dto;

namespace Nucleus.Application.Authorization.Users.Dto;

public class UserOutput : EntityDto
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public string ProfileImageUrl { get; set; }

    public IEnumerable<Guid> SelectedRoleIds { get; set; }

    public IEnumerable<string> SelectedPermissions { get; set; }

    public IEnumerable<RoleListOutput> AllRoles { get; set; }

    public IEnumerable<string> AllPermissions { get; set; }
}