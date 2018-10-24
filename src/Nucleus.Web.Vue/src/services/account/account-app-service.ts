import BaseAppService from '../base-app-service';
import AuthStore from '../../stores/auth-store';

export default class AccountAppService extends BaseAppService {
    public login(loginViewModel: ILoginViewModel) {
        return this.post<ILoginResult>('/api/account/login', loginViewModel)
            .then((response) => {
                if (!response.isError) {
                    AuthStore.setToken(response.content.token);
                }

                return response;
            });
    }

    public logOut(): void {
        AuthStore.removeToken();
    }

    public register(registerViewModel: IRegisterViewModel) {
        return this.post<IRegisterResult>('/api/account/register', registerViewModel)
            .then((response) => {
                return response;
            });
    }
}
