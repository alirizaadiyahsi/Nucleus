using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Nucleus.Core.Users;

namespace Nucleus.Core.Roles
{
    public class Role : IdentityRole<Guid>
    {
        public bool IsSystemDefault { get; set; } = false;

        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
