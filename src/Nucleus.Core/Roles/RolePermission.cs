using System;
using Nucleus.Core.Permissions;

namespace Nucleus.Core.Roles
{
    public class RolePermission
    {
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }

        public Guid PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
    }
}