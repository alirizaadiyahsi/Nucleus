interface ICreateOrEditRoleInput extends IEntityDto {
    name: string;
    permissions: IPermissionDto[];
}
