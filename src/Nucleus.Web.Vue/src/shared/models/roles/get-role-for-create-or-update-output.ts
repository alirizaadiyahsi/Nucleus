interface IGetRoleForCreateOrUpdateOutput {
    role: IRoleDto;
    allPermissions: IPermissionDto[];
    grantedPermissionIds: string[];
}