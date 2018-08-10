using Microsoft.AspNetCore.Authorization;
using Nucleus.Core.Permissions;

namespace Nucleus.Web.Core.Authentication
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(Permission permission)
        {
            Permission = permission;
        }

        public Permission Permission { get; }
    }
}
