using Nucleus.Application.Dto;

namespace Nucleus.Application.Authorization.Users.Dto;

public class UpdateUserInput : EntityDto
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Phone { get; set; }

    public string ProfileImageUrl { get; set; }

    public List<Guid> SelectedRoleIds { get; set; } = new List<Guid>();

    public List<string> SelectedPermissions { get; set; } = new List<string>();
}