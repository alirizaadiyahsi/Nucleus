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
                AdminRole,
                MemberRole
            };
        }

        public static readonly Role AdminRole = new Role
        {
            Id = new Guid("F22BCE18-06EC-474A-B9AF-A9DE2A7B8263"),
            Name = Admin,
            NormalizedName = Admin.ToUpper(CultureInfo.InvariantCulture)
        };

        public static readonly Role MemberRole = new Role
        {
            Id = new Guid("11D14A89-3A93-4D39-A94F-82B823F0D4CE"),
            Name = Member,
            NormalizedName = Member.ToUpper(CultureInfo.InvariantCulture)
        };

        private const string Admin = "Admin";
        private const string Member = "Member";
    }
}
