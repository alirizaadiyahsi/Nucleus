using Microsoft.AspNetCore.Identity;

namespace Nucleus.Domain.Entities.Authorization;

public class UserClaim : IdentityUserClaim<Guid>
{
    public virtual User User { get; set; }
}