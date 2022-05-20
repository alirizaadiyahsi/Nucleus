using Microsoft.AspNetCore.Identity;

namespace Nucleus.Domain.Entities.Authorization;

public class RoleClaim : IdentityRoleClaim<Guid>
{
    public virtual Role Role { get; set; }
}