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
            Name = AdminRoleName,
            NormalizedName = AdminRoleName.ToUpper(CultureInfo.InvariantCulture)
        };

        public static readonly Role Member = new Role
        {
            Id = new Guid("11D14A89-3A93-4D39-A94F-82B823F0D4CE"),
            Name = MemberRoleName,
            NormalizedName = MemberRoleName.ToUpper(CultureInfo.InvariantCulture)
        };

        private const string AdminRoleName = "Admin";
        private const string MemberRoleName = "Member";
    }
}
