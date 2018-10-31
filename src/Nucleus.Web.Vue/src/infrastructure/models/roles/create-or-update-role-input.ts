interface ICreateOrUpdateRoleInput {
    role: IRoleDto;
    grantedPermissionIds: string[];
}