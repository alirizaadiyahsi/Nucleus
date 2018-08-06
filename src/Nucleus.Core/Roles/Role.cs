using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Nucleus.Core.Users;

namespace Nucleus.Core.Roles
{
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
