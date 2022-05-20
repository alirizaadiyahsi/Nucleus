using Microsoft.AspNetCore.Identity;

namespace Nucleus.Domain.Entities.Authorization;

public class UserRole : IdentityUserRole<Guid>
{
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
}