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
                UserList,
                RoleList,
                RoleAdd
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

        public static readonly Permission RoleList = new Permission
        {
            DisplayName = "Role list",
            Name = PermissionNameForRoleList,
            Id = new Guid("80562F50-8A7D-4BCD-8971-6D856BBCBB7F")
        };

        public static readonly Permission RoleAdd = new Permission
        {
            DisplayName = "Role add",
            Name = PermissionNameForRoleAdd,
            Id = new Guid("D4D7C0E3-EFCF-4DD2-86E7-17D69FDA8C75")
        };

        public static readonly Permission RoleDelete = new Permission
        {
            DisplayName = "Role delete",
            Name = PermissionNameForRoleDelete,
            Id = new Guid("8F76DE0B-114A-4DF8-A93D-CEC927D06A3C")
        };

        // todo: change permission names to CRUD letters for exam: create, read, update, delete
        public const string PermissionNameForAdministration = "Permissions_Administration";
        public const string PermissionNameForMemberAccess = "Permissions_Member_Access";
        public const string PermissionNameForUserList = "Permissions_User_List";
        public const string PermissionNameForUserAdd = "Permissions_User_Add";
        public const string PermissionNameForUserEdit = "Permissions_User_Edit";
        public const string PermissionNameForUserDelete = "Permissions_User_Delete";
        public const string PermissionNameForRoleList = "Permissions_Role_List";
        public const string PermissionNameForRoleAdd = "Permissions_Role_Add";
        public const string PermissionNameForRoleDelete = "Permissions_Role_Delete";
    }
}
