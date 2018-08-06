using System;
using System.Globalization;
using System.Collections.Generic;

namespace Nucleus.Core.Users
{
    public class DefaultUsers
    {
        public static List<User> All()
        {
            return new List<User>
            {
                AdminUser,
                MemberUser
            };
        }

        public static readonly User AdminUser = new User
        {
            Id = new Guid("C41A7761-6645-4E2C-B99D-F9E767B9AC77"),
            UserName = AdminUserName,
            Email = AdminEmail,
            EmailConfirmed = true,
            NormalizedEmail = AdminEmail.ToUpper(CultureInfo.InvariantCulture),
            NormalizedUserName = AdminUserName.ToUpper(CultureInfo.InvariantCulture),
            AccessFailedCount = 5,
            PasswordHash = PasswordHashFor123Qwe
        };

        public static readonly User MemberUser = new User
        {
            Id = new Guid("065E903E-6F7B-42B8-B807-0C4197F9D1BC"),
            UserName = MemberUserName,
            Email = MemberUserEmail,
            EmailConfirmed = true,
            NormalizedEmail = MemberUserEmail.ToUpper(CultureInfo.InvariantCulture),
            NormalizedUserName = MemberUserName.ToUpper(CultureInfo.InvariantCulture),
            AccessFailedCount = 5,
            PasswordHash = PasswordHashFor123Qwe
        };

        private const string AdminUserName = "admin";
        private const string AdminEmail = "admin@mail.com";
        private const string PasswordHashFor123Qwe = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw=="; //123qwe
        private const string MemberUserName = "memberuser";
        private const string MemberUserEmail = "memberuser@mail.com";
    }
}
