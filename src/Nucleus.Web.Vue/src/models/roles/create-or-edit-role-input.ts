interface ICreateOrEditRoleInput extends IEntityDto {
    name: string;
    permissionIds: string[];
}
