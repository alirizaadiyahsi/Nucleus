using Microsoft.AspNetCore.Identity;

namespace Nucleus.Domain.Entities.Authorization;

public class UserToken : IdentityUserToken<Guid>
{
    public virtual User User { get; set; }
}