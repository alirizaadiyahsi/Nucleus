import BaseAppService from '@/services/base-app-service';
import AuthStore from '@/stores/auth-store';

export default class AccountAppService extends BaseAppService {
    public login(loginViewModel: ILoginInput) {
        return this.post<ILoginOutput>('/api/account/login', loginViewModel)
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

    public register(registerViewModel: IRegisterInput) {
        return this.post<IRegisterOutput>('/api/account/register', registerViewModel)
            .then((response) => {
                return response;
            });
    }
}
