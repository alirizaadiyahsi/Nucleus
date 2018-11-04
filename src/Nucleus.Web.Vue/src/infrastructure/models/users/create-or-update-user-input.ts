interface ICreateOrUpdateUserInput {
    user: IUserDto;
    grantedRoleIds: string[];
}