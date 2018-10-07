using System;
using System.Collections.Generic;
using System.Globalization;

namespace Nucleus.Core.Roles
{
    public class DefaultRoles
    {
        public static List<Role> All()
        {
            return new List<Role>
            {
                Admin,
                Member
            };
        }

        public static readonly Role Admin = new Role
        {
            Id = new Guid("F22BCE18-06EC-474A-B9AF-A9DE2A7B8263"),
            Name = RoleNameForAdmin,
            NormalizedName = RoleNameForAdmin.ToUpper(CultureInfo.InvariantCulture),
            IsSystemDefault = true
        };

        public static readonly Role Member = new Role
        {
            Id = new Guid("11D14A89-3A93-4D39-A94F-82B823F0D4CE"),
            Name = RoleNameForMember,
            NormalizedName = RoleNameForMember.ToUpper(CultureInfo.InvariantCulture),
            IsSystemDefault = true
        };

        public const string RoleNameForAdmin = "Admin";
        public const string RoleNameForMember = "Member";
    }
}
