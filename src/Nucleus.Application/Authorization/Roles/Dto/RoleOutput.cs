using Nucleus.Application.Dto;

namespace Nucleus.Application.Authorization.Roles.Dto;

public class RoleOutput : EntityDto
{
    public string Name { get; set; }

    public IEnumerable<string> SelectedPermissions { get; set; }

    public IEnumerable<string> AllPermissions { get; set; }
}