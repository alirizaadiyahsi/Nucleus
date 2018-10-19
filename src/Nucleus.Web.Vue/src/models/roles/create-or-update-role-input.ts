interface ICreateOrUpdateRoleInput extends IEntityDto {
    name: string;
    permissionIds: string[];
}
