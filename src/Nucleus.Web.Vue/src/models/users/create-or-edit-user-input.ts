interface ICreateOrEditUserInput extends IEntityDto {
    userName: string;
    email: string;
    permissions: IPermissionDto[];
}