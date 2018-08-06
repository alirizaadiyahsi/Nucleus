using System;
using System.Collections.Generic;

namespace Nucleus.Core.Permissions
{
   public class DefaultPermissions
    {
        public static List<Permission> All()
        {
            return new List<Permission>
            {
                MemberAccess,
                UserList
            };
        }

        public static readonly Permission MemberAccess = new Permission
        {
            DisplayName = "Member access",
            Name = Member_Access,
            Id = new Guid("28126FFD-51C2-4201-939C-B64E3DF43B9D")
        };

        public static readonly Permission UserList = new Permission
        {
            DisplayName = "User list",
            Name = User_List,
            Id = new Guid("86D804BD-D022-49A5-821A-D2240478AAC4")
        };

        private const string Member_Access = "Permissions_Member_Access";
        private const string User_List = "Permissions_User_List";
        private const string User_Create = "Permissions_User_Create";
        private const string User_Update = "Permissions_User_Update";
        private const string User_Delete = "Permissions_User_Delete";
    }
}
