using System;
using System.Collections.Generic;

namespace Nucleus.Core.Permissions
{
    //todo: update permissions on app start, search about same example
    //todo: + this can be only applied for admin user
   public class DefaultPermissions
    {
        public static List<Permission> All()
        {
            return new List<Permission>
            {
                AdministrationAccess,
                MemberAccess,
                UserList
            };
        }

        public static readonly Permission AdministrationAccess = new Permission
        {
            DisplayName = "Administration access",
            Name = PermissionNameForAdministration,
            Id = new Guid("2A1CCB43-FA4F-48CE-B601-D3AB4D611B32")
        };

        public static readonly Permission MemberAccess = new Permission
        {
            DisplayName = "Member access",
            Name = PermissionNameForMemberAccess,
            Id = new Guid("28126FFD-51C2-4201-939C-B64E3DF43B9D")
        };

        public static readonly Permission UserList = new Permission
        {
            DisplayName = "User list",
            Name = PermissionNameForUserList,
            Id = new Guid("86D804BD-D022-49A5-821A-D2240478AAC4")
        };

        public const string PermissionNameForAdministration = "Permissions_Administration";
        public const string PermissionNameForMemberAccess = "Permissions_Member_Access";
        public const string PermissionNameForUserList = "Permissions_User_List";
        public const string PermissionNameForUserCreate = "Permissions_User_Create";
        public const string PermissionNameForUserUpdate = "Permissions_User_Update";
        public const string PermissionNameForUserDelete = "Permissions_User_Delete";
    }
}
