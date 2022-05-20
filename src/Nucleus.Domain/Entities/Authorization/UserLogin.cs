using Microsoft.AspNetCore.Identity;

namespace Nucleus.Domain.Entities.Authorization;

public class UserLogin : IdentityUserLogin<Guid>
{
    public virtual User User { get; set; }
}