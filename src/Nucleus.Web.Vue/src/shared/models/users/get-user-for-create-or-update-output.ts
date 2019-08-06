interface IGetUserForCreateOrUpdateOutput {
    user: IUserDto;
    allRoles: IPermissionDto[];
    grantedRoleIds: string[];
}