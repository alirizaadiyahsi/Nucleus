interface IResetPasswordInput {
    userNameOrEmail: string;
    password: string;
    passwordRepeat: string;
    token: string;
}