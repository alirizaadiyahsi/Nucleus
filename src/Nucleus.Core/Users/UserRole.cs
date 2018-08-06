using System;
using Microsoft.AspNetCore.Identity;
using Nucleus.Core.Roles;

namespace Nucleus.Core.Users
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
